using System;
using System.IO;
using System.Reflection;

namespace FakerLib
{
    public static class RegisterGenerators
    {
        public static string Path { get; } = "F:\\Work\\spp\\lab2\\FakerApp\\plugins";

        public static void Register(IGenerator gen)
        {
            Generator.Generators.Add(gen);
        }
        
        public static void RegisterPlugins()
        {
            string[] plugins = Directory.GetFiles(Path);
            foreach (var plugin in plugins)
            {
                Assembly asm = Assembly.LoadFrom(plugin);
                Type objType = asm.GetType($"{asm.GetTypes()[0]}", true, true);

                IGenerator gen = (IGenerator)Activator.CreateInstance(objType);

                Generator.Generators.Add(gen);
            }
        }
        
    }
}