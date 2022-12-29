using System;
using System.Globalization;

namespace BinaryTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyBinaryTree<int> tree = new MyBinaryTree<int>();

            tree.Add(5);
            tree.Add(3);
            tree.Add(4);
            tree.Add(7);
            tree.Add(6);
            tree.Add(9);
            tree.Add(10);
            tree.Add(8);

            Console.WriteLine(tree.Root.Value); // 5
            Console.WriteLine(tree.Root.Left.Value); // 3
            Console.WriteLine(tree.Root.Left.Right.Value); // 4
            Console.WriteLine(tree.Root.Right.Value); // 7
            Console.WriteLine(tree.Root.Right.Left.Value); // 6
            Console.WriteLine(tree.Root.Right.Right.Value); // 9
            Console.WriteLine(tree.Root.Right.Right.Left.Value); // 8
            Console.WriteLine(tree.Root.Right.Right.Right.Value); // 10

            tree.Remove(7);
            Console.WriteLine(tree.Root.Right.Value); // 8
            Console.WriteLine(tree.Root.Right.Right.Value); // 9
            Console.WriteLine(tree.Root.Right.Right.Right.Value); // 9
        }
    }
}
