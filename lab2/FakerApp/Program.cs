using System;
using FakerLib;
using Newtonsoft.Json;

namespace FakerApp
{

    public class Program
    {
        public static void Main()
        {
            
            RegisterGenirators.Register(new IntGen());
            RegisterGenirators.Register(new StrGen());
            RegisterGenirators.Register(new ListGen());           
            RegisterGenirators.Register(new DateGen());
            
            RegisterGenirators.RegisterPlugins();


            var faker = new Faker();
            Foo foo = faker.Create<Foo>();
            Moo moo = faker.Create<Moo>();

            Console.WriteLine(JsonConvert.SerializeObject(foo));
            Console.WriteLine(JsonConvert.SerializeObject(moo));

        }
    }
}