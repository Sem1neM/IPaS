using System;
using System.Collections.Generic;
using System.Linq;

namespace Stemming
{
    public class BMHSearch
    {
        public static Result GetResult(string text, string substring)
        {
            if (text == null)
                throw new ArgumentNullException();
            var textLenght = text.Length;
            var subLength = substring.Length;
            var shiftTable = BuildShiftTable(substring);
            for (var i = 0; i <= textLenght - subLength;)
            {
                var j = subLength - 1;
                while (substring[j] == text[j + i])
                {
                    if (j == 0)
                        return new Result(true, i, i + subLength - 1);
                    j--;
                }
                i += shiftTable.ContainsKey(text[j + i]) ? shiftTable[text[j + i]] : shiftTable.Last().Value;
            }
            return new Result(false);
        }

        private static Dictionary<char, int> BuildShiftTable(string substring)
        {
            var result = new Dictionary<char, int>();
            var lastIndex = substring.Length - 1;
            for (var i = 0; i < lastIndex; i++)
            {
                result[substring[i]] = lastIndex - i;
            }
            if (!result.ContainsKey(substring[lastIndex]))
                result.Add('*', substring.Length);
            return result;
        }
    }
}