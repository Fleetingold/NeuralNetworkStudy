using System;

namespace AutoEncoder
{
    public class Utility
    {
        public static void SetArrayToZero(double[] dArray)
        {
            dArray = null;
        }

        public static void WithinBounds(string errorInfo, int pWhichLayer, int numlayers)
        {
            if (pWhichLayer < 0 || pWhichLayer >= numlayers)
                throw new IndexOutOfRangeException(errorInfo);
        }
    }
}