using System;
using System.Collections.Generic;
using System.Linq;

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

        public static TValue GetRandomValue<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            List<TValue> values = Enumerable.ToList(dict.Values);
            int size = dict.Count;
            return values[Rand.Next(size)];
        }

        public static TKey GetRandomKey<TKey, TValue>(IDictionary<TKey, TValue> dict)
        {
            List<TKey> values = Enumerable.ToList(dict.Keys);
            int size = dict.Count;
            return values[Rand.Next(size)];
        }
    }
}
