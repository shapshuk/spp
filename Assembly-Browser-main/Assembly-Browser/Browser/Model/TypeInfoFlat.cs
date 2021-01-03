using System;
using System.Collections.Generic;
using System.Text;

namespace Browser.Model
{
    class TypeInfoFlat
    {
        public string Name { get; set; }
        //MFPC - MethodsFieldsPropertiesConstructors
        public List<string> MFPC { get; set; } = new List<string>();
    }
}
