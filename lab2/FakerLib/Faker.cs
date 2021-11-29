using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// получить класс исходного объекта

// 1. получаем список конструкторов
// получаем первый конструктор, смотрим, какие у него параметры
// перечисляем параметры, задаем им исходные значения с помощью генераторов
// с помощью активатора создаем объект типа Т

// 2. получаем список свойств и полей
// заполняем свойства и поля объекта с помощью генераторов


// определить DTO / не DTO
// DTO - класс без методов

// реализовать генерацию интов, стрингов и т.д.
// реализовать генерацию одного из типов коллекций (List)
// отследить цикличные зависимости
// рекурсивное заполнение объектов


namespace FakerLib
{
    public class Faker
    {
        private readonly Stack<Type> types = new Stack<Type>();

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type type)
        {
            types.Push(type);

            if (Generator.CanGenerate(type))
            {
                return Generator.Generate(type);
            }

            var ctors = type.GetConstructors();
            if (ctors.Length == 0)
            {
                throw new Exception("Type has no public constructors");
            }
            
            var parameters = ctors.First().GetParameters()
                .Select(x => Generate(x.ParameterType));
            
            var @object = parameters.Any()
                ? Activator.CreateInstance(type, parameters)
                : Activator.CreateInstance(type);

            foreach (var propInfo in type.GetProperties())
            {
                propInfo.SetValue(@object, Generate(propInfo.PropertyType));
            }

            foreach (var fieldInfo in type.GetFields())
            {
                fieldInfo.SetValue(@object, Generate(fieldInfo.FieldType));
            }

            types.Pop();
            return @object;
        }

        private object Generate(Type type)
        {
            if (!Generator.CanGenerate(type))
            {
                if (!IsDto(type))
                {
                    return null;
                }

                if (types.Contains(type))
                {
                    throw new Exception("Cyclic dependency!!!!!!!!");
                }

                return Create(type);
            }
            
            return Generator.Generate(type);
        }

        private static bool IsDto(Type type)
        {
            var gettersCount = type.GetProperties().Count(x => x.GetMethod?.IsPublic == true);
            var settersCount = type.GetProperties().Count(x => x.SetMethod?.IsPublic == true);
            // DTO type mustn't have any methods except base Object's methods
            
            return type.GetMethods().Length - settersCount - gettersCount <= 4;
        }
    }
}