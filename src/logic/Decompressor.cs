using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Alpha_Two.src.logic
{
    public class Decompressor
    {
        private string dictionaryFilePath = ConfigurationManager.AppSettings["dictionaryFilePath"];
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        /// <summary>
        /// Reads from dictionary and initializes Dictionary<string, string>
        /// </summary>
        public void initDictionary()
        {
            string dict = File.ReadAllText(dictionaryFilePath);

            //string[] key_value = dict.Split(new[] { '=', '\n' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] key_value = Regex.Split(dict, @" = |\n");
            for (int i = 0; i < key_value.Length - 1; i += 2)
            {
                dictionary[key_value[i]] = key_value[i + 1];
            }
        }

        /// <summary>
        /// Performs decoding and decompression of the compressed text using the dictionary
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="outputPath"></param>
        /// <returns>Decompressed text</returns>
        public string decompress(string inputPath, string outputPath)
        {
            initDictionary();
            Logger.WriteLog("Reading compressed file", false);
            string input = File.ReadAllText(inputPath);
            Logger.WriteLog("Successfully read compressed file", false);
            string[] split_to_words = input.Split(new[] { ' ', '\n' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new();
            Logger.WriteLog("Decompressing file", false);
            foreach (string word in split_to_words)
            {
                try
                {
                    sb.Append(dictionary[word]);

                }
                catch (Exception e)
                {
                    sb.Append(word);
                }
                sb.Append(" ");

            }
            File.WriteAllText(outputPath, sb.ToString());
            Logger.WriteLog($"Successfully decompressed file and wrote to {outputPath}", false);
            return sb.ToString();
        }
    }
}