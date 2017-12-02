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
         * 注意:此專案的陣列以1當起始
         * 
         */
        static int[,] map;// 節點連通表
        static int[] vis;// 節點走訪標記
        static int size, way, sum;
        static void Main(string[] args)
        {
            Console.WriteLine("圖的走訪，使用DFS");
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
                    else map[r, c] = 999;
                }
            }
            // 讀取
            Console.WriteLine("輸入連通的兩點(空白分隔)，請以1號節點為根結點EX:1 2");
            int p1, p2;
            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);
                // 無向圖
                map[p1, p2] = 1;
                map[p2, p1] = 1;
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

            Console.WriteLine("走訪順序");
            vis[1] = 1;// 從1號節點出發
            dfs(1);


            Console.ReadKey();
        }

        static void dfs(int cur)//cur所在編號
        {
            Console.WriteLine(cur);
            sum++;
            if (sum == size) return;// 走訪完畢
            for (int i = 1; i <= size; i++)
            {
                if (map[cur, i] == 1 && vis[i] == 0)//如果該節點有邊，往下搜尋
                {
                    // 走訪一遍就夠，所以不用把vis[i]在重設
                    vis[i] = 1;
                    dfs(i);
                }
            }
            return;
        }
    }
}
