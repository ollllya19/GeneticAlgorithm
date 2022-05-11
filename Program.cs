using System;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataHandler parser = new DataHandler("Data.txt");

            parser.ReadData();
        }
    }
}
