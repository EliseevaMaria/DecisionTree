using System;
using System.Collections.Generic;

namespace DecisionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            //Car[] cars = Helpers.FillCarArray(30);
            //Helpers.WriteCarArrayToFile(cars);
            Car[] cars = Helpers.ReadIntoCarArray();

            Node root = new Node(cars);
            root.BuildTree();

            Console.Write("ROOT");
            DescribeTree(root);

            Console.ReadLine();
        }

        private static void DescribeTree(Node root, int depth = 0)
        {
            for (int i = 0; i < depth; i++)
                Console.Write("\t");

            if (root.Children.Count == 0)
            {
                Console.WriteLine(root.Path + " (" + (root.IsCool ? "cool" : "not cool") + ")");
                return;
            }
            else
            {
                Console.WriteLine(root.Path);
                List<Node> children = root.Children;
                foreach (Node child in children)
                    DescribeTree(child, depth + 1);
            }
        }
    }
}
