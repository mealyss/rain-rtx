namespace RainRTX
{
    public static class FloatExtenstion
    {
        public static float Saturate(this float x)
        {
            if (x > 1)
                return 1;
            else if (x < 0)
                return 0;
            return x;
        }
    }
}