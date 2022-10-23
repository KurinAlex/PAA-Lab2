using System.Text;

namespace PAA_Lab2
{
    public class BinaryTree<T> where T : IComparable
    {
        private Node<T> _root;

        private const string LeftTurn = "└";
        private const string RightTurn = "┌";
        private const string Line = "────";
        private const string LineSpace = "│    ";
        private const string Space = "     ";

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
            _root = BalanceInternal(_root);
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

        private void AddInternal(Node<T> node, T value)
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
            CorrectHeight(node);
        }
        private Node<T> BalanceInternal(Node<T> node)
        {
            if (node.Right != null)
            {
                node.Right = BalanceInternal(node.Right);
            }
            if (node.Left != null)
            {
                node.Left = BalanceInternal(node.Left);
            }
            while (GetBalanceFactor(node) > 1)
            {
                if (GetBalanceFactor(node.Right) < 0)
                {
                    node.Right = RotateRight(node.Right);
                }
                node = RotateLeft(node);
            }
            while (GetBalanceFactor(node) < -1)
            {
                if (GetBalanceFactor(node.Left) > 0)
                {
                    node.Left = RotateLeft(node.Left);
                }
                node = RotateRight(node);
            }
            return node;
        }
        private void CorrectHeight(Node<T> node)
        {
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }
        private int GetBalanceFactor(Node<T> node)
        {
            return GetHeight(node.Right) - GetHeight(node.Left);
        }
        private int GetHeight(Node<T>? node)
        {
            return node == null ? 0 : node.Height;
        }
        private Node<T> RotateLeft(Node<T> node)
        {
            Node<T> right = node.Right;
            node.Right = right.Left;
            right.Left = node;
            CorrectHeight(node);
            CorrectHeight(right);
            return right;
        }
        private Node<T> RotateRight(Node<T> node)
        {
            Node<T> left = node.Left;
            node.Left = left.Right;
            left.Right = node;
            CorrectHeight(node);
            CorrectHeight(left);
            return left;
        }
        private void ToStringInternal(StringBuilder sb, string prefix, Node<T> node, bool isLeft)
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
    }
}
