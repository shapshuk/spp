using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakerLib
{
    public class ListGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
        }

        public object Generate(GeneratorContext context)
        {
            var result = Activator.CreateInstance(context.TargetType);
            int size = context.Random.Next(1, 6);
            Type currentType = context.TargetType.GetGenericArguments()[0];

            foreach (var gen in Generator.generators)
            {
                if (gen.CanGenerate(currentType))
                {
                    var method = context.TargetType.GetMethod("Add");
                    context = new GeneratorContext(context.Random, context.TargetType.GetGenericArguments()[0]);
                    for (var i = 0; i < size; i++)
                    {
                        method.Invoke(result, new object[] { gen.Generate(context) } );
                    }
                    break;
                }
            }  
            return result;
        }
    }
}
