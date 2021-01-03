using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public interface IGenerator
    {
        object Generate(GeneratorContext context);
        bool CanGenerate(Type type);
    }
}
