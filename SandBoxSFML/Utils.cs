using System;

namespace SandBoxSFML
{
    public static class Utils
    {
        private static readonly Random _r = new Random();

        public static int Clamp(int num, int min, int max)
        {
            return num < min ? min : num > max ? max : num;
        }

        public static int Next(int min, int max)
        {
            return _r.Next(min, max);
        }

        public static double NextDouble()
        {
            return _r.NextDouble();
        }

        public static int RandomValue(int lower, int upper)
        {
            if (lower > upper)
            {
                var t = lower;
                lower = upper;
                upper = t;
            }

            return _r.Next(int.MaxValue) % (upper - lower + 1) + lower;
        }

        public static bool NextBoolean()
        {
            return _r.Next() > (Int32.MaxValue / 2);
        }
    }
}
