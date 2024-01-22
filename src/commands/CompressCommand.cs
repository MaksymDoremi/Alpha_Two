using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha_Two.src.logic;

namespace Alpha_Two.src.commands
{
    public class CompressCommand : ICommand
    {
        private Compressor compressor;
        public CompressCommand(Compressor compressor)
        {
            this.compressor = compressor;
        }

        /// <summary>
        /// Executes compression of the file
        /// </summary>
        /// <returns>CompressText()</returns>
        public string Execute()
        {
            return CompressText();
        }

        /// <summary>
        /// Uses UI to retrieve data from user and performs compression of the input file
        /// </summary>
        /// <returns>Compressed text</returns>
        public string CompressText()
        {
            Console.Write("Type path to your input file: ");
            string input = Console.ReadLine();
            Console.Write("Type path to where you want to get the output file: ");
            string output = Console.ReadLine();
            try
            {
                Console.Write("Type vowel threshold for compression: ");
                int vowels_count = Int32.Parse(Console.ReadLine());

                if (File.Exists(input))
                {
                    string readText = File.ReadAllText(input);
                    return compressor.CompressString(readText, output, vowels_count);
                }
                else
                {
                    Logger.WriteLog($"File {input} can't be found", true);
                }


            }
            catch (Exception e)
            {
                Logger.WriteLog("Exception occured = " + e.Message, true);
            }

            return "Couldn't compress your file";
        }
    }
}
