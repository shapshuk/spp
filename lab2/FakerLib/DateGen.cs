using System;

namespace FakerLib
{
    public class DateGen : IGenerator
    {
        private readonly Random _random = new Random();
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        public object Generate(Type type)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}