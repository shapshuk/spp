using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assembly_BrowserLib
{
     public abstract class BaseInfo
    {
        public string Name {get; protected set; }
        protected string PrettyName(Type type)
        {
            if (type.GetGenericArguments().Length == 0)
            {
                return type.Name;
            }
            var genericArguments = type.GetGenericArguments();
            var typeDefeninition = type.Name;
            var unmangledName = typeDefeninition.Substring(0, typeDefeninition.IndexOf("`"));
            return unmangledName + "<" + string.Join(",", genericArguments.Select(PrettyName)) + ">";
        }
    }
}
