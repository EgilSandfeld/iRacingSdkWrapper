using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using iRSDKSharp;
using YamlDotNet.RepresentationModel;

namespace iRacingSdkWrapper
{
    /// <summary>
    /// Represents a single update of the Session Info YAML and includes parsing capabilities.
    /// </summary>
    public class SessionInfo
    {
        public SessionInfo(string yaml, double updateTime)
        {
            _updateTime = updateTime;

            //_yaml = yaml;
            this.FixYaml(yaml);
            this.ParseYaml();
        }

        #region Properties

        private readonly double _updateTime;

        /// <summary>
        /// The time of this update.
        /// </summary>
        public double UpdateTime
        {
            get { return _updateTime; }
        }

        private string _yaml;

        /// <summary>
        /// The raw YAML string representing the session info.
        /// </summary>
        public string Yaml
        {
            get { return _yaml; }
        }

        private bool _isValidYaml;

        public bool IsValidYaml
        {
            get { return _isValidYaml; }
        }

        private YamlStream _yamlStream;

        public YamlStream YamlStream
        {
            get { return _yamlStream; }
        }

        private YamlMappingNode _yamlRoot;

        public YamlMappingNode YamlRoot
        {
            get { return _yamlRoot; }
        }

        #endregion

        #region Methods

        private void FixYaml(string yaml)
        {
            if (string.IsNullOrEmpty(yaml))
                return;

            yaml = ApplyQuotingFixes(yaml);
            _yaml = FixMultiColons(yaml);

            var indexOfSetup = _yaml.IndexOf("CarSetup:", StringComparison.Ordinal);
            if (indexOfSetup > 0)
            {
                ExtractCarSetupData(indexOfSetup);

                // Remove the setup info
                _yaml = _yaml.Substring(0, indexOfSetup);
            }

            //Obsolete - Handled now by ApplyQuotingFixes
            // AbbrevName missing name due to ??????????
            //_yaml = Regex.Replace(_yaml, @"AbbrevName:\s*,?\s*(?=\r?\n)", "AbbrevName: Doe");
            //_yaml = _yaml.Replace("AbbrevName:   ,  ", "AbbrevName: Doe, John");
            //_yaml = _yaml.Replace("AbbrevName:  ,  ", "AbbrevName: Doe, John");
            //_yaml = _yaml.Replace("AbbrevName:          ", "AbbrevName: Doe");
        }

        private static readonly Regex SetupFuelLevelRegex = new("FuelLevel: (.*) L", RegexOptions.Compiled);
        private static readonly Regex TireTypeRegex = new("TireType: (.*)", RegexOptions.Compiled);
        private static readonly Regex TireCompoundRegex = new("TireCompound: (.*)", RegexOptions.Compiled);

        // private void ExtractCarSetupData(int indexOfSetup)
        // {
        //     var setupString = _yaml.Substring(indexOfSetup);
        //     var setupFuelLevelMatch = SetupFuelLevelRegex.Match(setupString);
        //     if (setupFuelLevelMatch.Success && float.TryParse(setupFuelLevelMatch.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var setupFuelLevel))
        //         SetupFuelLevel = setupFuelLevel;
        //
        //     var setupTiresMatch = TireTypeRegex.Match(setupString);
        //     if (setupTiresMatch.Success)
        //     {
        //         var newSetupTires = setupTiresMatch.Groups[1].Value.Replace("\r", string.Empty).Replace("\n", string.Empty);
        //         if (!string.IsNullOrEmpty(newSetupTires))
        //             SetupTires = newSetupTires;
        //     }
        //     else
        //     {
        //         setupTiresMatch = TireCompoundRegex.Match(setupString);
        //         if (setupTiresMatch.Success)
        //         {
        //             var newSetupTires = setupTiresMatch.Groups[1].Value.Replace("\r", string.Empty).Replace("\n", string.Empty);
        //             if (!string.IsNullOrEmpty(newSetupTires))
        //                 SetupTires = newSetupTires;
        //         }
        //     }
        // }

        private void ExtractCarSetupData(int indexOfSetup)
        {
            // Only create a small substring containing the relevant section
            var setupSection = _yaml.Substring(indexOfSetup);

            var setupFuelLevelMatch = SetupFuelLevelRegex.Match(setupSection);
            if (setupFuelLevelMatch.Success && float.TryParse(setupFuelLevelMatch.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var setupFuelLevel))
                SetupFuelLevel = setupFuelLevel;

            // For tire types, search in limited section rather than entire yaml
            var setupTiresMatch = TireTypeRegex.Match(setupSection);
            if (setupTiresMatch.Success)
            {
                var newSetupTires = setupTiresMatch.Groups[1].Value.Replace("\r", string.Empty).Replace("\n", string.Empty);
                if (!string.IsNullOrEmpty(newSetupTires))
                    SetupTires = newSetupTires;
            }
            else
            {
                // Look for tire compound in a limited substring
                setupTiresMatch = TireCompoundRegex.Match(setupSection);
                if (setupTiresMatch.Success)
                {
                    var newSetupTires = setupTiresMatch.Groups[1].Value.Replace("\r", string.Empty).Replace("\n", string.Empty);
                    if (!string.IsNullOrEmpty(newSetupTires))
                        SetupTires = newSetupTires;
                }
            }
        }

        /// <summary>
        /// Quick hack: if there's more than 1 colon ":" in a line, keep only the first
        /// </summary>
        private static string FixMultiColons(string yaml)
        {
            using var reader = new StringReader(yaml);
            var builder = new StringBuilder();
            while (reader.ReadLine() is { } line)
            {
                // Only process lines with multiple colons that don't have quotes
                // (to avoid breaking already properly quoted values with colons)
                if (line.Count(c => c == ':') > 1 && !line.Contains("'"))
                {
                    var chars = line.ToCharArray();
                    var foundFirst = false;
                    for (var i = 0; i < chars.Length; i++)
                    {
                        var c = chars[i];
                        if (c != ':')
                            continue;

                        if (!foundFirst)
                        {
                            foundFirst = true;
                            continue;
                        }

                        chars[i] = '-';
                    }

                    line = new string(chars);
                }

                builder.AppendLine(line);
            }

            return builder.ToString();
        }

        private readonly string[] _keysToFix = ["AbbrevName:", "TeamName:", "UserName:", "Initials:", "DriverSetupName:"];
        private const int MaxNumDrivers = 64;
        private const int MaxNumAdditionalBytesPerFixedKey = 2;

        private string ApplyQuotingFixes(string input)
        {
            var keysToTrack = new YamlKeys[_keysToFix.Length];
            for (var i = 0; i < keysToTrack.Length; i++)
                keysToTrack[i] = new YamlKeys { Key = _keysToFix[i] };

            var keyTrackersIgnoringUntilNextLine = 0;
            var stringBuilder = new StringBuilder(input.Length + _keysToFix.Length * MaxNumAdditionalBytesPerFixedKey * MaxNumDrivers);

            foreach (var ch in input)
            {
                if (keyTrackersIgnoringUntilNextLine == keysToTrack.Length)
                {
                    if (ch == '\n')
                    {
                        keyTrackersIgnoringUntilNextLine = 0;
                        foreach (var keyTracker in keysToTrack)
                        {
                            keyTracker.Count = 0;
                            keyTracker.IgnoreUntilNextLine = false;
                        }
                    }
                }
                else
                {
                    foreach (var keyTracker in keysToTrack)
                    {
                        if (keyTracker.IgnoreUntilNextLine)
                        {
                            if (ch == '\n')
                            {
                                keyTracker.Count = 0;
                                keyTracker.IgnoreUntilNextLine = false;
                                keyTrackersIgnoringUntilNextLine--;
                            }
                        }
                        else if (keyTracker.AddFirstQuote)
                        {
                            if (ch == '\n')
                            {
                                keyTracker.Count = 0;
                                keyTracker.AddFirstQuote = false;
                            }
                            else if (ch != ' ')
                            {
                                stringBuilder.Append('\'');
                                keyTracker.AddFirstQuote = false;
                                keyTracker.AddSecondQuote = true;
                            }
                        }
                        else if (keyTracker.AddSecondQuote)
                        {
                            if (ch == '\n')
                            {
                                stringBuilder.Append('\'');
                                keyTracker.Count = 0;
                                keyTracker.AddSecondQuote = false;
                            }
                            else if (ch == '\'')
                            {
                                stringBuilder.Append('\''); // Escape single quotes within the value
                            }
                        }
                        else
                        {
                            if (ch == keyTracker.Key[keyTracker.Count])
                            {
                                keyTracker.Count++;
                                if (keyTracker.Count == keyTracker.Key.Length)
                                {
                                    keyTracker.AddFirstQuote = true;
                                }
                            }
                            else if (ch != ' ')
                            {
                                keyTracker.IgnoreUntilNextLine = true;
                                keyTrackersIgnoringUntilNextLine++;
                            }
                        }
                    }
                }

                stringBuilder.Append(ch);
            }

            // Handle any pending quotes at the end of the string
            foreach (var keyTracker in keysToTrack)
            {
                if (keyTracker.AddSecondQuote)
                    stringBuilder.Append('\'');
            }

            return stringBuilder.ToString();
        }

        private class YamlKeys
        {
            public string Key = string.Empty;
            public int Count;
            public bool IgnoreUntilNextLine;
            public bool AddFirstQuote;
            public bool AddSecondQuote;
        }

        public float SetupFuelLevel { get; set; }
        public string SetupTires { get; set; }

        private void ParseYaml()
        {
            // Clear existing resources before parsing to help with memory pressure
            _yamlRoot = null;
            _isValidYaml = false;

            if (string.IsNullOrEmpty(Yaml))
                return;

            try
            {
                // Use StreamReader with minimal buffer to reduce memory allocation
                using var reader = new StringReader(Yaml);

                // Reuse existing YamlStream if possible
                if (_yamlStream == null)
                    _yamlStream = new YamlStream();
                else
                    _yamlStream.Documents.Clear();

                // Consider adding deserialization options here if the YAML library supports it
                // For example: var deserializerBuilder = new DeserializerBuilder().WithMaximumRecursion(20);

                _yamlStream.Load(reader);

                // Avoid cast if possible by checking type first
                if (_yamlStream.Documents.Count > 0)
                {
                    var rootNode = _yamlStream.Documents[0].RootNode as YamlMappingNode;
                    if (rootNode != null)
                    {
                        _yamlRoot = rootNode;
                        _isValidYaml = true;
                    }
                }
            }
            catch (Exception)
            {
                // Consider implementing IDisposable pattern if storing large YAML data
                _yamlRoot = null;
                _isValidYaml = false;
            }
        }

        public YamlQuery this[string key]
        {
            get { return YamlQuery.Mapping(_yamlRoot, key); }
        }

        /// <summary>
        /// Gets a value from the session info YAML, or null if there is an error.
        /// </summary>
        /// <param name="query">The YAML query path to the value.</param>
        public string TryGetValue(string query)
        {
            if (!this.IsValidYaml) return null;
            try
            {
                return YamlParser.Parse(_yaml, query);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a value from the session info YAML. Returns true if successfull, false if there is an error.
        /// </summary>
        /// <param name="query">The YAML query path to the value.</param>
        /// <param name="value">When this method returns, contains the requested value if the query was valid, or null if the query was invalid.</param>
        public bool TryGetValue(string query, out string value)
        {
            if (!this.IsValidYaml)
            {
                value = null;
                return false;
            }

            try
            {
                value = YamlParser.Parse(_yaml, query);
                return true;
            }
            catch (Exception)
            {
                value = null;
                return false;
            }
        }

        /// <summary>
        /// Gets a value from the session info YAML. May throw an exception. Use <see cref="TryGetValue"/> for safer operation.
        /// </summary>
        /// <param name="query">The YAML query path to the value.</param>
        public string GetValue(string query)
        {
            if (!this.IsValidYaml) return null;
            return YamlParser.Parse(_yaml, query);
        }

        #endregion
    }
}