using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace Foo
{
    public class Foo
    {
        [Test]
        public void Run(int a)
        {
            Assert.Fail("autogenerated");
        }
    }
}