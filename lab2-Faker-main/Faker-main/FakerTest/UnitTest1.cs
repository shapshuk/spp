using NUnit.Framework;
using FakerLib;
using System;
using NLog;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace FakerTest
{
    public class Tests
    {
        private Faker faker;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        [SetUp]
        public void Setup()
        {
            RegisterGen.Register(new IntGen());
            RegisterGen.Register(new StrGen());
            RegisterGen.Register(new DateGen());
            RegisterGen.Register(new ListGen());

            RegisterGen.RegisterPlugins();

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
        class FooTwoConstuctors
        {
            private int Data;
            private bool constructor1 = false;
            private bool constructor2 = false;

            public FooTwoConstuctors()
            {
                constructor1 = true;
            }
            public FooTwoConstuctors(int data)
            {
                Data = data;
                constructor2 = true;
            }
            public bool IsSetData()
            {
                return constructor2;
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
                logger.Info("ObjectFieldTest compled succesfully: object are't zero");
            else
                logger.Info("ObjectFieldTest compled not succesfully: object are zero");
            Assert.NotZero(i);
        }

        [Test]
        public void StringPropertyTest()
        {
            Bar bar = faker.Create<Bar>();
            if ((bar.DataBar != null) && (bar.DataBar.Any()))
                logger.Info("ObjectFieldTest compled succesfully: object are not empty");
            else
                logger.Info("ObjectFieldTest compled not succesfully: object are empty or null");
            Assert.IsNotEmpty(bar.DataBar);
        }
        [Test]
        public void ObjectFieldTest()
        {
            Foo foo = faker.Create<Foo>();
            if (foo.bar == null)
                logger.Info("ObjectFieldTest compled succesfully: object are equal");
            else
                logger.Info("ObjectFieldTest compled not succesfully: object are't equal");
            Assert.AreNotEqual(null, foo.bar);
        }
        [Test]
        public void PrivateFieldTest()
        {
            Foo2 foo = faker.Create<Foo2>();
            if (foo.IsSetData() == true)
                logger.Info("PrivateFieldTest compled succesfully: object are equal");
            else
                logger.Info("PrivateFieldTest compled not succesfully: object are't equal");
            Assert.AreEqual(true, foo.IsSetData());
        }
        [Test]
        public void TwoConstructorsTest()
        {
            FooTwoConstuctors foo = faker.Create<FooTwoConstuctors>();
            if (foo.IsSetData() == true)
                logger.Info("TwoConstructorsTest compled succesfully: object are equal");
            else
                logger.Info("TwoConstructorsTest compled not succesfully: object are't equal");
            Assert.AreEqual(true, foo.IsSetData());
        }
        [Test]
        public void ReccursionTest()
        {
            Rec1 rec = faker.Create<Rec1>();
            if (rec.A.B == null)
                logger.Info("ReccursionTest compled succesfully: object are equal");
            else
                logger.Info("ReccursionTest compled not succesfully: object are't equal");
            Assert.AreEqual(null, rec.A.B);
        }
        [Test]
        public void PrivateConstructorTest()
        {
            PrivateConstructor obj = faker.Create<PrivateConstructor>();
            if (obj == null)
                logger.Info("PrivateConstructorTest compled succesfully: object are equal");
            else
                logger.Info("PrivateConstructorTest compled not succesfully: object are't equal");
            Assert.AreEqual(null, obj);
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
                logger.Info("CreateIntTest compled succesfully: object are equal");
            else
                logger.Info("CreateIntTest compled not succesfully: object are't equal");
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
                logger.Info("CreateGeneticTest compled succesfully: object are empty");
            else
                logger.Info("CreateGeneticTest compled not succesfully: object are't empty or null");
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
                logger.Info("CreateRecTest compled succesfully: object are equal");
            else
                logger.Info("CreateRecTest compled not succesfully: object are't equal");
            Assert.AreEqual(expected, actual);
        }
    }
}