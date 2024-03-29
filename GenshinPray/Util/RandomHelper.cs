﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Util
{
    public static class RandomHelper
    {
        public static int getRandomBetween(int minValue, int maxValue)
        {
            Random random = getRandom();
            return random.Next(minValue, maxValue + 1);
        }

        public static Random getRandom()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int seed = BitConverter.ToInt32(buffer, 0);
            return new Random(seed);
        }

    }
}
