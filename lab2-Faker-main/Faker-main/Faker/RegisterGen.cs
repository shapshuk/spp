using FakerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FakerLib
{
    public static class RegisterGen
    {
        public static string Path { get; set; } = "F:\\Work\\SPP\\lab2-Faker-main\\Faker-main\\plugins";
        public static void Register(IGenerator gen)
        {
            Generator.generators.Add(gen);
        }
        public static void RegisterPlugins()
        {
            string[] plugins = Directory.GetFiles(Path);
            foreach (var plugin in plugins)
            {
                Assembly asm = Assembly.LoadFrom(plugin);
                Type obj_type = asm.GetType($"{asm.GetTypes()[0]}", true, true);

                IGenerator gen = (IGenerator)Activator.CreateInstance(obj_type);

                Generator.generators.Add(gen);
            }
        }
    }
}
