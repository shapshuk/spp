using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjectionContainerLib
{
    public class DependenciesConfiguration
    {
        public Dictionary<Type, List<Implementation>> Dependensies = new Dictionary<Type, List<Implementation>>();

        private bool IsImpAbstract(Type TImplementation)
        {
            return TImplementation.IsAbstract;
        }
        private bool IsNotImplementDep(Type TDependency, Type TImplementation)
        {
            return (!TDependency.IsAssignableFrom(TImplementation) && !TDependency.IsGenericTypeDefinition);
        }
        private bool IsNotHavePublicConstructor(Type TImplementation)
        {
            return (!TImplementation.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any());
        }
        public void Register(Type TDependency, Type TImplementation, Implementation.LiveTimes liveTime = Implementation.LiveTimes.instPerDep)
        {
            if (IsImpAbstract(TImplementation) || IsNotImplementDep(TDependency, TImplementation) ||
                IsNotHavePublicConstructor(TImplementation))
            {
                throw new ArgumentException("Incorrect TImplementation");
            }
                
            if (!Dependensies.ContainsKey(TDependency))
            {
                Dependensies.Add(TDependency, new List<Implementation>());
            }
            

            if (Dependensies[TDependency].Contains(new Implementation(TImplementation, liveTime)))
                throw new ArgumentException("Such dependency is already registred");
            else
                Dependensies[TDependency].Add(new Implementation(TImplementation, liveTime));

        }
        public void Register<TDependency, TImplementation>(Implementation.LiveTimes liveTime = Implementation.LiveTimes.instPerDep)
        {
            Register(typeof(TDependency), typeof(TImplementation), liveTime);
        }
    }
}
