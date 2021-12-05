using System;
using System.Collections.Generic;
using System.Text;

namespace Assembly_BrowserLib
{
    public class PropertyInfo : BaseInfo
    {
        public void SetName(System.Reflection.PropertyInfo property)
        {
            Name = "Property: ";
            if (property.PropertyType.IsGenericType)
            {
                Name += $"{PrettyName(property.PropertyType)} {property.Name}";
            }
            else
            {
                Name += $"{property.PropertyType.Name} {property.Name}";
            }
        }
    }
}
