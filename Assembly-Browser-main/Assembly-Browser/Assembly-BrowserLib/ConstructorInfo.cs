using System;
using System.Collections.Generic;
using System.Text;

namespace Assembly_BrowserLib
{
    public class ConstructorInfo : BaseInfo
    {
        public void SetName(System.Reflection.ConstructorInfo constructor)
        {
            Name = $"Constructor: {constructor.DeclaringType.Name}(";
            foreach (var param in constructor.GetParameters())
            {
                if (param.ParameterType.IsGenericType)
                {
                    Name += $"{PrettyName(param.ParameterType)}";
                }
                else
                {
                    Name += param.Name;
                }
            }
            Name += ")";
        }
    }
}
