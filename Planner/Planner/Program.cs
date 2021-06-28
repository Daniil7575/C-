
  
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm_2
{

    class Node
    {
        public int value;
        public Node LeftChild;
        public Node RightChild;
        public Node Parent;

        public Node()
        {
            LeftChild = null;
            RightChild = null;
            Parent = null;
        }

        public Node(int value)
        {
            this.value = value;
            LeftChild = null;
            RightChild = null;
            Parent = null;
        }


    }

    class BinaryTree
    {
        public Node rootNode { get; set; }
        public List<int> _addingHistory = new List<int>();
        private const int Chances = 10;
        public BinaryTree(int rootValue)
        {
            rootNode = new Node(rootValue);
        }

        public BinaryTree()
        {

        }

        public void Add(int value) //добавить значение в дерево
        {
            if (rootNode == null)
                rootNode = new Node(value);
            Node tempNode = rootNode;
            _addingHistory.Add(value);
            while (true)
            {
                if (tempNode.value == value)
                    return;

                if (tempNode.value > value)
                {
                    if (tempNode.LeftChild != null)
                    {
                        tempNode = tempNode.LeftChild;
                        continue;
                    };

                    tempNode.LeftChild = new Node(value)
                    {
                        Parent = tempNode
                    };
                    return;
                }

                if (tempNode.value < value)
                {
                    if (tempNode.RightChild != null)
                    {
                        tempNode = tempNode.RightChild;
                        continue;
                    }
                    tempNode.RightChild = new Node(value)
                    {
                        Parent = tempNode
                    };
                    return;
                }

            }
        }

        public Node Find(int key) //найти поддерево с корнем, значение которого равно key
        {
            Node tempNode = rootNode;
            while (true)
            {
                if (tempNode.value == key)
                    return tempNode;

                if (tempNode.value > key)
                {
                    if (tempNode.LeftChild != null)
                    {
                        tempNode = tempNode.LeftChild;
                        continue;
                    }
                    return null;
                }

                if (tempNode.value < key)
                {
                    if (tempNode.RightChild != null)
                    {
                        tempNode = tempNode.RightChild;
                        continue;
                    }
                    return null;
                }

            }
        }

        public void StraightOrder(Node tempNode) //прямой обход
        {
            if (tempNode == null)
                return;
            Console.Write($"{tempNode.value} ; ");
            StraightOrder(tempNode.LeftChild);
            StraightOrder(tempNode.RightChild);
        }

        public void SymmetricOrder(Node tempNode) //прямой обход
        {
            if (tempNode == null)
                return;

            SymmetricOrder(tempNode.LeftChild);
            Console.Write($"{tempNode.value} ; ");
            SymmetricOrder(tempNode.RightChild);
        }

        public void TreeRemove(BinaryTree Removal)
        {
            _addingHistory.Insert(0, rootNode.value);
            _addingHistory = _addingHistory.Distinct().ToList();
            _addingHistory.Sort();
            List<int> RemovingKeysList = new List<int>();
            foreach (var key in _addingHistory)
                if (!Removal._addingHistory.Contains(key))
                    RemovingKeysList.Add(key);
            RemovingKeysList.Sort();

            PrintColorful(rootNode, RemovingKeysList, spacing: 11);

            foreach (var key in RemovingKeysList)
            {
                if (rootNode.value != key)
                    NodeRemove(key);
            }

        }

        public void NodeRemove(int key)
        {
            Node temp = Find(key);
            Node Ptemp = temp.Parent;
            if (temp.LeftChild == null && temp.RightChild == null)
            {
                if (Ptemp.LeftChild == temp)
                    Ptemp.LeftChild = null;
                if (Ptemp.RightChild == temp)
                    Ptemp.RightChild = null;
            }
            else if (temp.LeftChild == null || temp.RightChild == null)
            {
                if (temp.LeftChild == null)
                {
                    if (Ptemp.LeftChild == temp)
                        Ptemp.LeftChild = temp.RightChild;
                    else
                        Ptemp.RightChild = temp.RightChild;
                    temp.RightChild.Parent = Ptemp;
                }
                else
                {
                    if (Ptemp.LeftChild == temp)
                        Ptemp.LeftChild = temp.LeftChild;
                    else
                        Ptemp.RightChild = temp.LeftChild;
                    temp.LeftChild.Parent = Ptemp;
                }
            }
            else
            {
                var Next = Find(_addingHistory[_addingHistory.IndexOf(temp.value) + 1]);
                temp.value = Next.value;
                if (Next.LeftChild == null && Next.RightChild == null)
                {
                    if (Next.Parent.LeftChild == Next)
                        Next.Parent.LeftChild = null;
                    if (Next.Parent.RightChild == Next)
                        Next.Parent.RightChild = null;
                }
                else
                {
                    if (Next.LeftChild == null)
                    {
                        if (Next.Parent.LeftChild == Next)
                            Next.Parent.LeftChild = Next.RightChild;
                        else
                            Next.Parent.RightChild = Next.RightChild;
                        Next.RightChild.Parent = Next.Parent;
                    }
                    else
                    {
                        if (Next.Parent.LeftChild == Next)
                            Next.Parent.LeftChild = Next.LeftChild;
                        else
                            Next.Parent.RightChild = Next.LeftChild;
                        Next.LeftChild.Parent = Next.Parent;
                    }
                }
                //if (Next.Parent.LeftChild == Next)
                //{
                //    Next.Parent.LeftChild = Next.RightChild;
                //    if (Next.RightChild != null)
                //        Next.RightChild.Parent = Next.Parent;
                //}
                //else
                //{
                //    Next.Parent.RightChild = Next.LeftChild;
                //    if (Next.LeftChild != null)
                //        Next.RightChild.Parent = Next.Parent;
                //}
            }

            _addingHistory.Remove(key);

        }

        public void LeftRotation()
        {
            var RightNode = rootNode.RightChild;
            rootNode.RightChild = RightNode.LeftChild;
            RightNode.LeftChild.Parent = rootNode;
            RightNode.LeftChild = rootNode;
            rootNode = RightNode;
        }

        public void RightRotation()
        {
            var LeftNode = rootNode.LeftChild;
            rootNode.LeftChild = LeftNode.RightChild;
            LeftNode.RightChild.Parent = rootNode;
            LeftNode.RightChild = rootNode;
            rootNode = LeftNode;
        }

        private class NodeInfo //для вывода
        {
            public Node Node;
            public string Text;
            public int StartPos;
            private int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public void Print(Node root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2) //для вывода
        {
            if (root == null) root = rootNode;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.value.ToString(textFormat) };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.LeftChild)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.LeftChild ?? next.RightChild;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    Print(item.Text, top, item.StartPos);
                    if (item.Left != null)
                    {
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.Node.RightChild;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        public void PrintColorful(Node root, List<int> RemovingKeysList, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2) //для вывода
        {
            if (root == null) root = rootNode;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.value.ToString(textFormat) };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.LeftChild)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.LeftChild ?? next.RightChild;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    if (RemovingKeysList.Contains(Convert.ToInt32(item.Text)) &&
                        Convert.ToInt32(item.Text) != rootNode.value)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    Print(item.Text, top, item.StartPos);
                    Console.ForegroundColor = ConsoleColor.White;
                    if (item.Left != null)
                    {
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.Node.RightChild;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(string s, int top, int left, int right = -1) //для вывода
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            // Console.ForegroundColor = ConsoleColor.Red;
            while (Console.CursorLeft < right) Console.Write(s);
            // Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintAddingHistory() //история добавлений значений
        {
            _addingHistory.Insert(0, rootNode.value);
            _addingHistory = _addingHistory.Distinct().ToList();
            foreach (var key in _addingHistory)
                Console.Write($"{key} ; ");
        }

        public BinaryTree OBST()
        {
            _addingHistory.Insert(0, rootNode.value);
            _addingHistory = _addingHistory.Distinct().ToList();
            _addingHistory.Sort();
            int[,] keyAndChance = new int[2, _addingHistory.Count];
            (int, int)[,] rootTable = new (int, int)[_addingHistory.Count + 1, _addingHistory.Count + 1];
            int h = 0, v = _addingHistory.Count;
            for (int t = 0; t < _addingHistory.Count; t++)
                keyAndChance[0, t] = _addingHistory[t];
            for (int t = 0; t < _addingHistory.Count; t++)
                keyAndChance[1, t] = Math.Abs(_addingHistory[t]) % Chances;


            while (v != -1)
            {
                for (int i = 0, j = h; i < v || j <= _addingHistory.Count; i++, j++)
                {
                    //int minDepth = int.MaxValue;
                    //int Row;
                    int O = 0;
                    (int, int) Pair = (int.MaxValue, 0); //minDepth and Row
                    if (i == j)
                    {
                        rootTable[i, j].Item1 = 0;
                        rootTable[i, j].Item2 = -1;
                        continue;
                    }
                    if (j == i + 1)
                    {
                        rootTable[i, j].Item1 = keyAndChance[1, j - 1];
                        //rootTable[i, j].Item2 = -1;
                        rootTable[i, j].Item2 = i;
                        continue;
                    }

                    for (int t = i; t <= j - 1; t++)
                        O += keyAndChance[1, t];

                    for (int _j = i, _i = i + 1; _j != j; _j++, _i++)
                    {
                        var temp = rootTable[i, _j].Item1 + rootTable[_i, j].Item1;
                        if (temp < Pair.Item1)
                        {
                            Pair.Item1 = temp;
                            Pair.Item2 = _j;
                        }
                    }

                    Pair.Item1 += O;
                    rootTable[i, j] = Pair;
                }

                v--;
                h++;
            }

            //Console.WriteLine();
            //for (int i = 0; i < _addingHistory.Count + 1; i++)
            //{
            //    for (int j = 0; j < _addingHistory.Count + 1; j++)
            //    {
            //        Console.Write($"({rootTable[i, j].Item1} , {rootTable[i, j].Item2})   ");
            //    }
            //    Console.WriteLine();
            //}


            //Console.ReadKey();
            BinaryTree OptimizedTree = new BinaryTree(rootTable[0, _addingHistory.Count].Item2);
            OptimizedTree.StraightOrderOBST(OptimizedTree.rootNode, (0, _addingHistory.Count), rootTable, keyAndChance);
            return OptimizedTree;

        }

        private void StraightOrderOBST(Node tempNode, (int, int) I_J, (int, int)[,] rootTable, int[,] keyAndChance)
        {
            if (I_J.Item1 != tempNode.value)
            {
                tempNode.LeftChild = new Node(rootTable[I_J.Item1, tempNode.value].Item2);
                tempNode.LeftChild.Parent = tempNode;
                StraightOrderOBST(tempNode.LeftChild, (I_J.Item1, tempNode.value), rootTable, keyAndChance);
            }

            if (tempNode.value + 1 != I_J.Item2)
            {
                tempNode.RightChild = new Node(rootTable[tempNode.value + 1, I_J.Item2].Item2);
                tempNode.RightChild.Parent = tempNode;
                StraightOrderOBST(tempNode.RightChild, (tempNode.value + 1, I_J.Item2), rootTable, keyAndChance);
            }

            tempNode.value = keyAndChance[0, tempNode.value];
        }

    }


    class Program
    {
        private static int RandNumber(int Low, int High)
        {
            return new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber)).Next(Low, High + 1);
        }
        static void Main(string[] args)
        {

            //Console.SetWindowSize(240, 63);
            //Console.ReadKey();
            BinaryTree a = new BinaryTree();
            a.Add(4);
            a.Add(2);
            a.Add(6);
            a.Add(3);
            var opta = a.OBST();
            //for (int i = 0; i < 50; i++)
            //    A.Add(RandNumber(-100, 100));
            opta.Print(null, spacing: 7);
            //BinaryTree B = new BinaryTree();

            //for (int i = 0; i < 50; i++)
            //    B.Add(RandNumber(-100, 100));

            //A.Print(null, spacing: 11);

            //Console.WriteLine("\n\nStraightOrder:");
            //A.StraightOrder(A.rootNode);

            //Console.WriteLine("\n\nSymmetricOrder:");
            //A.SymmetricOrder(A.rootNode);

            //Console.WriteLine();
            //for (int i = 0; i < Console.WindowWidth; i++)
            //    Console.Write("-");

            //var OptA = A.OBST();

            //OptA.Print(OptA.rootNode, spacing: 11);

            //Console.WriteLine("\n\nStraightOrder:");
            //OptA.StraightOrder(OptA.rootNode);

            //Console.WriteLine("\n\nSymmetricOrder:");
            //OptA.SymmetricOrder(OptA.rootNode);

            //Console.WriteLine();
            //Console.ForegroundColor = ConsoleColor.Red;
            //for (int i = 0; i < Console.WindowWidth; i++)
            //    Console.Write("-");
            //Console.ForegroundColor = ConsoleColor.White;

            //var OptB = B.OBST();

            //B.Print(null, spacing: 11);


            //Console.WriteLine("\n\nStraightOrder:");
            //B.StraightOrder(B.rootNode);

            //Console.WriteLine("\n\nSymmetricOrder:");
            //B.SymmetricOrder(B.rootNode);

            //Console.WriteLine();
            //for (int i = 0; i < Console.WindowWidth; i++)
            //    Console.Write("-");

            //OptB.Print(null, spacing: 11);

            //Console.WriteLine("\n\nStraightOrder:");
            //OptB.StraightOrder(OptB.rootNode);

            //Console.WriteLine("\n\nSymmetricOrder:");
            //OptB.SymmetricOrder(OptB.rootNode);

            //Console.WriteLine();
            //Console.ForegroundColor = ConsoleColor.Red;
            //for (int i = 0; i < Console.WindowWidth; i++)
            //    Console.Write("-");
            //Console.ForegroundColor = ConsoleColor.White;

            //OptA._addingHistory = A._addingHistory;
            //OptB._addingHistory = B._addingHistory;

            //OptA.TreeRemove(OptB);
            //OptA.Print(OptA.rootNode, spacing: 9);

            //Console.WriteLine();
            //for (int i = 0; i < Console.WindowWidth; i++)
            //    Console.Write("-");

            //OptA.LeftRotation();
            //OptA.Print(OptA.rootNode, spacing: 9);

            //OptA.RightRotation();
            //OptA.Print(OptA.rootNode, spacing: 9);

            //Console.ReadKey();


        }
    }
}
