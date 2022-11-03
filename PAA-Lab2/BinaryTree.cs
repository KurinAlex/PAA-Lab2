using System.Text;

namespace PAA_Lab2
{
    public class BinaryTree<T> where T : IComparable
    {
        private TreeNode _root;

        private const string LeftTurn = "└";
        private const string RightTurn = "┌";
        private const string Line = "────";
        private const string LineSpace = "│    ";
        private const string Space = "     ";

        public BinaryTree(T rootValue)
        {
            _root = new(rootValue);
        }
        public BinaryTree(T rootValue, IEnumerable<T> values)
        {
            _root = new(rootValue);
            foreach (T value in values)
            {
                Add(value);
            }
        }

        public void Add(T value)
        {
            AddInternal(_root, value);
        }
        public void Balance()
        {
            int balancingStep = 0;
            BalanceInternal(_root, null, ref balancingStep);
        }
        public static BinaryTree<T> CreateHeap(IEnumerable<T> values)
        {
            var valuesArray = values.ToArray();
            BinaryTree<T> tree = new(valuesArray[0]);
            tree._root.Left = CreateHeapInternal(valuesArray, 1);
            tree._root.Right = CreateHeapInternal(valuesArray, 2);
            return tree;
        }
        public override string ToString()
        {
            StringBuilder sb = new();
            if (_root.Right != null)
            {
                ToStringInternal(sb, string.Empty, _root.Right, false);
            }
            sb.AppendLine(_root.Value.ToString());
            if (_root.Left != null)
            {
                ToStringInternal(sb, string.Empty, _root.Left, true);
            }
            return sb.ToString();
        }

        private void AddInternal(TreeNode node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new(value);
                }
                else
                {
                    AddInternal(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new(value);
                }
                else
                {
                    AddInternal(node.Right, value);
                }
            }
            node.CorrectHeight();
        }
        private void BalanceInternal(TreeNode node, TreeNode? parent, ref int balancingStep)
        {
            while (node.GetBalanceFactor() > 1)
            {
                bool doubleRotate = false;
                if (node.Right!.GetBalanceFactor() < 0)
                {
                    node.Right = RotateRight(node.Right!);
                    doubleRotate = true;
                }
                node = ReplaceChildOfNodeOrRoot(parent, node, RotateLeft(node));
                WriteBalancingStep(doubleRotate ? "right-left" : "left", node, ref balancingStep);
            }
            while (node.GetBalanceFactor() < -1)
            {
                bool doubleRotate = false;
                if (node.Left!.GetBalanceFactor() > 0)
                {
                    node.Left = RotateLeft(node.Left!);
                    doubleRotate = true;
                }
                node = ReplaceChildOfNodeOrRoot(parent, node, RotateRight(node));
                WriteBalancingStep(doubleRotate ? "left-right" : "right", node, ref balancingStep);
            }
            if (node.Right != null)
            {
                BalanceInternal(node.Right, node, ref balancingStep);
            }
            if (node.Left != null)
            {
                BalanceInternal(node.Left, node, ref balancingStep);
            }
        }
        private static TreeNode? CreateHeapInternal(T[] values, int i)
        {
            return i >= values.Length
                ? null
                : new(values[i], CreateHeapInternal(values, 2 * i + 1), CreateHeapInternal(values, 2 * i + 2));
        }
        private TreeNode ReplaceChildOfNodeOrRoot(TreeNode? parent, TreeNode child, TreeNode newChild)
        {
            if (parent == null)
            {
                _root = newChild;
                return _root;
            }
            if (parent.Left == child)
            {
                parent.Left = newChild;
                return parent.Left;
            }
            parent.Right = newChild;
            return parent.Right;
        }
        private TreeNode RotateLeft(TreeNode node)
        {
            TreeNode right = node.Right!;
            node.Right = right!.Left;
            right.Left = node;
            node.CorrectHeight();
            right.CorrectHeight();
            return right;
        }
        private TreeNode RotateRight(TreeNode node)
        {
            TreeNode left = node.Left!;
            node.Left = left!.Right;
            left.Right = node;
            node.CorrectHeight();
            left.CorrectHeight();
            return left;
        }
        private void ToStringInternal(StringBuilder sb, string prefix, TreeNode node, bool isLeft)
        {
            if (node.Right != null)
            {
                ToStringInternal(sb, prefix + (isLeft ? LineSpace : Space), node.Right, false);
            }
            sb.AppendJoin(string.Empty, prefix, isLeft ? LeftTurn : RightTurn, Line,
                node.Value, Environment.NewLine);
            if (node.Left != null)
            {
                ToStringInternal(sb, prefix + (isLeft ? Space : LineSpace), node.Left, true);
            }
        }
        private void WriteBalancingStep(string rotationType, TreeNode node, ref int balancingStep)
        {
            Console.WriteLine($"Step {++balancingStep} ({rotationType} rotation on {node.Value}):");
            Writer.WriteDivider();
            Console.Write(this);
            Writer.WriteDivider();
        }

        private class TreeNode
        {
            public TreeNode(T value, TreeNode? left = null, TreeNode? right = null)
            {
                Value = value;
                Left = left;
                Right = right;
                CorrectHeight();
            }

            public T Value { get; set; }
            public TreeNode? Left { get; set; }
            public TreeNode? Right { get; set; }
            private int _height;

            public void CorrectHeight()
            {
                _height = 1 + Math.Max(GetHeight(Left), GetHeight(Right));
            }
            public int GetBalanceFactor()
            {
                return GetHeight(Right) - GetHeight(Left);
            }
            private static int GetHeight(TreeNode? node)
            {
                return node == null ? 0 : node._height;
            }
        }
    }
}
