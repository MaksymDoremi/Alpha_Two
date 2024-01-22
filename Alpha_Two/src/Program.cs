using Alpha_Two.src.commands;
using Alpha_Two.src.logic;
using System.Text;

namespace Alpha_Two.src
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init objects
            Logger.OnLogEvent += Console.Write;
            Compressor compressor = new Compressor();
            Decompressor decompressor = new Decompressor();
            //Init commands
            Dictionary<string, ICommand> myCommands = new Dictionary<string, ICommand>
            {
                { "help", new HelpCommand() },
                { "compress", new CompressCommand(compressor)},
                { "decompress", new DecompressCommand(decompressor)},
                { "log", new LogCommand()}

            };

            Console.OutputEncoding = Encoding.UTF8;


            string command = string.Empty;

            //Start UI loop
            Console.WriteLine("Type \"help\" for help");
            Console.Write("Command> ");
            while (command != "exit")
            {
                
                command = Console.ReadLine();

                if (myCommands.ContainsKey(command.ToLower()))
                {
                    Console.WriteLine(myCommands[command.ToLower()].Execute());
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
                Console.Write("Command> ");
            }

            Console.WriteLine("SHUTDOWN");

        }
    }
}