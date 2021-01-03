using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DependencyInjectionContainerLib
{
    public class DependencyProvider
    {
        private Implementation implementation;
        private Dictionary<Type, List<Implementation>> _dependensies { get; }

        private ConcurrentDictionary<Type, object> _instances { get; } = new ConcurrentDictionary<Type, object>();

        public DependencyProvider(DependenciesConfiguration dependenciesConfiguration)
        {
            _dependensies = dependenciesConfiguration.Dependensies;
        }
        private Array CreateIEnumerableContainer(Type TDependency)
        {
            var actual = TDependency.GetGenericArguments().First();
            int implementationlCount = _dependensies[actual].Count;

            var container = Array.CreateInstance(actual, implementationlCount);

            for (int i = 0; i < implementationlCount; i++)
            {
                container.SetValue(Resolve(actual, i), i);
            }
            return container;
        }

        private Type CreateGenericType(Type TDependency, int numOfImplentation)
        {
            var t = TDependency.GetGenericTypeDefinition();
            if (_dependensies.ContainsKey(t))
            {
                implementation = _dependensies[t].First();
                var targetType = implementation.TImplementation;
                return targetType.MakeGenericType(TDependency.GetGenericArguments().First());
            }
            else
            {
                implementation = _dependensies[TDependency][numOfImplentation];
                return implementation.TImplementation;
            }
        }
        private object CreateInstanse(Type targetType)
        {
            var constructor = targetType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).First();
            var parametrs = constructor.GetParameters();

            var constrParams = new List<object>();

            foreach (var parameter in parametrs)
            {
                if (parameter.ParameterType.IsValueType)
                {
                    constrParams.Add(Activator.CreateInstance(parameter.ParameterType));
                }
                else
                {
                    constrParams.Add(Resolve(parameter.ParameterType));
                }
            }

            try
            {
                object result = constructor.Invoke(constrParams.ToArray());

                if (implementation.LiveTime == Implementation.LiveTimes.singleton)
                {
                    return _instances.TryAdd(targetType, result) ? result : _instances[targetType];
                }
                return result;
            }
            catch (Exception)
            {
                throw new ArgumentException($"{targetType.Name} constructor threw an exception");
            }
        }
        public object Resolve(Type TDependency, int numOfImplentation = 0)
        {
            if (typeof(IEnumerable).IsAssignableFrom(TDependency))
            {
                return CreateIEnumerableContainer(TDependency);
            }

            Type targetType;
            if (TDependency.GetGenericArguments().Length != 0)
            {
                targetType = CreateGenericType(TDependency, numOfImplentation);
            }
            else
            {
                implementation = _dependensies[TDependency][numOfImplentation];
                targetType = implementation.TImplementation;
            }

            return CreateInstanse(targetType);
        }
        public object Resolve<TDependency>(int numOfImplentation = 0)
        {
            return Resolve(typeof(TDependency), numOfImplentation);
        }
    }
}
