using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static int size, way;
        static edge[] edges;
        static int sum, count;// 權值總和,連通邊數( < size - 1)
        static int[] father;
        static List<edge> result = new List<edge>();
        static void Main(string[] args)
        {
            Console.WriteLine("圖的最小生成樹 kruskal");
            Console.WriteLine("輸入節點數、連通數(空白分隔) EX:6 9");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            edges = new edge[way + 1];

            Console.WriteLine("輸入連通的兩點、路徑長(空白分隔)EX:1 2 2");
            for (int i = 1; i <= way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                edges[i] = new edge(int.Parse(temp[0]), int.Parse(temp[1]), int.Parse(temp[2]));
            }

            //------------Start------------------

            QuickSort(edges, 1, way);


            father = new int[size + 1];
            for (int i = 1; i <= size; i++) father[i] = i;

            Console.WriteLine("Sort");
            for (int i = 1; i <= way; i++) Console.WriteLine($"u:{edges[i].u}, v:{edges[i].v}, w:{edges[i].w}");

            for (int i = 1; i <= way; i++)
            {
                var obj = edges[i];
                if (merge(obj.u, obj.v) == 1)
                {
                    count++;
                    sum += obj.w;
                    result.Add(obj);
                }
                if (count == size - 1) break;// 如果加入的邊已達最大邊數，代表結束
            }
            Console.WriteLine("--------------------");
            Console.WriteLine("Result");
            foreach (var i in result)
                Console.WriteLine($"u:{i.u}, v:{i.v}, w:{i.w}");
            Console.WriteLine($"最小生成成本:{sum}");

            Console.Read();
        }

        static int merge(int p1, int p2)// 將右節點合併到左節點下 return 1: 不同集合、return 0:同集合
        {
            int root1 = getf(p1),
                root2 = getf(p2);

            if (root1 != root2)// 這兩節點不再同一集合中
            {
                father[root2] = root1;
                return 1;
            }
            return 0;
        }
        static int getf(int p)// 回朔根結點
        {
            if (father[p] == p)
                return p;
            else
                return getf(father[p]);
        }

        static void QuickSort(edge[] arr, int left, int right)
        {
            if (left >= right) return;

            int L = left,
                R = right;
            edge temp = arr[left];


            while (L < R)
            {
                while (arr[R].w >= temp.w && L < R) R--;
                while (arr[L].w <= temp.w && L < R) L++;

                if (L < R) swap(arr, L, R);
            }

            swap(arr, left, R);// here

            QuickSort(arr, left, L - 1);
            QuickSort(arr, L + 1, right);
        }
        static void swap(edge[] arr, int index1, int index2)
        {
            edge temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }

        struct edge
        {
            public int u, v, w;
            public edge(int u, int v, int w)
            {
                this.u = u;
                this.v = v;
                this.w = w;
            }
        }
    }
}
