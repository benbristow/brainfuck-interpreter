using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainfuck_Interpreter
{
    static class Interpreter
    {      
        public static void Interpret(string code)
        {       
            //Sanitize code (remove comments etc.)     
            code = Sanitize(code);

            //Init. cells, pointer and current instruction index.
            byte[] cells = new byte[3000];
            byte pointer = 0;

            //Loop through each command
            for(int i = 0; i < code.Length; i++)
            {
                switch (code[i])
                {
                    //Increment data pointer
                    case '>':
                        pointer++;
                        break;
                    //Decrement data pointer
                    case '<':
                        pointer--;
                        break;
                    //Increment byte at data pointer
                    case '+':
                        cells[pointer]++;
                        break;
                    //Decrement byte at data pointer
                    case '-':
                        cells[pointer]--;
                        break;
                    //Output byte at data pointer
                    case '.':
                        byte[] arr = { cells[pointer] };
                        Console.Write(Encoding.ASCII.GetString(arr));
                        break;
                    //Accept one byte of input
                    case ',':
                        byte input = (byte)Console.Read();
                        cells[pointer] = input;
                        break;
                    //Jump past to matching ] if the cell under the pointer is 0
                    case '[':
                        if (cells[pointer] == (byte)0)
                        {
                            i = FindClosingBracket(code, i);
                        }
                        break;
                    //Jump back to matching ] if the cell under the pointer is not zero
                    case ']':
                        if(cells[pointer] != (byte)0)
                        {
                            i = FindOpenBracket(code, i) - 1;
                        }
                        break;
                    
                }
            }

            Console.WriteLine("\n \nFinished execution");
            Console.Read();
        }

        //https://stackoverflow.com/questions/12752225/how-do-i-find-the-position-of-matching-parentheses-or-braces-in-a-given-piece-of
        private static int FindClosingBracket(string code, int openPos)
        {
            int closePos = openPos;
            int counter = 1;
            while (counter > 0)
            {
                char c = code[++closePos];
                if (c == '[')
                {
                    counter++;
                }
                else if (c == ']')
                {
                    counter--;
                }
            }
            return closePos;
        }

        //https://stackoverflow.com/questions/12752225/how-do-i-find-the-position-of-matching-parentheses-or-braces-in-a-given-piece-of
        private static int FindOpenBracket(string code, int closePos)
        {
            int openPos = closePos;
            int counter = 1;
            while (counter > 0)
            {
                char c = code[--openPos];
                if (c == '[')
                {
                    counter--;
                }
                else if (c == ']')
                {
                    counter++;
                }
            }
            return openPos;
        }


        private static string Sanitize(string code)
        {
            String alphabet = "><+-.,[]";
            String newCode = String.Empty;
            foreach(char c in code)
            {               
                if(alphabet.Contains(c))
                {
                    newCode += c;
                }
            }
            return newCode;
        }
    }

}
