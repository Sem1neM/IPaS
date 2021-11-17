using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Stemming
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var data = "";
            bool file = true;
            while (file)
            {
                Console.WriteLine("Введите имя файла:");
                var fileName = Console.ReadLine();
                if (File.Exists($"{fileName}.txt"))
                {
                    data = GetTextFromFile(fileName);
                    Console.WriteLine("Файл успешно загружен");
                    Console.ReadKey();
                    Console.Clear();
                    file = false;
                }
                else
                {
                    Console.WriteLine("Файл не найден");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            bool exit  = true;
            var word = "";
            while (exit)
            {
                Console.WriteLine("Введите слово для поиска"); 
                word = Console.ReadLine();
                if (word.Equals("") || Regex.IsMatch(word, "\\d")) 
                {
                    Console.WriteLine("Неверный ввод!");
                    Console.ReadKey();
                    Console.Clear();
                }
                
                else
                {
                    exit = false;
                }
            }

            var rt = new RootSearch();
                var root = rt.FindRoot(word);
                var finded = Search.Start(data, root);
                if (finded.Count == 0) Console.WriteLine("Не удалось найти однокоренных слов");
                else
                {
                    var flag = true;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"Искомое слово - {word}");
                        Console.WriteLine($"Корень слова - {root}");
                        Console.WriteLine($"Найдено {finded.Count} слов");
                        Console.WriteLine("1. Показать");
                        Console.WriteLine("2. Сохранить в файл");
                        Console.WriteLine("ESC. Выход");
                        var choice = Console.ReadKey().Key;
                        switch (choice)
                        {
                            case ConsoleKey.D1:
                            {
                                Console.Clear();
                                for (var i = 0; i < finded.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {finded[i]}");
                                }

                                Console.WriteLine("\nНажмите любую кнопку для возврата в меню...");
                                Console.ReadKey();

                                break;
                            }
                            case ConsoleKey.D2:
                            {
                                Console.Clear();
                                var sw = new StreamWriter("result.txt");
                                sw.WriteLine($"Искомое слово - {word}");
                                sw.WriteLine($"Корень - {root}");
                                sw.WriteLine($"Найдено {finded.Count} слов\n");
                                for (var i = 0; i < finded.Count; i++)
                                {
                                    sw.WriteLine($"{i + 1}. {finded[i]}");
                                }

                                sw.Close();
                                Console.WriteLine("Найденные одкоренные слова сохранены в файл result.txt");
                                Console.ReadKey();

                                break;
                            }
                            case ConsoleKey.Escape:
                            {
                                flag = false;
                                break;
                            }
                        }
                    } while (flag);
                }
            }


        private static string GetTextFromFile(string fileName)
        {
            var sr = new StreamReader($"{fileName}.txt"); 
            return sr.ReadToEnd();
        }
    }
}