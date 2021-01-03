using FakerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public class StrGen : IGenerator
    {
        private readonly char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public object Generate(GeneratorContext context)
        {
            return RandomString(context.Random);
        }

        private string RandomString(Random rand)
        {
            string word = "";
            int numLetters = rand.Next(1, 10);
            for (int j = 1; j <= numLetters; j++)
            {
                int letterNum = rand.Next(0, letters.Length - 1);
                word += letters[letterNum];
            }
            return word;
        }
    }
}
