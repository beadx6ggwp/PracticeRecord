using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class Node
    {
        public string data;
        public Node left;
        public Node right;
        public Node(string content)
        {
            data = content;
        }
    }

    public class Tree
    {
        public List<Node> list = new List<Node>();

        public void Add(Node node)
        {
            list.Add(node);
            if (list.Count > 1)
            {
                Node father = list[list.Count / 2 - 1];
                if (father.left == null)
                    father.left = node;
                else
                    father.right = node;
            }
        }

        public Node getRoot()
        {
            return list.Count > 0 ? list[0] : null;
        }
        public int getLevel(int count)
        {
            int result = 0;
            while (Math.Pow(2, result) <= count) result++;
            return result;
        }



        public Queue<Node> inOrderTraverse()// 中序
        {
            Stack<Node> stack = new Stack<Node>();
            Queue<Node> result = new Queue<Node>();
            inOrder(getRoot(), result);

            return result;
        }
        private void inOrder(Node n, Queue<Node> queue)
        {
            if (n != null)
            {
                inOrder(n.left, queue);
                queue.Enqueue(n);
                inOrder(n.right, queue);
            }
        }

        public Queue<Node> preOrderTraverse()// 前序
        {
            Stack<Node> stack = new Stack<Node>();
            Queue<Node> result = new Queue<Node>();

            stack.Push(getRoot());
            while (stack.Count > 0)
            {
                Node n = stack.Pop();
                result.Enqueue(n);

                if (n.right != null)
                    stack.Push(n.right);
                if (n.left != null)
                    stack.Push(n.left);
            }

            return result;
        }
        public Stack<Node> postOrderTraverse()// 後序
        {
            Stack<Node> stack = new Stack<Node>();
            Stack<Node> result = new Stack<Node>();
            stack.Push(getRoot());

            while (stack.Count > 0)
            {
                Node n = stack.Pop();
                result.Push(n);

                if (n.left != null)
                    stack.Push(n.left);
                if (n.right != null)
                    stack.Push(n.right);
            }

            return result;
        }
    }
}
