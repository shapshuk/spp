using NUnit.Framework;
using FakerLib;
using System;
using NLog;
using Moq;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal;
using Logger = NLog.Logger;

namespace FakerTest
{
    public class Tests
    {
        private Faker faker;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [SetUp]
        public void Setup()
        {
            RegisterGenerators.Register(new IntGen());
            RegisterGenerators.Register(new StrGen());
            RegisterGenerators.Register(new DateGen());
            RegisterGenerators.Register(new ListGen());

            RegisterGenerators.RegisterPlugins();

            faker = new Faker();
        }

        class Bar
        {
            public string DataBar { get; set; }
        }

        class Foo
        {
            public Bar bar;
        }

        class Foo2
        {
            private int Data;
            private bool flag = false;

            public Foo2(int data)
            {
                Data = data;
                flag = true;
            }

            public bool IsSetData()
            {
                return flag;
            }
        }

        class Rec1
        {
            public Rec2 A;
        }

        class Rec2
        {
            public Rec1 B;
        }

        class PrivateConstructor
        {
            private PrivateConstructor()
            {

            }
        }

        [Test]
        public void SHouldGEnerateINt()
        {
            var i = faker.Create<int>();
            if (i != 0)
                logger.Info("ObjectFieldTest compled succesfully: objects are't zero");
            else
                logger.Info("ObjectFieldTest compled not succesfully: objects are zero");
            Assert.NotZero(i);
        }

        [Test]
        public void StringPropertyTest()
        {
            Bar bar = faker.Create<Bar>();
            if ((bar.DataBar != null) && (bar.DataBar.Any()))
                logger.Info("ObjectFieldTest completed succesfully: objects are not empty");
            else
                logger.Info("ObjectFieldTest completed not succesfully: objects are empty or null");
            Assert.IsNotEmpty(bar.DataBar);
        }

        [Test]
        public void ObjectFieldTest()
        {
            Foo foo = faker.Create<Foo>();
            if (foo.bar == null)
                logger.Info("ObjectFieldTest completed succesfully: objects are equal");
            else
                logger.Info("ObjectFieldTest completed not succesfully: objects are't equal");
            Assert.AreNotEqual(null, foo.bar);
        }

        [Test]
        public void PrivateFieldTest()
        {
            Foo2 foo = faker.Create<Foo2>();
            if (foo.IsSetData() == true)
                logger.Info("PrivateFieldTest completed succesfully: objects are equal");
            else
                logger.Info("PrivateFieldTest completed not succesfully: objects are't equal");
            Assert.AreEqual(true, foo.IsSetData());
        }

        [Test]
        public void RecursionTest()
        {
            var exception = Assert.Throws<Exception>(() => faker.Create<Rec1>());
            Assert.AreEqual(exception.Message, "Cyclic dependency!");
            
        }

        [Test]
        public void PrivateConstructorTest()
        {
            var exception = Assert.Throws<Exception>(() => faker.Create<PrivateConstructor>());
            Assert.AreEqual(exception.Message, "Type has no public constructors!");
        }

        [Test]
        public void CreateIntTest()
        {
            var mock = new Mock<IFaker>();
            mock.Setup(a => a.Create<int>()).Returns(15);

            IFaker mockFaker = mock.Object;
            int expected = 15;
            int actual = mockFaker.Create<int>();

            if (expected == actual)
                logger.Info("CreateIntTest completed succesfully: objects are equal");
            else
                logger.Info("CreateIntTest completed not succesfully: objects are't equal");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateGeneticTest()
        {
            var mock = new Mock<IFaker>();
            mock.Setup(a => a.Create<List<string>>()).Returns(new List<string>());

            IFaker mockFaker = mock.Object;
            List<string> actual = mockFaker.Create<List<string>>();

            if ((actual != null) && (!actual.Any()))
                logger.Info("CreateGeneticTest completed succesfully: objects are empty");
            else
                logger.Info("CreateGeneticTest completed not succesfully: objects are't empty or null");
            Assert.IsEmpty(actual);
        }

        [Test]
        public void CreateRecTest()
        {
            var mock = new Mock<IFaker>();
            mock.Setup(a => a.Create<Rec1>()).Returns(new Rec1());

            IFaker mockFaker = mock.Object;
            Rec2 expected = null;
            Rec2 actual = mockFaker.Create<Rec1>().A;

            if (expected == actual)
                logger.Info("CreateRecTest completed succesfully: objects are equal");
            else
                logger.Info("CreateRecTest completed not succesfully: objects are't equal");
            Assert.AreEqual(expected, actual);
        }
    }
}
