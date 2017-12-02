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
        static readonly int inf = 999;
        static int[] dist, u, v, w;
        static int size, way, calc_count = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("圖的最短路徑，使用Belllman-Ford，起點預設為1號點，計算起點至每個節點的最短距離");
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


            // ---------------------------------------------

            dist = new int[size + 1];
            for (int i = 1; i <= size; i++)
                dist[i] = inf;

            dist[1] = 0;// 因為起點為1號，所以自己的距離為0，重要
            for (int k = 1; k <= size - 1; k++)// 最差的鬆弛次數為頂點數-1
            {
                for (int i = 1; i <= way; i++)
                {
                    calc_count++;
                    if (dist[u[i]] + w[i] < dist[v[i]])
                    {
                        dist[v[i]] = dist[u[i]] + w[i];
                    }
                }
            }

            Console.WriteLine("總計算成本:" + calc_count);
            for (int i = 1; i <= size; i++)
                Console.WriteLine($"起點到頂點{i}的距離: {dist[i]}");


            Console.Read();
        }
    }
}
