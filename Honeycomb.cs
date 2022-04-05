using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace honeycomb
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Bad use of honeycomb.\n\n");
                HelpScreen();
                Console.ReadKey();
            }
            else if (args[0].Contains("-h"))
            {
                HelpScreen();
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"[+] Starting on {args[0]}!\n\n");
                StartHoneycomb(args[0]);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("\n\nPress any key to exit...");
                Console.ReadKey();
            }
        }

        public static void StartHoneycomb(string file)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            ModuleDefMD module = ModuleDefMD.Load(file);
            foreach (TypeDef type in module.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (method.Body == null)
                        continue;
                    else
                    {
                        string functionName = method.Name;
                        Console.WriteLine($"[+] Honeying the {functionName} function...");
                        int randomNumber = new Random().Next(35564, 100000);
                        for (int i = 0; i < randomNumber; i++)
                        {
                            method.Body.Instructions.Insert(0, OpCodes.Nop.ToInstruction());
                        }
                        Console.WriteLine($"[+] {functionName.ToUpper()} HAS BEEN HONEYED!");
                    }
                }
            }
            module.Write($"honeyed_{module.Name}.exe");
            Console.WriteLine($"[+] Honeycomb has completed successfully. Written binary to honeyed_{module.Name}.exe!");
            System.Threading.Thread.Sleep(5000);
        }

        public static void HelpScreen()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[Usage:>]\nhoneycomb [path]    <--  ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("flood .net executable with nop\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("honeycomb -h/--help <--  ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("prints this msg\n\n");
        }
    }
}
