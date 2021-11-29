using System;

namespace FakerLib
{
    public class StrGen : IGenerator
    {
        private readonly char[] _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly Random _random = new Random();
        
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public object Generate(Type type)
        {
            return RandomString(_random);
        }

        private string RandomString(Random rand)
        {
            string word = "";
            int numLetters = rand.Next(1, 10);
            for (int j = 1; j <= numLetters; j++)
            {
                int letterNum = rand.Next(0, _letters.Length - 1);
                word += _letters[letterNum];
            }
            return word;
        }
    }
}