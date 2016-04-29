using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Brainfuck_Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("No input file specified");
                return;
            }

            var code = ReadFile(args[0]);

            if(code == null)
            {
                Console.WriteLine("File doesn't exist");
                return;
            }

            try
            {
                Interpreter.Interpret(code);
            } catch(Exception e)
            {
                Console.WriteLine("\n \nAn error occured.");
                Console.WriteLine(e.Message);
            }
        }

        private static string ReadFile(string path)
        {
            if(!File.Exists(path))
            {
                return null;
            } else {
                StreamReader sr = new StreamReader(path);
                var text = sr.ReadToEnd();
                text.Trim();
                sr.Close();
                return text;                              
            }                        
        }
    }
}
