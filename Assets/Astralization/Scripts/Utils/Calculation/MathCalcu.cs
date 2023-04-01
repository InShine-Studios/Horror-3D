using System;

namespace Astralization.Utils.Calculation
{
    /*
     * Struct for storing ranged value
     */
    [Serializable]
    public struct Range
    {
        public float min;
        public float max;
    }


    /*
    * Class that has methods for mathematical calculation
    */
    public static class MathCalcu
    {
        public static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
