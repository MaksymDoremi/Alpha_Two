using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha_Two.src.logic;

namespace Alpha_Two.src.commands
{
    public class DecompressCommand : ICommand
    {
        private Decompressor decompressor;

        public DecompressCommand(Decompressor decompressor)
        {
            this.decompressor = decompressor;
        }

        /// <summary>
        /// Executes decompression of the file
        /// </summary>
        /// <returns>DecompressText()</returns>
        public string Execute()
        {
            return DecompressText();
        }

        /// <summary>
        /// Uses UI to retrieve data from user and performs decompression of the input file
        /// </summary>
        /// <returns>Decompressed text</returns>
        public string DecompressText()
        {
            Console.Write("Type path to your input file: ");
            string input = Console.ReadLine();
            Console.Write("Type path to where you want to get the output file: ");
            string output = Console.ReadLine();
            try
            {

                if (File.Exists(input))
                {
                    return decompressor.decompress(input, output);
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

            return "Couldn't decompress your file";
        }
    }
}
