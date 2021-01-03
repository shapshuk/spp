using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class DateGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        public object Generate(GeneratorContext context)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(context.Random.Next(range));
        }
    }
}
