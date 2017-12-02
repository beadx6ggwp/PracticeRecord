using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        struct note
        {
            public int x;// 編號
            public int s;// 次數
            public note(int x, int s)
            {
                this.x = x;
                this.s = s;
            }
        }
        /*
         * 注意:此專案陣列從1開始
        */
        static readonly int limit = 999;
        static int[,] map;
        static int size, way, start, end, min = 999;

        static int[] vis;
        static List<note> queue = new List<note>();
        static int head, tail;
        static void Main(string[] args)
        {
            Console.WriteLine("圖的最短路徑，使用BFS");
            Console.WriteLine("輸入節點數、連通數、起始點、終點(空白分隔) EX:5 7 1 5");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);
            start = int.Parse(temp[2]);
            end = int.Parse(temp[3]);

            map = new int[size + 1, size + 1];
            vis = new int[size + 1];
            for (int r = 1; r <= size; r++)
            {
                for (int c = 1; c <= size; c++)
                {
                    if (r == c) map[r, c] = 0;
                    else map[r, c] = limit;
                }
            }

            Console.WriteLine("輸入連通的兩點(空白分隔)\nEX:1 2");
            int p1, p2;
            for (int i = 0; i < way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                p1 = int.Parse(temp[0]);
                p2 = int.Parse(temp[1]);
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

            Console.WriteLine("最少轉點");
            // 初始佇列
            head = 0;
            tail = 0;
            //加入起始點
            queue.Add(new note(start, 0));
            tail++;
            vis[start] = 1;

            int flag = 0;
            while(head < tail)
            {
                note cur = queue[head];// 取得被加入佇列的節點
                for (int i = 1; i <= size; i++)
                {
                    if (map[cur.x, i] == 1 && vis[i] == 0)
                    {
                        queue.Add(new note(i, cur.s + 1));
                        tail++;
                        vis[i] = 1;
                    }

                    if (cur.x == end)
                    {
                        flag = 1;
                        break;
                    }
                }

                if (flag == 1) break;
                head++;// 往下個點檢查
            }
            // 直接取得最後一個點是因為當抵達終點時就跳出，所以最後一點一定是完成的
            // 而且BFS在搜索時，是該點的所有周圍都一起擴展，所以最先到達終點的一定是轉點最少的，得證
            Console.WriteLine(queue[tail - 1].s);

            Console.ReadKey();
        }
    }
}
