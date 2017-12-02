using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace bfs
{
    class Program
    {        
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
        static void Main(string[] args)
        {
            string path, text;
            string[] temp;
            int[][] map; // 主地圖
            int[,] vis; // 走訪標記
            //使用佇列儲存路徑
            int head, tail;
            List<note> list = new List<note>();
            // 反轉最短路徑
            List<note> shortest = new List<note>();
            // 右下左上(x, y)
            int[,] next = new int[,]
            {
                { 1, 0 },
                { 0, 1 },
                { -1, 0 },
                { 0, -1 }
            };
            int sx = 0, sy = 0, 
                ex = 2, ey = 3;

            Console.WriteLine("BFS最短步數與路徑搜尋，使用佇列");
            Console.WriteLine("輸入檔案名稱(跟執行檔不同資料夾請輸入絕對路徑)\nEx: maze.txt、maze1.txt、maze2.txt");
            path = Console.ReadLine();

            text = GetFileText(path);

            map = Get2DMap(text, " ", "", new string[] { "\r\n" });
            vis = new int[map.Length, map[0].Length];

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


            // 初始佇列
            head = 0;
            tail = 0;
            // 加入起始位置
            vis[0, 0] = 1;
            list.Add(new note(sx, sy, 0, 0));
            tail++;

            int flag = 0;
            int tx, ty;
            while (head < tail)
            {
                for (int dir = 0; dir < 4; dir++)
                {
                    tx = list[head].x + next[dir, 0];
                    ty = list[head].y + next[dir, 1];

                    if (tx < 0 || tx > map[0].Length - 1 || ty < 0 || ty > map.Length - 1)
                        continue;
                    if (map[ty][tx] == 0 && vis[ty, tx] == 0)
                    {
                        vis[ty, tx] = 1;
                        list.Add(new note(tx, ty, head, list[head].s + 1));
                        tail++;
                    }
                    if(tx == ex && ty == ey)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 1) break;

                head++;
            }
            // 最短步數只需取最後一個，因為每次擴展的步數是一樣的，所以先到終點的一定是最短
            Console.WriteLine($"\n==結果==\n");
            Console.WriteLine($"最短步數:{list[tail - 1].s}");
            Console.WriteLine($"路徑輸出->");

            // 最短路徑
            // 先加入終點
            shortest.Add(list[list.Count - 1]);

            // 從終點開始往回找
            int fa = tail - 1;
            while (fa > 0)
            {
                fa = list[fa].f;
                shortest.Add(list[fa]);
            }
            //輸出最短路徑，從起點輸出
            for (int i = shortest.Count - 1; i >= 0; i--)
            {
                note n = shortest[i];
                Console.WriteLine($"{shortest.Count - 1 - i}: ({n.x}, {n.y}), f:{n.f}, s:{n.s}");
            }


            Console.ReadKey();
        }
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
