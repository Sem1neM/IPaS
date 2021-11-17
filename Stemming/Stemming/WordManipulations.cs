using System;
using System.Collections.Generic;
using System.Linq;

namespace Stemming
{
    public class WordManipulations
    {
        private readonly char[] Vowels = {'а', 'е', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я'};
        public string GetRV(string word)
        {
            word = word.Replace("ё", "е");
            for (var i = 0; i < word.Length; i++)
            {
                if (Vowels.Any(c => c == word[i]))
                {
                    return i == word.Length - 1 ? "-1" : word.Substring(i + 1);
                }
            }

            return word;
        }

        private string GetR1(string word)
        {
            for (var i = 0; i < word.Length - 1; i++)
            {
                if (Vowels.Any(c => c == word[i]))
                {
                    if (Vowels.Any(c => c != word[i + 1]))
                    {
                        return word.Substring(i + 2);
                    }
                }
            }

            return "";
        }

        public string GetR2(string word)
        {
            var r1 = GetR1(word);
            for (var i = 0; i < r1.Length - 1; i++)
            {
                if (Vowels.Any(c => c == r1[i]))
                {
                    if (Vowels.Any(c => c != r1[i + 1]))
                    {
                        return r1.Substring(i + 2);
                    }
                }
            }

            return "";
        }
        
        public string FindEnding(string word, IEnumerable<string> endings)
        {
            foreach (var ending in endings)
            {
                if (word.Length >= ending.Length && word.Substring(word.Length - ending.Length) == ending)
                    return ending;
            }

            return "";
        }
        
        public string DeleteEnding(string word, string ending)
        {
            return word.Substring(0, word.Length - ending.Length);
        }

        public bool IsGroup1VowelBeforeEnding(string word, string ending)
        {
            var group1Vowels = new[] {'а', 'я'};
            return word.Length > ending.Length && group1Vowels.Any(vowel => vowel == word[word.Length - ending.Length - 1]);
        }

        public bool IsNNLast(string word)
        {
            if (word.Length < 2) return false;
            return word.Substring(word.Length - 2).Equals("нн");
        }
    }
}