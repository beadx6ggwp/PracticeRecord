using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_P30
{
    class Program
    {
        static int[] num;
        static int n, k;
        static bool result;
        static void Main(string[] args)
        {
            Console.WriteLine("部分和問題");
            Console.WriteLine("input n");
            n = int.Parse(Console.ReadLine());

            Console.WriteLine("input numbers");
            num = new int[n];
            for (int i = 0; i < n; i++)
            {
                num[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("input k");
            k = int.Parse(Console.ReadLine());

            solve(0, 0);
            Console.WriteLine(result);


            Console.Read();
        }


        static void solve(int step, int sum)
        {
            if (sum == k)
            {
                result = true;
                return;
            }


            if (step >= n) return;

            solve(step + 1, sum);

            solve(step + 1, sum + num[step]);

            return;
        }
    }
}
