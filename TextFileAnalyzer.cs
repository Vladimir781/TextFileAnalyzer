using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp
{
    public class TextFileAnalyzer
    {
        private string filePath;
        public Dictionary<string, int> wordCounts;
        public Dictionary<string, int> palindromeCounts;
        public List<string> palindromeSentences;

        public int TotalWordCount { get; private set; }
        public int UniqueWordCount { get; private set; }
        public int TotalPalindromeCount { get; private set; }

        public TextFileAnalyzer(string filePath)
        {
            this.filePath = filePath;
            this.wordCounts = new Dictionary<string, int>();
            this.palindromeCounts = new Dictionary<string, int>();
            this.palindromeSentences = new List<string>();
        }

        public void AnalyzeFile()
        {
            // Read the file and split it into sentences
            string text = File.ReadAllText(filePath);
            string[] sentences = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            // Reset the counts and lists
            TotalWordCount = 0;
            UniqueWordCount = 0;
            TotalPalindromeCount = 0;
            wordCounts.Clear();
            palindromeCounts.Clear();
            palindromeSentences.Clear();

            foreach (string sentence in sentences)
            {
                // Split the sentence into words
                string[] words = sentence.Split(new[] { ' ', '\t', '\n', '\r', ',', ';', '-', '—' }, StringSplitOptions.RemoveEmptyEntries);

                // Check if the sentence contains palindromes
                if (HasPalindrome(words))
                {
                    // Add the sentence to the palindrome sentences list
                    palindromeSentences.Add(sentence);
                }

                foreach (string word in words)
                {
                    // Increment the total word count
                    TotalWordCount++;

                    // Normalize the word to lowercase
                    string normalizedWord = word.ToLower();

                    // Count the word occurrences
                    if (wordCounts.ContainsKey(normalizedWord))
                        wordCounts[normalizedWord]++;
                    else
                    {
                        wordCounts[normalizedWord] = 1;
                        UniqueWordCount++;
                    }

                    // Check if the word is a palindrome
                    if (IsPalindrome(normalizedWord))
                    {
                        // Increment the palindrome count
                        TotalPalindromeCount++;

                        // Count the palindrome occurrences
                        if (palindromeCounts.ContainsKey(normalizedWord))
                            palindromeCounts[normalizedWord]++;
                        else
                            palindromeCounts[normalizedWord] = 1;
                    }
                }
            }
            UniqueWordCount--; // Subtract 1 from UniqueWordCount
            TotalWordCount--;  // Subtract 1 from TotalWordCount
        }








        public void WriteAnalysisResult(string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Total Word Count: " + TotalWordCount);
                writer.WriteLine("Unique Word Count: " + UniqueWordCount);
                writer.WriteLine("Total Palindrome Count: " + TotalPalindromeCount);
                writer.WriteLine();

                writer.WriteLine("Word Counts:");
                foreach (var entry in wordCounts)
                    writer.WriteLine(entry.Key + " - " + entry.Value);

                writer.WriteLine();

                writer.WriteLine("Palindrome Counts:");
                foreach (var entry in palindromeCounts)
                    writer.WriteLine(entry.Key + " - " + entry.Value);

                writer.WriteLine();

                writer.WriteLine("Palindrome Sentences:");
                foreach (string sentence in palindromeSentences)
                    writer.WriteLine(sentence);
            }
        }

        private bool IsPalindrome(string word)
        {
            if (word.Length <= 2)
                return false;

            int left = 0;
            int right = word.Length - 1;

            while (left < right)
            {
                if (word[left] != word[right])
                    return false;

                left++;
                right--;
            }

            return true;
        }

        private bool HasPalindrome(string[] words)
        {
            foreach (string word in words)
            {
                if (IsPalindrome(word.ToLower()))
                    return true;
            }

            return false;
        }
    }
}

