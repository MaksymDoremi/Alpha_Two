using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Two.src.commands
{
    public class HelpCommand : ICommand
    {
        /// <summary>
        /// Execute 'help' command
        /// </summary>
        /// <returns>
        /// Commands: <br/>
        /// help <br/>
        /// exit <br/>
        /// compress <br/>
        /// decompress <br/>
        /// log <br/>
        /// </returns>
        public string Execute()
        {
            return " - help - shows command you can use\n" +
                " - exit - exits the program\n" +
                " - compress - step into compression module\n" +
                " - decompress - step into decompression module\n" +
                " - log - prints all logs\n";
        }
    }
}
