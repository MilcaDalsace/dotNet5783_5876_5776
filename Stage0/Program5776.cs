using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Wellcome5776();
            Console.ReadKey();
        }

        static partial void wellcome5876();
        private static void Wellcome5776()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, Welcome to my first console application", userName);
        }
    }
}
