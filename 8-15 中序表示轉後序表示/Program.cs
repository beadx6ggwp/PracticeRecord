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
            Console.WriteLine("四則運算(中序轉後序)");
            Console.WriteLine("輸入運算式");
            string input = Console.ReadLine();

            var postfix = toPostfix(input);

            Console.WriteLine("後續表示法");
            foreach (var i in postfix)
                Console.Write(i);

            Console.Read();
        }

        static Queue<string> toPostfix(string prefix)
        {
            Queue<string> postfix = new Queue<string>();
            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < prefix.Length; i++)
            {
                string content = prefix[i].ToString();

                switch (content)
                {
                    case "(":
                        stack.Push(content);
                        break;

                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        while (stack.Count > 0 && priority(stack.Peek()) >= priority(content))
                        {
                            postfix.Enqueue(stack.Pop());
                        }

                        stack.Push(content);
                        break;

                    case ")":
                        while (stack.Peek() != "(")
                        {
                            postfix.Enqueue(stack.Pop());
                        }
                        stack.Pop(); // 將"("也排除掉
                        break;

                    // 直接輸出
                    default:
                        StringBuilder temp = new StringBuilder();
                        // 防止2位數以上的數字被拆散
                        for (; i < prefix.Length; i++)
                        {
                            if (!isNum(prefix[i].ToString())) break;
                            temp.Append(prefix[i]);
                        }
                        i--;// 因為迴圈會多跑一圈，所以要-1

                        postfix.Enqueue(temp.ToString());
                        break;
                }
            }

            while (stack.Count > 0)
            {
                postfix.Enqueue(stack.Pop());
            }

            return postfix;
        }
        static int priority(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                    return 1;

                case "*":
                case "/":
                    return 2;

                default:
                    return 0;
            }
        }
        static bool isNum(string i)
        {
            switch (i)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "(":
                case ")":
                    return false;
                default:
                    return true;
            }
        }

    }

}
