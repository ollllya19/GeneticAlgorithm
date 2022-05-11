using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp1
{
    class DataHandler
    {
        int w;
        int itemCol;
        int expectedValue;
        int[] wList;

        string path;

        public DataHandler(string path)
        {
            this.path = path;
        }

        public void ReadData()
        {
            
            StreamReader reader = new StreamReader(path);
            itemCol = Convert.ToInt32(reader.ReadLine());
            for (int i = 0; i < itemCol; i++)
            {
                reader.ReadLine();
                string[] str = reader.ReadLine().Split(' ');
                w = Convert.ToInt32(str[1]);
                itemCol = Convert.ToInt32(str[2]);
                expectedValue = Convert.ToInt32(str[3]);

                wList = new int[itemCol];
                for (int j = 0; j < itemCol; j++)
                {
                    wList[j] = Convert.ToInt32(reader.ReadLine());
                }

                Output();
            }
            reader.Close();
        }

        private void Output()
        {
            GeneticAlgorithm obj = new GeneticAlgorithm();
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Полученное решение: {obj.Perform(itemCol, w, wList)}");
            Console.WriteLine($"Ожидаемое решение: {expectedValue}");
        }
    }
}
