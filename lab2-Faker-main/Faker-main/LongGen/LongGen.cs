using FakerLib;
using System;

namespace LongGen
{
    class LongGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(long);
        }

        public object Generate(GeneratorContext context)
        {
            return LongRandom(context.Random);
        }
        long LongRandom(Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            long result = (Math.Abs(longRand % (2000000000000000 - 1000000000000000)) + 1000000000000000);

            long random_seed = rand.Next(1000, 5000);
            random_seed = random_seed * result + rand.Next(1000, 5000);

            return random_seed / 655 % 10000000000000001;
        }
    }
}
