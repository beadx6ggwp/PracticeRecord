using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        /*
        注意
        此程式陣列從1開始             
        */
        static readonly int inf = 99;
        static int[,] map;
        static int[] dist;
        static bool[] viste;
        static int size, way, start;
        static void Main(string[] args)
        {
            Console.WriteLine("圖的最小生成樹 Prim (Dijkstra版)");
            Console.WriteLine("輸入節點數、連通數(空白分隔) EX:6 9");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            start = 1;

            map = new int[size + 1, size + 1];
            for (int r = 1; r <= size; r++)
                for (int c = 1; c <= size; c++)
                    map[r, c] = (r == c ? 0 : inf);

            Console.WriteLine("輸入連通的兩點、長度(空白分隔)\nEX:2 4 11");
            int p1, p2, length;
            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);
                length = int.Parse(temp[2]);
                map[p1, p2] = length;
                map[p2, p1] = length;
                // 此為無向圖
            }

            Console.WriteLine("圖形預覽(0:無法自己到自己，大於1:可以通行，999:無法通行)，先直排在橫排(EX:1 2 = [1,2])");
            for (int r = 1; r <= size; r++)
            {
                for (int c = 1; c <= size; c++)
                {
                    Console.Write(map[r, c] + "\t");
                }
                Console.Write("\n");
            }

            // start--------------------------------------------------------------------------------------------

            dist = new int[size + 1];
            viste = new bool[size + 1];
            int count = 0, sum = 0,
                min, near = 0, next = 0,
                last = 0;
            //初始化起點各點距離
            for (int i = 1; i <= size; i++) dist[i] = map[start, i];

            viste[start] = true; // 假設節點1開始
            count++; // 重要:當加入1號點時代表只需搜索size-2條邊
            last = start;

            
            while (count < size)// count <= size -1 每兩個節點之間只有一條線
            {
                min = inf;
                for (int i = 1; i <= size; i++)
                {
                    // 找到較短的節點
                    if (dist[i] < min && !viste[i])
                    {
                        min = dist[i];
                        near = i;
                    }
                }
                viste[near] = true;

                sum += dist[near];
                count++;

                for (next = 1; next <= size; next++)
                {
                     if (map[near, next] < dist[next] && !viste[next])
                        dist[next] = map[near, next];
                }
            }

            Console.WriteLine($"最小成本:{sum}");

            Console.Read();
        }
    }
}
