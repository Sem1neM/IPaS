using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Stemming;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        // private WordManipulations vm = new WordManipulations();
        // [TestCase("мама", "ма")]
        // [TestCase("маама", "ама")]
        // [TestCase("мам", "м")]
        // [TestCase("ма", "")]
        // [TestCase("ам", "м")]
        // [TestCase("еб", "б")]
        // [TestCase("м", "м")]
        // [TestCase("", "")]
        // public void RVTest(string search, string expected)
        // {
        //     var actual = vm.GetRV(search);
        //     Assert.AreEqual(expected, actual);
        // }
        //
        // [TestCase("мамочка", "ка")]
        // [TestCase("противоестественном", "оестественном")]
        // [TestCase("мама", "")]
        // [TestCase("балаклава", "лава")]
        // [TestCase("м", "")]
        // public void R2Test(string search, string expected)
        // {
        //     var actual = vm.GetR2(search);
        //     Assert.AreEqual(expected, actual);
        // }
        //
        // [TestCase("abcdefg", "fg", "abcde")]
        // [TestCase("abc","c","ab")]
        // [TestCase("ab","b","a")]
        // [TestCase("a","a","")]
        // [TestCase("a","","a")]
        // public void DeleteEndingTest(string word, string ending, string expected)
        // {
        //     var actual = vm.DeleteEnding(word, ending);
        //     Assert.AreEqual(expected, actual);
        // }
        //
        // [TestCase("таро", new[] {"ро", "о"}, "та", "ро")]
        // [TestCase("маковый", new[] {"ый", "й"}, "маков", "ый")]
        // [TestCase("величие", new[] {"ие", "е"}, "велич", "ие")]
        // [TestCase("величие", new[] {"ый", "й"}, "величие", "")]
        // [TestCase("маркер", new[] {"ый", "й"}, "маркер", "")]
        // public void FindEndingsTest(string word, string[] endings, string expectedWord, string expectedEnding)
        // {
        //     var ending = vm.FindEnding(word, endings);
        //     var actual = vm.DeleteEnding(word, ending);
        //     Assert.AreEqual(expectedEnding, ending);
        //     Assert.AreEqual(expectedWord, actual);
        // }
        //
        // [TestCase("маялась", "сь", true)]
        // [TestCase("ушла", "ла", false)]
        // [TestCase("", "ама", false)]
        // [TestCase("мама", "мама", false)]
        // public void IsGroup1VowelBeforeEndingTest(string word, string ending, bool expected)
        // {
        //     var actual = vm.IsGroup1VowelBeforeEnding(word, ending);
        //     Assert.AreEqual(expected, actual);
        // }
        //
        // [TestCase("сделав", "сдела")]
        // [TestCase("нанявшись", "наня")]
        // [TestCase("уделив", "удел")]
        // [TestCase("выпивши", "вып")]
        // [TestCase("бегавшая", "бега")]
        // [TestCase("утопившего", "утоп")]
        // [TestCase("видишь", "вид")]
        // [TestCase("наливайте", "налива")]
        // [TestCase("камнями", "камн")]
        // public void Step1Test(string word, string expected)
        // {
        //     var rt = new RootSearch();
        //     var actual = rt.Step1(word);
        //     Assert.AreEqual(expected, actual);
        // }
        //
        // [TestCase("камнями", "камням")]
        // [TestCase("рожок", "рожок")]
        // public void Step2Test(string word, string expected)
        // {
        //     var rt = new RootSearch();
        //     var actual = rt.Step2(word);
        //     Assert.AreEqual(expected, actual);
        // }
        //
        // [TestCase("молодость", "молод")]
        // [TestCase("наростит", "наростит")]
        // public void Step3Test(string word, string expected)
        // {
        //     var rt = new RootSearch();
        //     var actual = rt.Step3(word);
        //     Assert.AreEqual(expected, actual);
        // }
        //
        // [TestCase("сделанн", "сделан")]
        // [TestCase("ебейше", "еб")]
        // [TestCase("баннейше", "бан")]
        // [TestCase("лань", "лан")]
        // public void Step4Test(string word, string expected)
        // {
        //     var rt = new RootSearch();
        //     var actual = rt.Step4(word);
        //     Assert.AreEqual(expected, actual);
        // }

        // [Test]
        // public void StemmingTest()
        // {
        //     var data = ReadBigFile();
        //     var rt = new RootSearch();
        //     foreach (var (original, expected) in data)
        //     {
        //         var actual = rt.FindRoot(original);
        //         Assert.AreEqual(expected, actual);
        //     }
        // }
        
        private static IEnumerable<(string original, string expected)> ReadBigFile()
        {
            var sr = new StreamReader("bigData.txt");
            return sr.ReadToEnd()
                .Split('\n')
                .Select(str => str
                    .Substring(4, str.Length - 8))
                .Select(str => str.Replace(',', ' '))
                .Select(str => str.Split(new[] {'\"'}, StringSplitOptions.RemoveEmptyEntries))
                .Select(str => (str[0], str[2]));
        }
    }
}