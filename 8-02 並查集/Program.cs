using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static int[] father;
        static int peoples, relations;
        static void Main(string[] args)
        {
            Console.WriteLine("並查集練習");
            Console.WriteLine("請輸入人數與關連數EX:10 9");
            string[] temp = Console.ReadLine().Split(' ');
            peoples = int.Parse(temp[0]);
            relations = int.Parse(temp[1]);

            father = new int[peoples + 1];
            for (int i = 1; i <= peoples; i++)
                father[i] = i;


            int p1, p2;
            for (int i = 1; i <= relations; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);

                merge(p1, p2);
            }

            int sum = 0;
            for (int i = 1; i <= peoples; i++)
                if (father[i] == i) sum++;

            Console.WriteLine($"總集合數{sum}");

            Console.Read();
        }

        static void merge(int p1, int p2)// 將兩組集合合併
        {
            int t1 = getFather(p1),
                t2 = getFather(p2);

            // 如果他們兩個不是同集合，將t2歸到t1底下
            if (t1 != t2)
                father[t2] = t1;
        }

        static int getFather(int p) // 回朔該點的根結點
        {
            if (father[p] == p) return p;
            else return getFather(father[p]);
        }
    }
}
