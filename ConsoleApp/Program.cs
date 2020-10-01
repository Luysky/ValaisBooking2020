using System;
using System.Data.SqlClient;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            String a = "=";
            String b = "TRUE";
            String c = $"{a}{b}";

            Console.WriteLine(c);

        }
    }
}
