using System;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter title: ");
            var title = Console.ReadLine();
            Console.WriteLine(title);
            Console.ReadKey();
        }
    }
}