using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Two.src.commands
{
    public class LogCommand : ICommand
    {
        /// <summary>
        /// Reads log/log.txt and returns it
        /// </summary>
        /// <returns>Program logs from log/log.txt</returns>
        public string Execute()
        {
            string logFilePath = ConfigurationManager.AppSettings["logFilePath"];
            if (File.Exists(logFilePath))
            {
                string result = File.ReadAllText(logFilePath);
                return result;
            }
            return "Logs are empty";
        }
    }
}
