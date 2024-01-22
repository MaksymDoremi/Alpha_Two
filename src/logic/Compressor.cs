using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Two.src.logic
{
    public class Compressor
    {
        private string vowels = "aeiou";

        HashSet<string> keywords = new HashSet<string>();

        private List<char> signs = new List<char>();

        private Dictionary<string, char> dictionary = new Dictionary<string, char>();

        private string dictionaryPath = ConfigurationManager.AppSettings["dictionaryFilePath"];

        

        /// <summary>
        /// Checks if char is vowel
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Whether or not char is vowel letter</returns>
        public bool IsVowel(char c)
        {
            return vowels.Contains(c.ToString().ToLower());
        }

        /// <summary>
        /// Counts vowels in signle word
        /// </summary>
        /// <param name="word"></param>
        /// <returns>Vowel count</returns>
        public int vowelCount(string word)
        {
            int i = 0;
            foreach (char c in word)
            {
                if (vowels.Contains(c.ToString().ToLower())) { i++; }
            }

            return i;
        }

        /// <summary>
        /// Calculates word density in the whole text <br/>
        /// Create dictionary with word:count
        /// </summary>
        /// <param name="input"></param>
        /// <param name="keywords"></param>
        /// <returns>Dictionary with word:count</returns>
        public Dictionary<string, int> CalculateKeywordDensity(string input, HashSet<string> keywords)
        {
            // Split the input string into words
            string[] words = input.Split(new[] { ' ', '.', ',', ';', ':', '!', '?', '\n' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            // add each word to keyword set
            foreach (string w in words)
            {
                keywords.Add(w.ToLower());
            }

            // Count occurrences of each keyword
            Dictionary<string, int> keywordCounts = new Dictionary<string, int>();
            foreach (string keyword in keywords)
            {
                int count = words.Count(w => string.Equals(w, keyword, StringComparison.OrdinalIgnoreCase));
                keywordCounts[keyword] = count;
            }

            return keywordCounts;
        }

        /// <summary>
        /// Initialize ASCII signs, which program can use for coding words
        /// </summary>
        public void InitSigns()
        {
            //string result = "";
            int i = 1;
            // 33-126, 161-255
            for (int asciiValue = 33; asciiValue <= 255; asciiValue++)
            {
                if (asciiValue == 173)
                {
                    asciiValue = 174;
                }
                signs.Add(Convert.ToChar(asciiValue));
                i++;
                if (asciiValue == 126)
                {
                    asciiValue = 160;
                }
            }

        }
        /// <summary>
        /// Performs compression of the input text and writing to output file
        /// </summary>
        /// <param name="input"></param>
        /// <param name="outputPath"></param>
        /// <param name="vowels_count"></param>
        /// <returns>Compressed text</returns>
        public string CompressString(string input, string outputPath, int vowels_count)
        {
            // init signs, create all ASCII signs the program will use
            Logger.WriteLog("Initializing coding signs", false);
            InitSigns();
            Logger.WriteLog("Initializing dictionary", false);

            // keyword density => ascii symbols => dictionary
            // we get "word":count
            Dictionary<string, int> keywordCounts = CalculateKeywordDensity(input, keywords);
            //  words that are not marked ass ascii => aplly rules down below

            // define dicitonary for the input
            var newDict = keywordCounts.OrderByDescending(entry => entry.Value).ThenBy(entry => entry.Key);
            int i = 0;
            StringBuilder sb = new();
            foreach (KeyValuePair<string, int> entry in newDict)
            {

                if (i < signs.Count)
                {
                    sb.Append($"{signs[i]} = {entry.Key}\n");
                    dictionary[entry.Key] = signs[i];
                    i++;
                }
                else
                {
                    break;
                }

            }
            File.WriteAllText(dictionaryPath, sb.ToString());
            Logger.WriteLog($"Successfully initialized dictionary and wrote to {dictionaryPath}", false);

            // start to compress the file using the dictionary,
            // if word is not in the dictionary
            // then rules for vowel removal are applied
            string[] split_to_words = input.Split(new[] { ' ', '.', ',', ';', ':', '!', '?', '\n' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            sb.Clear();

            foreach (string word in split_to_words)
            {

                try
                {
                    sb.Append(dictionary[word.ToLower()]);
                }
                catch (Exception e)
                {
                    if (vowelCount(word) >= vowels_count)
                    {
                        foreach (char c in word)
                        {
                            if (!IsVowel(c))
                            {
                                sb.Append(c);
                            }
                        }
                    }
                    else
                    {
                        sb.Append(word);
                    }
                }
                sb.Append(" ");


            }
            File.WriteAllText(@outputPath, sb.ToString());
            Logger.WriteLog($"Successfully compressed file to {outputPath}", false);
            Logger.WriteLog($"Config => dictionary inside {dictionaryPath}, output inside {outputPath}, vowels removed at threshold {vowels_count}", false);
            return sb.ToString();

        }
    }
}
