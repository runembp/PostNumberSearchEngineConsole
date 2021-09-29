#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PostNumberSearchEngineConsole
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Welcome to this Post Code -> City search engine");
            Console.Write("Please enter a Post Code: ");
            var foundCity  = SearchResult(Console.ReadLine());
            Console.WriteLine($"Result: : {foundCity ?? "Nothing"}");
        }

        private static string? SearchResult(string? userInput)
        {
            var isCorrectPostCode = int.TryParse(userInput,out _);
            
            if (string.IsNullOrWhiteSpace(userInput) || isCorrectPostCode == false)
            {
                return "Nothing";     
            }
            
            var data = PopulateDictionaryFromCsvFile();
            return data.Where(x => x.Key.Equals(userInput)).Select(x => x.Value).FirstOrDefault();
        }

        private static Dictionary<string, string> PopulateDictionaryFromCsvFile()
        {
            var dictionary = new Dictionary<string, string>();
            var csvData = File.ReadAllLines("../../../postnumre.csv");

            for (var i = 1; i < csvData.Length; i++)
            {
                var splitData = csvData[i].Split(";");
                if (string.IsNullOrEmpty(splitData[0]) || string.IsNullOrEmpty(splitData[1]))
                {
                    break;
                }
                dictionary.Add(splitData[0],splitData[1]);
            }

            return dictionary;
        }
    }
}