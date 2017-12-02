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
        static readonly int inf = 999;
        static int[] dist, copy, u, v, w;
        static int size, way, calc_count = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("圖的最短路徑，使用Belllman-Ford(負權與優化)，起點預設為1號點，計算起點至每個節點的最短距離");
            Console.WriteLine("輸入節點數、連通數(空白分隔) EX:5 5");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            u = new int[way + 1];
            v = new int[way + 1];
            w = new int[way + 1];

            for (int i = 1; i <= way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                u[i] = int.Parse(temp[0]);
                v[i] = int.Parse(temp[1]);
                w[i] = int.Parse(temp[2]);
            }

            Console.WriteLine($"size:{size}, way:{way}");
            for (int i = 1; i <= way; i++)
            {
                Console.WriteLine($"u:{u[i]}, v:{v[i]}, w:{w[i]}");
            }

            //--------------------------------------------

            // 初始化
            copy = new int[size + 1];
            dist = new int[size + 1];
            for (int i = 1; i <= size; i++) dist[i] = inf;

            dist[1] = 0;// 以1號為起點鬆弛

            for (int k = 1; k <= size - 1; k++)
            {
                // 複製鬆弛前的dist
                for (int j = 1; j <= size; j++) copy[j] = dist[j];

                for (int i = 1; i <= way; i++)
                {
                    calc_count++;
                    if (dist[u[i]] + w[i] < dist[v[i]])
                        dist[v[i]] = dist[u[i]] + w[i];
                }

                bool isChange = false;// false = 未更動, true = 有更動
                // 如果鬆弛後的dist跟鬆弛前一樣，代表已經完成鬆弛，跳出迴圈
                for (int j = 1; j <= size; j++)
                {
                    if (copy[j] != dist[j])
                    {
                        isChange = true;
                        break;
                    }
                }
                if (!isChange) break;
            }


            // 因為正常無負權在size -1時就已經鬆弛完畢，如果還能在鬆弛代表此路徑將會無限小
            /*
             * ex: 2 2
             *     1 2 -1
             *     2 1 -1
             */
            for (int i = 1; i <= way; i++)
            {
                if (dist[u[i]] + w[i] < dist[v[i]])
                {
                    Console.WriteLine("此圖有負環，無法計算");
                }
            }

            Console.WriteLine("總計算成本:" + calc_count);
            for (int i = 1; i <= size; i++) Console.Write(dist[i] + " ");

            Console.Read();
        }
    }
}
