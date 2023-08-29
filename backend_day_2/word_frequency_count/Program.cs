using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class WordFrequencyCounter
{
    public static Dictionary<string, int> CountWordFrequencies(string input)
    {
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        
        string[] words = Regex.Split(input, @"\W+");
        
        foreach (string word in words)
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                if (wordFrequency.ContainsKey(word))
                {
                    wordFrequency[word]++;
                }
                else
                {
                    wordFrequency[word] = 1;
                }
            }
        }

        return wordFrequency;
    }

    public static void Main()
    {
        Console.WriteLine("Enter a string to count its words: ");
        string input = Console.ReadLine();
        Dictionary<string, int> frequencyDict = CountWordFrequencies(input);

        foreach (var pair in frequencyDict)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
}
