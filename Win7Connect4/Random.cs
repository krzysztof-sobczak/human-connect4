using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4
{
    class Random
    {
        private static int RANDOM_SEED = 100;
        private static System.Random Rand = new System.Random(RANDOM_SEED);

        public static float PositiveFloat(float range = 0.1f)
        {
            return (float)Rand.NextDouble() * range;
        }
        public static float BipolarFloat(float range = 0.1f)
        {
            return ((float)Rand.NextDouble() * (range * 2) - range);
        }

        public static void setSeed(int seed)
        {
            RANDOM_SEED = seed;
        }
    }
}
