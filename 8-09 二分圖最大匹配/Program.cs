using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二分圖最大匹配
{
    // 陣列從1開始
    class Program
    {
        static int size, way;
        static int[,] map;
        static int[] viste, match;
        static List<int> result = new List<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("二分圖最大匹配");
            Console.WriteLine("輸入節點數、連通數EX:6 5");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            map = new int[size + 1, size + 1];

            Console.WriteLine("輸入連通的兩點");
            int p1, p2;
            for (int i = 1; i <= way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);
                map[p1, p2] = 1;
                map[p2, p1] = 1;
            }

            Console.WriteLine("View");
            for (int r = 1; r <= size; r++)
            {
                for (int c = 1; c <= size; c++)
                {
                    Console.Write(map[r, c] + "\t");
                }
                Console.Write("\n");
            }
            // ---------Start---------------------

            int sum = 0;
            match = new int[size + 1];
            viste = new int[size + 1];

            for (int u = 1; u <= size; u++)
            {
                Array.Clear(viste, 1, size);// 清空查詢結果
                if (dfs(u)) sum++;
            }

            Console.WriteLine("匹配結果");
            for (int i = 1; i <= size; i++) Console.WriteLine($"u:{i}, v:{match[i]}");

            Console.WriteLine($"此二分圖的最大匹配:{sum}");

            Console.Read();
        }

        static bool dfs(int u)
        {
            for (int v = 1; v <= size; v++)
            {
                if (map[u, v] == 1 && viste[v] == 0)
                {
                    viste[v] = 1;
                    // 先看v點能不能跟其他配，不行的話在直接u配v
                    if (dfs(v)) return true;
                    else if (match[v] == 0)
                    {
                        match[v] = u;
                        match[u] = v;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
