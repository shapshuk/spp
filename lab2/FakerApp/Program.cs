using System;
using FakerLib;
using Newtonsoft.Json;

namespace FakerApp
{

    public class Program
    {
        public static void Main()
        {
            
            RegisterGenerators.Register(new IntGen());
            RegisterGenerators.Register(new StrGen());
            RegisterGenerators.Register(new ListGen());           
            RegisterGenerators.Register(new DateGen());
            
            RegisterGenerators.RegisterPlugins();


            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            Moo moo = faker.Create<Moo>();

            Console.WriteLine(JsonConvert.SerializeObject(foo));
            Console.WriteLine(JsonConvert.SerializeObject(moo));

        }
    }
}