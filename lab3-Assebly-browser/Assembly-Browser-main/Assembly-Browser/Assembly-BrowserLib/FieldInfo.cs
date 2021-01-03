using System;
using System.Collections.Generic;
using System.Text;


namespace Assembly_BrowserLib
{
    public class FieldInfo : BaseInfo
    {
        public void SetName(System.Reflection.FieldInfo field)
        {
            Name = "Field: ";
            if (field.FieldType.IsGenericType)
            {
                Name += $"{PrettyName(field.FieldType)} {field.Name}";
            }
            else
            {
                Name += $"{field.FieldType.Name} {field.Name}";
            }
            
        }
    }
}
