using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Assembly_BrowserLib
{
    public class MethodInfo : BaseInfo
    {    
        public void SetName(System.Reflection.MethodInfo method)
        {
            Name = "Method: ";
            if (method.ReturnType.IsGenericType)
            {
                Name += $"{PrettyName(method.ReturnType)} {method.Name}(";
            }
            else
            {
                Name += $"{method.ReturnType.Name} {method.Name}(";
            }

            foreach (var param in method.GetParameters())
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
