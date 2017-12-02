using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace dfs
{
    
    class Program
    {
        static string path, text;
        static string[] temp;
        static int[][] map; // 主地圖
        static int[,] vis; // 走訪標記

        static Stack2 stack;

        static int min = 9999;
        // 右下左上(x, y)
        static int[,] next = new int[,]
        {
                { 1, 0 },
                { 0, 1 },
                { -1, 0 },
                { 0, -1 }
        };
        static int sx = 0, sy = 0,
                   ex = 2, ey = 3;
        static void Main(string[] args)
        {
            Console.WriteLine("DFS最短步數與路徑搜尋，使用堆疊");
            Console.WriteLine("輸入檔案名稱(跟執行檔不同資料夾請輸入絕對路徑)\nEx: maze.txt、maze1.txt、maze2.txt");
            path = Console.ReadLine();

            text = GetFileText(path);

            map = Get2DMap(text, " ", "", new string[] { "\r\n" });
            vis = new int[map.Length, map[0].Length];

            stack = new Stack2();

            Console.WriteLine($"路徑:{path}");
            Console.WriteLine($"讀入檔案\n{text}");
            Console.WriteLine("讀入陣列");
            for (int r = 0; r < map.Length; r++)
            {
                for (int c = 0; c < map[r].Length; c++)
                {
                    Console.Write(map[r][c]);
                }
                Console.Write("\n");
            }

            Console.WriteLine("\n輸入起始位置、終點位置(以空白分隔)EX:0 0 2 3");
            temp = Console.ReadLine().Split(' ');
            sx = int.Parse(temp[0]); sy = int.Parse(temp[1]);
            ex = int.Parse(temp[2]); ey = int.Parse(temp[3]);

            // 加入起始位置
            vis[sy, sx] = 1;
            stack.push(new note(sx, sy, stack.top, 0));
            dfs(sx, sy, 0);

            Console.WriteLine($"最短步數:{min}");

            Console.ReadKey();
        }
        static void dfs(int x, int y, int step)
        {
            if (x == ex && y == ey)
            {
                if (step < min)
                {
                    min = step;
                    ShowPath();
                }
                return;
            }
            int tx, ty;
            for (int dir = 0; dir < 4; dir++)
            {
                tx = x + next[dir, 0];
                ty = y + next[dir, 1];

                if (tx < 0 || tx > map[0].Length - 1 || ty < 0 || ty > map.Length - 1)
                    continue;
                if (map[ty][tx] == 0 && vis[ty, tx] == 0)
                {
                    vis[ty, tx] = 1;
                    stack.push(new note(tx, ty, stack.top, step + 1));//加入堆疊
                    dfs(tx, ty, step + 1);
                    stack.pop();// 搜索完畢，移出堆疊
                    vis[ty, tx] = 0;
                }
            }
            return;
        }
        static void ShowPath()
        {
            Console.WriteLine("----");
            for (int i = 0; i < stack.data.Count; i++)
            {
                note n = stack.data[i];
                Console.WriteLine($"{i}:({n.x}, {n.y}), f:{n.f}, s:{n.s}");
            }
        }
        class Stack2
        {
            public int top = -1;
            public List<note> data = new List<note>();
            public Stack2()
            {
                top = -1;
                data = new List<note>();
            }
            public note pop()
            {
                note n = data[top];
                data.RemoveAt(top);
                top--;
                return n;
            }
            public void push(note n)
            {
                top++;
                data.Add(n);
            }
            public bool IsStackEmpty()
            {
                return top <= 0 ? true : false;
            }
        }
        struct note
        {
            // x,y,父節點,步數
            public int x, y, f, s;
            public note(int x, int y, int f, int s)
            {
                this.x = x;
                this.y = y;
                this.f = f;
                this.s = s;
            }
        };

        static int[][] Get2DMap(string text, string oldText, string newText, string[] separator)
        {
            // 1.先將指定文字置換
            // 2.根據指定符號分出Row
            // 3.根據指定分隔符號分出Col
            int[][] result;
            string[] str_arr;

            text = text.Replace(oldText, newText);

            str_arr = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            result = new int[str_arr.Length][];
            for (int row = 0; row < result.Length; row++)
            {
                result[row] = Array.ConvertAll(str_arr[row].Split(','), int.Parse);
            }

            return result;
        }
        static string GetFileText(string path)
        {
            // 回傳指定路徑中的文字(不做任何處理)
            string result = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return result;
        }
    }
}

