using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("二分搜尋");
            Console.WriteLine("輸入資料串(空白分割)，再輸入搜尋資料");
            int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int search = int.Parse(Console.ReadLine());
            Array.Sort(input);


            foreach (var i in input) Console.Write($"{i} ");
            Console.WriteLine($"\n{Binary_Search(input, search)}");


            Console.Read();
        }

        static int Binary_Search(int[] arr, int key)
        {
            int low = 0, high = arr.Length - 1, mid = 0;

            while (low <= high)
            {
                mid = (low + high) / 2;

                if (key < arr[mid])
                    high = mid - 1;
                else if (key > arr[mid])
                    low = mid + 1;
                else
                    return mid;
            }
            return -1;
        }
    }
}
