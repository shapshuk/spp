using System;
using System.Collections.Generic;
using System.Text;

namespace Browser.Model
{
    class TypeInfoFlat
    {
        public string Name { get; set; }
        public List<string> ClassElements { get; set; } = new List<string>();
    }
}
