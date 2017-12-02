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
            Console.WriteLine("暴力比對法");
            string s = "abcdeabdeqbde";
            string t = "abd";

            Console.WriteLine($"原字串:{s}");
            Console.WriteLine($"檢查串:{t}");
            Console.WriteLine($"index:{index(s, t, 0)}");
                

            Console.Read();
        }

        // 回傳t在s中的第一個索引位置，如果沒找到就回傳-1
        static int index(string s,string t,int pos)
        {
            int i = pos;
            int j = 0;

            // 如果原索引到底，或是檢查索引到底就跳出
            while (i < s.Length && j < t.Length)
            {
                if (s[i] == t[j])
                {
                    ++i;
                    ++j;
                }
                else
                {
                    i -= j;// 減掉檢查串的量，在+1往前一個
                    i++;

                    j = 0;// 重製檢查串
                }
            }
            // 如果檢查完後，j的位置 >= 檢查字串長，代表檢查有找到完整檢查的位置
            if (j >= t.Length) return i - t.Length;
            else return -1;
        }
    }
}
