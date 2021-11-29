using System;
using System.Collections.Generic;

namespace FakerLib
{
    public class ListGen : IGenerator
    {
        private readonly Random _random = new Random();
        public bool CanGenerate(Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
        }
        

        public object Generate(Type type)
        {
            var list = Activator.CreateInstance(type);
            var size = _random.Next(1, 6);
            var currentType = type.GetGenericArguments()[0];

            foreach (var gen in Generator.Generators)
            {
                if (gen.CanGenerate(currentType))
                {
                    var method = type.GetMethod("Add");
                    // context = new GeneratorContext(_random, type.GetGenericArguments()[0]);
                    for (var i = 0; i < size; i++)
                    {
                        method.Invoke(list, new object[] { gen.Generate(currentType) });
                    }

                    break;
                }
            }

            return list;
        }
    }
}