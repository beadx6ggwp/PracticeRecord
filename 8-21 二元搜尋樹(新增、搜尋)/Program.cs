using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二元搜尋樹
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("二元搜尋樹");

            int[] arr = new int[] { 62, 88, 58, 47, 35, 73, 51, 99, 37, 93 };
            

            Node root = null;

            for (int i = 0; i < arr.Length; i++)
                InsertBST(ref root, arr[i]);


            Node result = null;
            SearchBST(root, 93, null, ref result);

            Console.ReadLine();
        }


        static bool SearchBST(Node tree, int key, Node father, ref Node result)// 回傳搜尋結果，並透過result取得待插入的點
        {
            if (tree == null)// 以到達葉子，搜尋失敗
            {
                result = father;
                return false;
            }
            else if (key == tree.data)// 搜尋成功
            {
                result = tree;
                return true;
            }
            else if (key < tree.data)// 透過二分法選邊繼續搜尋
                return SearchBST(tree.left, key, tree, ref result);
            else
                return SearchBST(tree.right, key, tree, ref result);
        }

        static bool InsertBST(ref Node tree, int key) // 回傳是否插入成功
        {
            Node result = null, newNode = null;

            if (!SearchBST(tree, key, null, ref result))
            {
                newNode = new Node(key);

                if (result == null)// 插入新結點
                    tree = newNode;
                else if (key < result.data)
                    result.left = newNode;// 插入為左子節點
                else
                    result.right = newNode;// 插入為右子節點

                return true;
            }
            else
                return false;
        }

        static bool DeleteBST(Node tree, int key)
        {
            if (tree == null)
                return false;
            else
            {
                if (key == tree.data)
                    return Delete(tree);
                else if (key < tree.data)
                    return DeleteBST(tree.left, key);
                else
                    return DeleteBST(tree.right, key);
            }
        }

        static bool Delete(Node tree)
        {
            return false;
        }
    }


    public class Node
    {
        public int data;
        public Node left;
        public Node right;
        public Node(int data)
        {
            this.data = data;
        }
        public Node() : this(0) { }
    }
}
