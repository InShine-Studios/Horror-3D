using System;

namespace Utils
{
    /**
     * Randomizer Class
     * Randomize int, float, string, etc
     */
    public static class Randomizer
    {
        public static Random Rand = new Random();

        public static float GetFloat(float min = 0.0f, float max = 1f)
        {
            float range = max - min;
            double sample = Rand.NextDouble();
            double scaled = (sample * range) + min;
            return (float)scaled;
        }
    }

}
