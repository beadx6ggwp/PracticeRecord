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
            Console.WriteLine("後序式的運算");
            Console.WriteLine("輸入四則運算式");
            string input = Console.ReadLine();//"9+(3-1)*3+10/2"

            var postfix = toPostfix(input);
            Console.WriteLine("後續表示法");
            foreach (var i in postfix)
                Console.Write(i);

            Console.WriteLine("\n\nresult");
            Console.WriteLine(calcPostfix(postfix));

            Console.Read();
        }


        static double calcPostfix(Queue<string> postfix)
        {
            Stack<double> stack = new Stack<double>();

            while (postfix.Count > 0)
            {
                string content = postfix.Dequeue();
                switch (content)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        double b = stack.Pop(),
                               a = stack.Pop();

                        stack.Push(calc(a, b, content));
                        break;

                    default:
                        stack.Push(double.Parse(content));
                        break;
                }
            }
            // 結束後堆疊頂就是答案
            return stack.Pop();
        }

        static double calc(double a, double b, string op)
        {
            double result = 0;
            switch (op)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    result = a / b;
                    break;
            }
            return result;
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
                        while (stack.Count != 0 && priority(stack.Peek()) >= priority(content))
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
                        stack.Pop();// 將"("也排除掉
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
            // 將堆疊中剩下的拿出來
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
