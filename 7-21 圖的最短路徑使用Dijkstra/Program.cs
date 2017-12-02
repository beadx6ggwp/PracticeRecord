using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        /*
        注意
        此程式陣列從1開始             
        */
        static readonly int inf = 99;
        static int[,] map;
        static int size, way;
        static void Main(string[] args)
        {
            Console.WriteLine("圖的最短路徑，使用Floyd-Warshall，起點預設為1號點，終點為最後一個");
            Console.WriteLine("輸入節點數、連通數(空白分隔) EX:6 9");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            map = new int[size + 1, size + 1];
            for (int r = 1; r <= size; r++)
                for (int c = 1; c <= size; c++)
                    map[r, c] = (r == c ? 0 : inf);


            Console.WriteLine("輸入連通的兩點、路徑長(空白分隔)，不可大於99\nEX:1 2 2");
            int p1, p2, length;
            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);
                length = int.Parse(temp[2]);
                map[p1, p2] = length;
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
            //--------------------------------------------------------------------------
            int[] dis = new int[size + 1];// 儲存起點到各個點的距離
            int[] book = new int[size + 1];
            int min = inf, last = 1, u = 0;// 記錄離當前節點最近的節點編號

            // 這裡以1號點為起點
            for (int i = 1; i <= size; i++)
                dis[i] = map[1, i];

            book[1] = 1;

            // 因為是以起點周圍開始擴展，所以不用包含起點，所以size-1
            for (int i = 1; i <= size - 1; i++)
            {
                min = inf;
                for (int j = 1; j <= size; j++)
                {
                    if (book[j] == 0 && dis[j] < min)
                    {
                        min = dis[j];
                        u = j;
                    }
                }
                book[u] = 1;

                Console.WriteLine($"節點{last}的最近點{u}\t以節點{u}探索");

                last = u;

                // 探索u點周圍的點，並更新起點到v點的距離
                for (int v = 1; v <= size; v++)
                {
                    int l = Math.Min(dis[u] + map[u, v], dis[v]);
                    Console.WriteLine($"{u}->{v}的距離: {map[u, v]} \t 1->{v} 的距離: {dis[v]} \t 更新後: {l}");
                    if (dis[v] > dis[u] + map[u, v])
                    {
                        dis[v] = dis[u] + map[u, v];
                    }
                }

                Console.WriteLine("-------");
            }

            Console.WriteLine("以1號當起點到各個節點的最短距離");
            for (int i = 1; i <= size; i++)
                Console.WriteLine($"1-{i}:{dis[i]} ");

            Console.ReadKey();
        }
    }
}
