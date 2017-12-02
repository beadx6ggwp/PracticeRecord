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
            Console.WriteLine("樹的走訪，前序、中序、後序\n");
            

            Tree tree = new Tree();
            tree.Add(new Node("A"));
            tree.Add(new Node("B"));
            tree.Add(new Node("C"));
            tree.Add(new Node("D"));
            tree.Add(new Node("E"));
            tree.Add(new Node("F"));
            tree.Add(new Node("G"));
            tree.Add(new Node("H"));
            tree.Add(new Node("I"));


            Console.WriteLine("前序");
            var preOrder = tree.preOrderTraverse();
            foreach (var i in preOrder) Console.Write(i.data);
            Console.WriteLine("\n");

            Console.WriteLine("中序");
            var inOrder = tree.inOrderTraverse();
            foreach (var i in inOrder) Console.Write(i.data);
            Console.WriteLine("\n");

            Console.WriteLine("後序");
            var postOrder = tree.postOrderTraverse();
            foreach (var i in postOrder) Console.Write(i.data);
            Console.WriteLine("\n");



            Console.ReadLine();
        }

    }
}
