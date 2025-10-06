using System;
using iRSDKSharp;
using iRacingSdkWrapper.Bitfields;

namespace iRacingSdkWrapper
{
    public abstract class TelemetryValue
    {
        protected TelemetryValue(iRacingSDK sdk, string name)
        {
            if (sdk == null) 
                throw new ArgumentNullException("sdk");

            _exists = sdk.VarHeaders.ContainsKey(name);
            if (_exists)
            {
                var header = sdk.VarHeaders[name];
                _name = name;
                _description = header.Desc;
                _unit = header.Unit;
                _type = header.Type;
            }
        }

        private readonly bool _exists;
        /// <summary>
        /// Whether or not a telemetry value with this name exists on the current car.
        /// </summary>
        public bool Exists { get { return _exists; } }

        private readonly string _name;
        /// <summary>
        /// The name of this telemetry value parameter.
        /// </summary>
        public string Name { get { return _name; } }

        private readonly string _description;
        /// <summary>
        /// The description of this parameter.
        /// </summary>
        public string Description { get { return _description; } }

        private readonly string _unit;
        /// <summary>
        /// The real world unit for this parameter.
        /// </summary>
        public string Unit { get { return _unit; } }

        private readonly CVarHeader.VarType _type;
        /// <summary>
        /// The data-type for this parameter.
        /// </summary>
        public CVarHeader.VarType Type { get { return _type; } }

        public abstract object GetValue();
    }

    /// <summary>
    /// Represents a telemetry parameter of the specified type.
    /// </summary>
    /// <typeparam name="T">The .NET type of this parameter (int, char, float, double, bool, or arrays)</typeparam>
    public sealed class TelemetryValue<T> : TelemetryValue 
    {
        private static readonly Func<int, T> _bitfieldFactory;
        private static readonly bool _useBitFieldFactory;
        
        static TelemetryValue()
        {
            var type = typeof(T);
            if (type.BaseType is { IsGenericType: true } && type.BaseType.GetGenericTypeDefinition() == typeof(BitfieldBase<>))
            {
                // THIS IS CORRECT - NO CHANGES NEEDED HERE
                // Find the constructor that takes an 'int' (the underlying type of the bitfield)
                var constructor = type.GetConstructor(new[] { typeof(int) });
                if (constructor != null)
                {
                    var param = System.Linq.Expressions.Expression.Parameter(typeof(int), "p");
                    var creator = System.Linq.Expressions.Expression.New(constructor, param);
                    // The factory now correctly takes an 'int' and returns 'T'
                    _bitfieldFactory = System.Linq.Expressions.Expression.Lambda<Func<int, T>>(creator, param).Compile();
                    _useBitFieldFactory = true;
                }
            }
        }
        
        public TelemetryValue(iRSDKSharp.iRacingSDK sdk, string name)
            : base(sdk, name)
        {
            RefreshValue(sdk);
        }

        // private void GetData(iRacingSDK sdk)
        // {
        //     try
        //     {
        //         if (!Exists)
        //             return;
        //         
        //         var data = sdk.GetData(this.Name);
        //         if (data == null)
        //             return;
        //         
        //         var type = typeof(T);
        //         if (type.BaseType is { IsGenericType: true } && type.BaseType.GetGenericTypeDefinition() == typeof(BitfieldBase<>))
        //             _Value = (T)Activator.CreateInstance(type, new[] { data });
        //         else
        //             _Value = (T)data;
        //     }
        //     catch (Exception ex)
        //     {
        //         ex.Data.Add("Name", Name);
        //         throw;
        //     }
        // }

        private T _Value;
        /// <summary>
        /// The value of this parameter.
        /// </summary>
        public T Value { get { return _Value; } }

        public override object GetValue()
        {
            return this.Value;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Value, this.Unit);
        }

        public void RefreshValue(iRacingSDK sdk)
        {
            try
            {
                if (!sdk.IsInitialized || !sdk.IsConnected() || !Exists) 
                    return;

                // Check if we pre-compiled a factory for this type 'T'
                if (_useBitFieldFactory)
                {
                    // This is a bitfield type. Get the raw int value (no boxing).
                    var rawValue = sdk.GetValue<int>(this.Name);
                    // Use the FAST, pre-compiled factory to create the object. No reflection!
                    _Value = _bitfieldFactory(rawValue);
                }
                else
                {
                    // This is a normal type (int, float, etc.)
                    // This is a direct cast from the generic method. NO BOXING.
                    _Value = sdk.GetValue<T>(this.Name);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Name", Name);
                throw;
            }
        }
    }
}
