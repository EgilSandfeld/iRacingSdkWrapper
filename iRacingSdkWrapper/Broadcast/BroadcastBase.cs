namespace iRacingSdkWrapper.Broadcast
{
    public abstract class BroadcastBase
    {
        private readonly SdkWrapper _wrapper;

        internal BroadcastBase(SdkWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        protected void Broadcast(iRSDKSharp.BroadcastMessageTypes type, int var1, int var2)
        {
            if (!_wrapper.IsConnected) return;
            _wrapper.Sdk.BroadcastMessage(type, var1, var2);
        }

        protected void Broadcast(iRSDKSharp.BroadcastMessageTypes type, int var1, int var2, int var3)
        {
            if (!_wrapper.IsConnected) return;
            _wrapper.Sdk.BroadcastMessage(type, var1, var2, var3);
        }

        /// <remark>
        /// Does not work as it multiplies the fraction away into an integer realm!
        /// 
        /// The selected code is a single line of C++ code that is used to convert a floating-point number to an integer, while preserving its fractional part. This is achieved by multiplying the floating-point number by `2^16-1` (or `65536.0f` in floating-point representation) before casting it to an integer.
        /// int real = (int)(var2 * 65536.0f);
        /// The multiplication by `65536.0f` effectively moves the fractional part of the floating-point number into the integer part. This is because `65536` is `2^16`, and multiplying by this value is equivalent to shifting the binary representation of the number 16 places to the left. This operation moves the fractional part of the number (the part after the decimal point) into the integer part of the number (the part before the decimal point).
        /// After this multiplication, the floating-point number is cast to an integer using `(int)`. This operation truncates any remaining fractional part, resulting in an integer.
        /// This technique is often used in fixed-point arithmetic, where a fixed number of digits after the decimal point are used to represent fractional values. It allows fractional values to be represented and manipulated using integer operations, which can be more efficient on some hardware.
        /// </remark>
        protected void Broadcast(iRSDKSharp.BroadcastMessageTypes type, int var1, float var2)
        {
            if (!_wrapper.IsConnected) return;
            _wrapper.Sdk.BroadcastMessage(type, var1, var2);
        }
    }
}
