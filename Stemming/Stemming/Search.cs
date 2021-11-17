using System;
using System.Collections.Generic;
using System.Text;

namespace Stemming
{
    public class Search
    {
        private static string _text;
        public static List<string> Start(string text, string substring)
        {
            _text = text;
            substring = substring.ToLower();
            var result = new List<string>();
            while (true)
            {
                var finded = BMHSearch.GetResult(_text, substring);
                if (!finded.IsFound)
                {
                    break;
                }
                
                result.Add(GetFullWord(finded));
            }

            return result;
        }

        private static string GetFullWord(Result result)
        {
            var startIndex = result.StartIndex;
            var endIndex = result.EndIndex;
            while (startIndex != 0 && _text[startIndex - 1] != ' ')
                startIndex--;
            while (endIndex != _text.Length - 1 &&  _text[endIndex] != ' ')
                endIndex++;
            var resultString = new StringBuilder(_text.Substring(startIndex, endIndex - startIndex));
            _text = _text.Substring(endIndex, _text.Length - endIndex);
            return resultString.ToString();
        }
    }
}