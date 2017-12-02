using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 圖的最小生成樹_Prim_堆積優化_
{
    /*
        此程式陣列由1開始         
        並使用最小堆積(小的在上面)
    */
    class Program
    {
        static int[] heap, pos;// 堆積中的頂點編號, 各節點在堆積中的位置
        static int heapSize;

        static int[] dis, viste;// 起點到各點的距離

        static int size, way, count, sum;
        static int[] u, v, w, first, next;

        static int inf = 999;

        static void Main(string[] args)
        {
            Console.WriteLine("輸入節點數、連通數(空白分隔) EX:6 9");
            string[] temp = Console.ReadLine().Split(' ');
            size = int.Parse(temp[0]);
            way = int.Parse(temp[1]);

            // 此為無向圖，所以way*2
            first = new int[size + 1];
            next = new int[way * 2 + 1];

            u = new int[way * 2 + 1];
            v = new int[way * 2 + 1];
            w = new int[way * 2 + 1];

            Console.WriteLine("輸入連通的兩點、長度(空白分隔)\nEX:2 4 11");
            for (int i = 1; i <= way; i++)
            {
                temp = Console.ReadLine().Split(' ');
                u[i] = int.Parse(temp[0]);
                v[i] = int.Parse(temp[1]);
                w[i] = int.Parse(temp[2]);

                u[i + way] = v[i];
                v[i + way] = u[i];
                w[i + way] = w[i];
            }

            for (int i = 1; i <= size; i++) first[i] = -1;

            for (int i = 1; i <= way * 2; i++)
            {
                next[i] = first[u[i]];
                first[u[i]] = i;
            }
            //Start---------------------------------------------------

            viste = new int[size + 1];
            dis = new int[size + 1];

            //將1號點加入生成樹
            viste[1] = 1;// 標記起點已加入生成樹
            count++;

            // 初始起始頂點距離，與起點到各點的距離
            dis[1] = 0;
            for (int i = 2; i <= size; i++) dis[i] = inf;
            int k = first[1];
            while (k != -1)
            {
                dis[v[k]] = w[k];
                k = next[k];
            }

            // 初始化堆積
            heapSize = size;
            heap = new int[size + 1];
            pos = new int[size + 1];
            for (int i = 1; i <= size; i++) { heap[i] = i; pos[i] = i; }
            for (int i = size / 2; i >= 1; i--) shiftdown(i);

            pop();// 先彈出頂點，因為這時堆積頂是起點

            int t;
            while (count < size)
            {
                t = pop();

                viste[t] = 1;
                count++;
                sum += dis[t];

                // 掃描目前頂點t，再以t為中間節點，進行鬆弛
                k = first[t];
                while (k != -1)
                {
                    if (viste[v[k]] == 0 && dis[v[k]] > w[k])
                    {
                        dis[v[k]] = w[k];// 更新距離
                        shiftup(pos[v[k]]);// 堆該點在堆積中向上調整
                    }
                    k = next[k];
                }
            }

            Console.WriteLine($"最小成本:{sum}");

            Console.Read();
        }



        static int pop()// 取出堆積頂元素(最小值)
        {
            int temp = heap[1];
            heap[1] = heap[heapSize];// 將堆積尾移到堆積頂
            pos[heap[1]] = 1;// 更新該節點在堆積中的位置

            heapSize--;
            shiftdown(1);
            return temp;
        }
        static void shiftdown(int i)// 一直下移，直到節點[i]比子節點還小
        {
            int flag = 0, temp = 0;
            // 先確認這個節點有左兒子
            while (i * 2 <= heapSize && flag == 0)
            {
                // 如果左兒子的值比較小，紀錄左兒子
                if (dis[heap[i * 2]] < dis[heap[i]])
                    temp = i * 2;
                else
                    temp = i;

                // 如果有右兒子
                if (i * 2 + 1 <= heapSize)
                {
                    // 如果右兒子的值比左兒子、原節點更小，紀錄右兒子
                    if (dis[heap[i * 2 + 1]] < dis[heap[temp]])
                        temp = i * 2 + 1;
                }

                // 如果最小節點不是自己，代表有更小的，交換堆積中的位置(下移)
                if (temp != i)
                {
                    swap(i, temp);
                    i = temp;// 更新與兒子交換後的編號，繼續往下檢查
                }
                else flag = 1;// 否則代表，當前節點比子節點小了
            }
        }
        static void shiftup(int i)// 一直上移，直到節點[i]比父節點大
        {
            int flag = 0;

            if (i == 1) return;// 代表已經在堆積頂

            while (i != 1 && flag == 0)
            {
                // 如果該節點比父節點小，跟父節點交換
                if (dis[heap[i]] < dis[heap[i / 2]])
                {
                    swap(i, i / 2);
                    i = i / 2;// 更新與父親交換後的編號，繼續往上檢查
                }
                else flag = 1;
            }
        }

        static void swap(int a, int b)
        {
            int temp = heap[a];
            heap[a] = heap[b];
            heap[b] = temp;

            //同步更新pos
            temp = pos[heap[a]];
            pos[heap[a]] = pos[heap[b]];
            pos[heap[b]] = temp;
        }

    }
}
