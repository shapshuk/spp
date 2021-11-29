using System;

namespace FakerLib
{
    public class IntGen : IGenerator
    {
        private readonly Random _random = new Random();
        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        public object Generate(Type type)
        {
            return _random.Next(-2147483648, 2147483647);
        }
    }
}