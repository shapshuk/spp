using FakerLib;
using NUnit.Framework;

namespace FakerTests
{
    public class Tests
    {
        private Faker _faker;
        
        [SetUp]
        public void Setup()
        {
            RegisterGenirators.Register(new IntGen());
            RegisterGenirators.Register(new StrGen());
            RegisterGenirators.Register(new ListGen());           
            RegisterGenirators.Register(new DateGen());
            
            RegisterGenirators.RegisterPlugins();
            
            _faker = new Faker();
        }

        class Foo
        {
            public int Bar { get; set; }
            public string Baz;
            public float Moo;
        }
        
        [Test]
        public void Test1()
        {
            var result = _faker.Create<Foo>();
            
            Assert.That(result.Bar != 0);
            Assert.IsNotEmpty(result.Baz);
        }
        
        [Test]
        public void Test2()
        {
            var result = _faker.Create<int>();
            
            Assert.That(result != 0);
        }
        
        [Test]
        public void Test3()
        {
            var result = _faker.Create<Foo>();
            Assert.That(result.Moo != default);
        }
        
    }
}