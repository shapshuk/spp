using System;

namespace FakerLib
{
    public interface IGenerator
    {
        object Generate(Type type);
        bool CanGenerate(Type type);
    }
}