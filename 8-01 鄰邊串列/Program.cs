using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static int[] u, v, w, firstLine, nextLine;// u頂點 -> v頂點 權值為w, first 對應頂點數, next對應連通數
        static int size, way;
        static void Main(string[] args)
        {
            Console.WriteLine("輸入節點數、連通數(空白分隔) EX:4 5");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            // +1 是因為從1開始使用
            u = new int[way + 1];
            v = new int[way + 1];
            w = new int[way + 1];

            firstLine = new int[size + 1];
            nextLine = new int[way + 1];

            for (int i = 1; i <= size; i++) firstLine[i] = -1;

            for (int i = 1; i <= way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                u[i] = int.Parse(temp[0]);
                v[i] = int.Parse(temp[1]);
                w[i] = int.Parse(temp[2]);

                nextLine[i] = firstLine[u[i]];
                firstLine[u[i]] = i;

                // first[u[i]] 保存頂點u[i]第一條邊的編號
                // next[i] 儲存編號為i的下一條邊
                Console.Write("first:");
                for (int j = 1; j <= size; j++) Console.Write(firstLine[j] + " ");
                Console.WriteLine();
                Console.Write("next:");
                for (int j = 1; j <= way; j++) Console.Write(nextLine[j] + " ");
                Console.WriteLine();
            }

            Console.WriteLine("-----------------");
            for (int i = 1; i <= size; i++)
            {
                int k = firstLine[i];
                while (k != -1)
                {
                    Console.WriteLine($"u:{u[k]}, v:{v[k]}, w:{w[k]}");
                    k = nextLine[k];
                }
            }

            Console.ReadKey();
        }
    }
}
