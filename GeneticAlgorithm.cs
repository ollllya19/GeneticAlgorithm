using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp1
{
    class GeneticAlgorithm
    {
        public int Perform(int n, int w, int[] wList)
        {
            Iteration iter;
            int min = n * 2;
            for (int i = 0; i < 1000; i++)
            {
                iter = new Iteration(10, n, w, wList);
                if (iter.MinContCol < min)
                    min = iter.MinContCol;
            }
            return min;
        }
    }

    class Iteration
    {
        protected int popCol;
        protected List<int[]> population;

        protected int n;
        protected int[] wList;

        protected int w;

        protected int minConCol;

        public Iteration(int popCol, int n, int w, int[] wList)
        {
            this.popCol = popCol;
            this.n = n;
            this.w = w;
            this.wList = wList;
            population = new List<int[]>();
            minConCol = n * 2;

            FirstPopulation();
            int[] newS = CrossS();
            newS = Mutate(newS);
            newS = LocalImprove(newS);
            UpdatePopoullation(newS);
        }

        public int MinContCol
        {
            get => minConCol;
        }

        protected void FirstPopulation()
        {
            Random rd = new Random();
            int l = rd.Next(0, n);
            int[] s = new int[n];

            for (int i = 0; i < n; i++)
            {
                s[i] = i;
            }
            s = SortByW(s);
            population.Add(s);

            for (int i = 1; i < popCol; i++)
            {
                int[] list = new int[n];
                s.CopyTo(list, 0);
                for (int j = 0; j < n; j++)
                {
                    int index1 = rd.Next(0, n);
                    int index2 = rd.Next(0, n);
                    int tmp = list[index1];
                    list[index1] = list[index2];
                    list[index2] = tmp;
                }
                population.Add(list);
            }
        }

        protected int[] SortByW(int[] s)
        {
            int temp;
            for (int i = 0; i < s.Length - 1; i++)
            {
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (wList[s[i]] <= wList[s[j]])
                    {
                        temp = s[i];
                        s[i] = s[j];
                        s[j] = temp;
                    }
                }
            }
            return s;
        }

        protected int[] CrossS()
        {
            int[] s1 = MinRecord();
            int[] s2 = GetRandArray(s1);
            return Cross(s1, s2);
        }

        protected int[] Mutate(int[] s)
        {
            Random rn = new Random();
            for (int i = 0; i < n; i++)
            {
                int q = rn.Next();
                if (q < 1.0 / n)
                {
                    int j = rn.Next(0, n);
                    int temp = s[i];
                    s[i] = s[j];
                    s[j] = temp;
                }
            }
            return s;
        }

        protected void UpdatePopoullation(int[] newS)
        {
            int maxRectord = 0;
            int index = 0;
            int temp;
            population.Add(newS);
            for (int i = 0; i < popCol + 1; i++)
            {
                temp = CountRods(population[i]);
                if (temp > maxRectord)
                    index = i;
            }
            population.RemoveAt(index);

            int tempCol = CountRods(newS);
            if (tempCol < minConCol)
                minConCol = tempCol;
        }

        protected int[] GetRandArray(int[] s1)
        {
            Random rn = new Random();
            int[] s2;
            while (true)
            {
                int val = rn.Next(0, popCol);
                s2 = population[val];
                if (s1 != s2)
                    break;
            }
            return s2;
        }

        protected int[] MinRecord()
        {
            int minIndex = 0;
            for (int i = 0; i < popCol; i++)
            {
                int curr = CountRods(population[i]);
                if (curr < minConCol)
                {
                    minConCol = curr;
                    minIndex = i;
                }
            }
            return population[minIndex];
        }

        protected int CountRods(int[] s)
        {
            int contCol = 1;
            int ost = w;
            for (int i = 0; i < s.Length; i++)
            {
                int wi = wList[s[i]];
                if (wi > ost)
                {
                    contCol++;
                    ost = w;
                }
                ost -= wi;
            }

            return contCol;
        }

        protected int[] Cross(int[] s1, int[] s2)
        {
            List<int> temp = new List<int>();
            Random rd = new Random();
            int l = rd.Next(1, n);
            int[] newX = new int[n];
            for (int i = 0; i < l; i++)
            {
                newX[i] = s1[i];
                temp.Add(newX[i]);
            }
            int k = l;
            for (int i = 0; i < n; i++)
            {
                if (!temp.Contains(s2[i]))
                {
                    newX[k] = s2[i];
                    k++;
                }
            }
            return newX;
        }

        protected int[] LocalImprove(int[] s)
        {
            int[] minS = s;
            for (int i = 0; i < s.Length; ++i)
            {
                int aLast = s[n - 1];
                for (int j = n - 1; j > 0; j--)
                    s[j] = s[j - 1];
                s[0] = aLast;

                int minR = CountRods(s);
                if (minR < minConCol)
                {
                    minConCol = minR;
                    minS = s;
                }
            }

            return minS;
        }
    }
}
