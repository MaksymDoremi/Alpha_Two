using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using static Alpha_Two.src.logic.Logger;

namespace Alpha_Two.src.logic
{
    public static class Logger
    {
        //private static readonly string? logPath = ConfigurationManager.AppSettings["logFilePath"];

        private static readonly object _lock = new();

        public delegate void OnLogMessage(string message);

        public static event OnLogMessage? OnLogEvent;

        /// <summary>
        /// Writes program logs to file specidied in App.config at "logFilePath"
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public static void WriteLog(string message, bool error)
        {
            lock (_lock)
            {
                string logMessage = string.Empty;
                if (!error)
                {
                    logMessage = $"LOG [{DateTime.Now}]: {message}\n";

                }
                else
                {
                    logMessage = $"ERROR [{DateTime.Now}]: {message}\n";
                }

                OnLogEvent?.Invoke(logMessage);

                File.AppendAllText(ConfigurationManager.AppSettings["logFilePath"], logMessage);

            }
        }
    }
}
