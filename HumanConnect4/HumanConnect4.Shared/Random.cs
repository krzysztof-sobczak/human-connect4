﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4
{
    class Random
    {
        private const int RANDOM_SEED = 100;
        private static System.Random Rand = new System.Random(RANDOM_SEED);

        public static float PositiveFloat(int range = 10)
        {
            return (float)Rand.NextDouble() * range;
        }
        public static float BipolarFloat(int range = 10)
        {
            return ((float)Rand.NextDouble() * (range * 2) - range);
        }
    }
}
