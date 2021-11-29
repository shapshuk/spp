using System;
using System.Collections.Generic;
using System.Linq;

namespace FakerLib
{
    public static class Generator
    {
        public static List<IGenerator> Generators = new List<IGenerator>();

        public static object Generate(Type type)
        {
            foreach (var gen in Generators)
            {
                if (gen.CanGenerate(type))
                {
                    return gen.Generate(type);
                }
            }
            
            return null;
        }

        public static bool CanGenerate(Type type)
        {
            return Generators.Any(gen => gen.CanGenerate(type));
        }
    }
}