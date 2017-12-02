using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static readonly int inf = 999;
        static int[,] map;
        static int size, way;
        static void Main(string[] args)
        {
            Console.WriteLine("圖的最短路徑，使用Floyd-Warshall");
            Console.WriteLine("輸入節點數、連通數(空白分隔) EX:4 8");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            map = new int[size + 1, size + 1];
            for (int r = 1; r <= size; r++)
                for (int c = 1; c <= size; c++)
                    map[r, c] = (r == c ? 0 : inf);


            Console.WriteLine("輸入連通的兩點、路徑長(空白分隔)\nEX:1 2 2");
            int p1, p2, length;
            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);
                length = int.Parse(temp[2]);
                map[p1, p2] = length;
            }

            Console.WriteLine("圖形(0:無法自己到自己，大於1:可以通行，999:無法通行)，先直排在橫排(EX:1 2 = [1,2])");
            for (int r = 1; r <= size; r++)
            {
                for (int c = 1; c <= size; c++)
                {
                    Console.Write(map[r, c] + "\t");
                }
                Console.Write("\n");
            }

            for (int k = 1; k <= size; k++)
                for (int i = 1; i <= size; i++)
                    for (int j = 1; j <= size; j++)
                        if (map[i, j] > map[i, k] + map[k, j])
                            map[i, j] = map[i, k] + map[k, j];
            Console.WriteLine("最短路徑表");
            for (int r = 1; r <= size; r++)
            {
                for (int c = 1; c <= size; c++)
                {
                    Console.Write(map[r, c] + "\t");
                }
                Console.Write("\n");
            }
            Console.ReadKey();
        }
    }
}
