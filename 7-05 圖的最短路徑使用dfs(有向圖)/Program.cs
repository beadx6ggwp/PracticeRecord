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
         * 注意:此專案陣列從1開始
        */
        static readonly int limit = 999;
        static int[,] map;
        static int[] vis;
        static int size, way, min = 999;

        static void Main(string[] args)
        {
            Console.WriteLine("圖的最短路徑，使用DFS");
            Console.WriteLine("輸入節點數與連通數(空白分隔) EX:5 5");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);// 節點個數
            way = int.Parse(temp[1]);// 連通邊數

            map = new int[size + 1, size + 1];
            vis = new int[size + 1];

            // 初始化
            for (int r = 1; r <= size; r++)
            {
                for (int c = 1; c <= size; c++)
                {
                    if (r == c) map[r, c] = 0;
                    else map[r, c] = limit;
                }
            }
            // 讀取
            Console.WriteLine("輸入連通的兩點與路徑長(空白分隔)，這裡已1號節點為起始點，最後一個節點為終點\nEX:1 2 4");
            int p1, p2, length;
            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);
                length = int.Parse(temp[2]);

                map[p1, p2] = length;
            }

            Console.WriteLine("圖形(0:無法自己到自己，1:可以通行，999:無法通行)，先直排在橫排(EX:1 2 = [1,2])");
            for (int r = 1; r <= size; r++)
            {
                for (int c = 1; c <= size; c++)
                {
                    Console.Write(map[r, c] + "\t");
                }
                Console.Write("\n");
            }
            
            Console.WriteLine("最短路徑");

            vis[1] = 1;
            dfs(1, 0);
            Console.WriteLine(min);

            Console.ReadKey();
        }

        static void dfs(int cur, int dist)
        {
            if (dist > min) return;// 該路徑已不是最短路徑，沒必要繼續探索
            if (cur == size)// 抵達終點
            {
                if (dist < min) min = dist;
                return;
            }

            for (int i = 1; i <= size; i++)
            {
                // 求路徑時可自己到自己
                if (map[cur, i] != limit && vis[i] == 0)
                {
                    vis[i] = 1;
                    dfs(i, dist + map[cur, i]);// 傳入探索節點與該節點長度
                    vis[i] = 0; // 探索完畢，取消標記
                }
            }
            return;
        }
    }
}
