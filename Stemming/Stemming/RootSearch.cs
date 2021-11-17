using System;
using System.Collections.Generic;
using System.Linq;

namespace Stemming
{
    public class RootSearch
    {
        private WordManipulations wm = new WordManipulations();
        private IEnumerable<string> perfectiveGerund = new[]
        {
            "в", "вши", "вшись",
            "ив", "ивши", 
            "ившись", "ыв", "ывши", "ывшись"
        };
        private IEnumerable<string> perfectiveGerundGroup1 = new[]
        {
            "в", "вши", "вшись"
        };
        private IEnumerable<string> adjective = new[]
        {
            "ее", "ие", "ые", "ое", "ими", 
            "ыми", "ей", "ий", "ый", "ой", 
            "ем", "им", "ым", "ом", "его", 
            "ого", "ему", "ому", "их", "ых", 
            "ую", "юю", "ая", "яя", "ою", "ею"
        };

        private IEnumerable<string> participle = new[]
        {
            "ем", "нн", "вш", "ющ", "щ",
            "ивш", "ывш", "ующ"
        };
        private IEnumerable<string> participleGroup1 = new[]
        {
            "ем", "нн", "вш", "ющ", "щ"
        };
        private IEnumerable<string> reflexive = new[] {"ся", "сь"};
        private IEnumerable<string> verb = new[]
        {
            "ла", "на", "ете", "йте", "ли",
            "й", "л", "ем", "н", "ло",
            "но", "ет", "ют", "ны", "ть",
            "ешь", "нно",
            "ила", "ыла", "ена",
            "ейте", "уйте", "ите", "или", "ыли",
            "ей", "уй", "ил", "ыл", "им",
            "ым", "ен", "ило", "ыло", "ено",
            "ят", "ует", "уют", "ит", "ыт",
            "ены", "ить", "ыть", "ишь", "ую",
            "ю"
        };
        private IEnumerable<string> verbGroup1 = new[]
        {
            "ла", "на", "ете", "йте", "ли",
            "й", "л", "ем", "н", "ло",
            "но", "ет", "ют", "ны", "ть",
            "ешь", "нно"
        };
        private IEnumerable<string> noun = new []
        {
            "а", "ев", "ов", "ие", "ье",
            "е", "иями", "ями", "ами", "еи",
            "ии", "и", "ией", "ей", "ой",
            "ий", "й", "иям", "ям", "ием",
            "ем", "ам", "ом", "о", "у",
            "ах", "иях", "ях", "ы", "ь",
            "ию", "ью", "ю", "ия", "ья",
            "я"
        };
        private IEnumerable<string> superlative = new []{"ейш", "ейше"};
        private IEnumerable<string> derivational = new []{"ост", "ость"};
        public RootSearch()
        {
            perfectiveGerund = perfectiveGerund.OrderByDescending(ending => ending.Length).ToArray();
            adjective = adjective.OrderByDescending(ending => ending.Length).ToArray();
            participle = participle.OrderByDescending(ending => ending.Length).ToArray();
            reflexive = reflexive.OrderByDescending(ending => ending.Length).ToArray();
            verb = verb.OrderByDescending(ending => ending.Length).ToArray();
            noun = noun.OrderByDescending(ending => ending.Length).ToArray();
            superlative = superlative.OrderByDescending(ending => ending.Length).ToArray();
            derivational = derivational.OrderByDescending(ending => ending.Length).ToArray();
        }
        public string FindRoot(string originalWord)
        {
            if (originalWord.Length == 1) return originalWord;
            var result = Step1(originalWord);
            result = Step2(result);
            result = Step3(result);
            return Step4(result);
        }

        private string Step1(string word)
        {
            var rv = wm.GetRV(word);
            var perfectiveGerundEnding = wm.FindEnding(rv, perfectiveGerund);
            if (!string.IsNullOrEmpty(perfectiveGerundEnding))
            {
                if (perfectiveGerundGroup1.Any(end => end == perfectiveGerundEnding))
                {
                    if (wm.IsGroup1VowelBeforeEnding(rv, perfectiveGerundEnding))
                        return wm.DeleteEnding(word, perfectiveGerundEnding);
                }
                else
                {
                    return wm.DeleteEnding(word, perfectiveGerundEnding);
                }
            }
            
            var reflexiveEnding = wm.FindEnding(rv, reflexive);
            if (!string.IsNullOrEmpty(reflexiveEnding))
            {
                word = wm.DeleteEnding(word, reflexiveEnding);
                rv = wm.GetRV(word);
            }
            
            var adjectiveEnding = wm.FindEnding(rv, adjective);
            if (!string.IsNullOrEmpty(adjectiveEnding))
            {
                word = wm.DeleteEnding(word, adjectiveEnding);
                rv = wm.GetRV(word);
                var participleEnding = wm.FindEnding(rv, participle);
                if (!string.IsNullOrEmpty(participleEnding))
                {
                    if (participleGroup1.Any(end => end == participleEnding))
                    {
                        if (wm.IsGroup1VowelBeforeEnding(rv, participleEnding))
                            return wm.DeleteEnding(word, participleEnding);
                    }
                    else
                    {
                        return wm.DeleteEnding(word, participleEnding);
                    }
                }
            }

            var verbEnding = wm.FindEnding(rv, verb);
            if (!string.IsNullOrEmpty(verbEnding))
            {
                if (verbGroup1.Any(end => end == verbEnding))
                {
                    if (wm.IsGroup1VowelBeforeEnding(rv, verbEnding))
                        return wm.DeleteEnding(word, verbEnding);
                }
                else
                {
                    return wm.DeleteEnding(word, verbEnding);
                }
            }

            var nounEnding = wm.FindEnding(rv, noun);
            if (!string.IsNullOrEmpty(nounEnding)) return wm.DeleteEnding(word, nounEnding);
            
            return word;
        }

        private string Step2(string word)
        {
            var rv = wm.GetRV(word);
            return rv.Last() == 'и' ? 
                word.Substring(0, word.Length - 1) : 
                word;
        }

        private string Step3(string word)
        {
            var r2 = wm.GetR2(word);
            var derivationalEnding = wm.FindEnding(r2, derivational);
            return !string.IsNullOrEmpty(derivationalEnding) ? 
                wm.DeleteEnding(word, derivationalEnding) : 
                word;
        }

        private string Step4(string word)
        {
            var rv = wm.GetRV(word);
            if (wm.IsNNLast(rv)) return word.Substring(0, word.Length - 1);
            var superlativeEnding = wm.FindEnding(rv, superlative);
            if (!string.IsNullOrEmpty(superlativeEnding))
            {
                word = wm.DeleteEnding(word, superlativeEnding);
                rv = wm.GetRV(word);
                return wm.IsNNLast(rv) ? 
                    word.Substring(0, word.Length - 1) : 
                    word;
            }
            return rv.Last().Equals('ь') ? 
                word.Substring(0, word.Length - 1) : 
                word;
        }
    }
}