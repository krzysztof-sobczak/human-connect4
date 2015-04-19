using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4
{
    class Random
    {
        private const int RANDOM_SEED = 100;

        public static float PositiveFloat(int range = 10)
        {
            System.Random random = new System.Random(RANDOM_SEED);
            return (float)random.NextDouble() * range;
        }
        public static float BipolarFloat(int range = 10)
        {
            System.Random random = new System.Random(RANDOM_SEED);
            return ((float)random.NextDouble() * (range * 2) - range);
        }
    }
}
