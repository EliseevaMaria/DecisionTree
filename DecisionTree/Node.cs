using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTree
{
    public class Node
    {
        public Car[] Set { set; get; }
        public List<Node> Children { set; get; }
        public bool IsLeaf { set; get; }
        public bool IsCool { set; get; }
        public string Path = "";
        bool isMoreCools = false;

        public Node()
        {
            Set = new Car[0];
            Children = new List<Node>();
            IsLeaf = false;
            Path = "";
        }
        public Node(Car[] set, string path = "")
        {
            Set = set;
            Children = new List<Node>();
            IsLeaf = false;
            Path = path;
        }

        internal void BuildTree()
        {
            int coolCount = 0,
                nonCoolCount = 0;
            foreach (Car car in Set)
            {
                if (car.IsCool) coolCount++;
                else nonCoolCount++;
            }
            isMoreCools = coolCount > nonCoolCount;

            if (coolCount == 0 || nonCoolCount == 0)
            {
                IsLeaf = true;
                IsCool = nonCoolCount == 0;
                return;
            }
            if (Set.Length == 0)
            {
                IsLeaf = true;
                return;
            }

            SplitSet();

            foreach (Node child in Children)
                child.BuildTree();
        }

        public void SplitSet()
        {
            double maxGain = 0.0;
            Node[] bestNodeSet = null;

            double someGain = 0.0;
            Node[] someNodeSet = null;

            someNodeSet = SplitSetWithEnum(typeof(Brand), 1);
            someGain = GetGain(someNodeSet);
            if (someGain >= maxGain)
            {
                maxGain = someGain;
                bestNodeSet = someNodeSet;
            }

            someNodeSet = SplitSetWithEnum(typeof(BodyType), 2);
            someGain = GetGain(someNodeSet);
            if (someGain >= maxGain)
            {
                maxGain = someGain;
                bestNodeSet = someNodeSet;
            }
                        
            for (int i = Car.MinYear; i < Car.MaxYear - 1; i++)
            {
                double middle = (i + i + 1) / 2.0;
                someNodeSet = SplitSetWithNumber(3, middle);
                someGain = GetGain(someNodeSet);
                if (someGain >= maxGain)
                {
                    maxGain = someGain;
                    bestNodeSet = someNodeSet;
                }
            }

            foreach (Node node in bestNodeSet)
            {
                node.IsCool = isMoreCools;
                Children.Add(node);
            }
        }

        private double GetGain(Node[] nodes)
        {
            double attrEntropy = 0.0;
            foreach (Node node in nodes)
            {
                if (node.Set.Length == 0)
                    return 0.0;
                attrEntropy += node.Set.Length / (Set.Length * 1.0) * node.GetEntropy();
            }
            return GetEntropy() - attrEntropy;
        }

        private Node[] SplitSetWithEnum(Type enumType, int fieldIndex)
        {
            int enumLength = Enum.GetNames(enumType).Length;
            Node[] result = new Node[enumLength];

            for (int i = 0; i < enumLength; i++)
            {
                result[i] = new Node();
                result[i].Set = (from car in Set
                                 where (int)car[fieldIndex] == i
                                 select car).ToArray();
                result[i].Path = enumType.ToString() + enumType.GetEnumName(i);
            }
            return result;
        }

        private Node[] SplitSetWithNumber(int fieldIndex, double middle)
        {
            Node[] result = new Node[2] { new Node(), new Node() };

            result[0].Set = (from car in Set
                             where (int)car[fieldIndex] <= middle
                             select car).ToArray();
            result[0].Path = "Year <= " + ((int)middle).ToString();
            result[1].Set = (from car in Set
                             where (int)car[fieldIndex] > middle
                             select car).ToArray();
            result[1].Path = "Year > " + ((int)middle).ToString();
            return result;
        }

        double GetEntropy()
        {
            return GetEntropy(Set);
        }        
        double GetEntropy(Car[] set)
        {
            double info = 0.0;
            int coolCarsCount = (from car in set
                                 where (bool)car[4]
                                 select car).ToArray().Length;

            double trueFreq = coolCarsCount / (set.Length * 1.0);
            info -= trueFreq * Math.Log(trueFreq, 2);

            double falseFreq = (set.Length - coolCarsCount) / (set.Length * 1.0);
            info -= falseFreq * Math.Log(falseFreq, 2);

            if (double.IsNaN(info))
                return -1.0;

            return info;
        }
    }
}
