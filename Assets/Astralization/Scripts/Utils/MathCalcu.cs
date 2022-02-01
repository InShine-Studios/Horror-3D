namespace Utils
{
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
