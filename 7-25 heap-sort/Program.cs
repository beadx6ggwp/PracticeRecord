using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_tree
{
    class Program
    {
        static int[] heap;
        static int size;
        static void Main(string[] args)
        {
            Console.WriteLine("堆積排序-最小堆積樹版本(小->大)");
            Console.WriteLine("請輸入數量");
            size = int.Parse(Console.ReadLine());

            Console.WriteLine("輸入愈排序數字");

            heap = new int[size + 1];
            for (int i = 1; i <= size; i++) heap[i] = int.Parse(Console.ReadLine());

            create();
            Console.WriteLine("二元樹");
            Show();

            // heap[0] = 不使用，如果完整走完，在DeleteTop()時候導致最後一個數出現兩次
            // heap.Length - 1單純只是執行的次數，跟陣列位置無關
            Console.WriteLine("排序後");
            for (int i = 1; i <= heap.Length - 1; i++)
                Console.Write(DeleteTop() + " ");

            Console.ReadKey();
        }
        static void Show()
        {
            Console.WriteLine("----------");
            for (int i = 1; i <= size; i++) Console.Write(heap[i] + " ");
            Console.WriteLine("");
        }

        static void create()
        {
            for (int i = size / 2; i >= 1; i--)
                ShiftDown(i);
        }
        static void ShiftDown(int i)
        {
            // i:愈下移點, temp:下移點
            int flag = 0, temp = 0;
            while (i * 2 <= size && flag == 0)
            {
                // 如果左節點較小，紀錄左節點位置
                if (heap[i * 2] < heap[i]) temp = i * 2;
                else temp = i;

                // 檢查右節點存在
                if (i * 2 + 1 <= size)
                {
                    // 重要heap[temp]，而不是heap[i]，否則可能會跟較大的子節點換位，導致出錯
                    // 如果右節點更小，temp = 右節點
                    if (heap[i * 2 + 1] < heap[temp]) temp = i * 2 + 1;
                }

                if (temp != i)
                {
                    swap(heap, i, temp);// 符合條件就交換
                    i = temp; //更新所在編號 
                }
                else
                    flag = 1;
            }
        }
        static int DeleteTop()
        {
            int temp = 0;// 儲存被移除的頂點
            temp = heap[1];
            heap[1] = heap[size];
            size--;
            ShiftDown(1);
            return temp;
        }

        static void swap(int[] arr, int a, int b)
        {
            arr[a] += arr[b];
            arr[b] = arr[a] - arr[b];
            arr[a] = arr[a] - arr[b];
        }
    }
}
