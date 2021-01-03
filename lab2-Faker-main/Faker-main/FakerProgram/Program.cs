using FakerLib;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace FakerProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterGen.Register(new IntGen());
            RegisterGen.Register(new StrGen());
            RegisterGen.Register(new DateGen());
            RegisterGen.Register(new ListGen());

            RegisterGen.RegisterPlugins();

            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            Moo moo = faker.Create<Moo>();
            Console.WriteLine(JsonConvert.SerializeObject(foo));
            Console.WriteLine(JsonConvert.SerializeObject(moo));
        }
    }
}
