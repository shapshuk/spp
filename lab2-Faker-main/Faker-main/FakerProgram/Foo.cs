using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace FakerProgram
{
    class Foo
    {
        public string Data { get; private set; }
        public int Number { get; private set; }
        public string Info { get; set; }
        public int Id { get; set; } 

        public Bar B { get; set; }

        public int field = 2;
        private Foo()
        {

        }


    }
}
