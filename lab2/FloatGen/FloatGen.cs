using System;
using FakerLib;

namespace FloatGen
{
    public class FloatGen : IGenerator
    {
        private readonly Random _random = new Random();
        public bool CanGenerate(Type type)
        {
            return type == typeof(float);
        }

        public object Generate(Type type)
        {
            double mantissa = (_random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, _random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }
    }
}