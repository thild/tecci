using System;

namespace prog2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"O que você escrever será gravado no arquivo ""out.txt"":");
            Console.WriteLine(@"(Digite ""quit"" para sair)");
            string str = "";
            using (var sw = new System.IO.StreamWriter("out.txt"))
            {
                while (str != "quit")
                {
                    Console.Write("Digite: ");
                    str = Console.ReadLine();
                    sw.WriteLine(str);
                }
            }
        }
    }
}
