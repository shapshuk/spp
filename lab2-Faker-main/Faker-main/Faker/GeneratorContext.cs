using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class GeneratorContext
    {
        public Random Random { get; }

        public Type TargetType { get; }

        public GeneratorContext(Random random, Type targetType)
        {
            Random = random;
            TargetType = targetType;
        }
    }
}
