using System.Text;

namespace PAA_Lab2
{
    public class BinaryTree<T> where T : IComparable
    {
        private Node<T> _root;
        private int _balancingStep;

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
            _balancingStep = 0;
            BalanceInternal(_root, null);
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
        private void BalanceInternal(Node<T> node, Node<T>? parent)
        {
            if (node.Right != null)
            {
                BalanceInternal(node.Right, node);
            }
            if (node.Left != null)
            {
                BalanceInternal(node.Left, node);
            }
            while (GetBalanceFactor(node) > 1)
            {
                bool doubleRotate = false;
                if (GetBalanceFactor(node.Right!) < 0)
                {
                    node.Right = RotateRight(node.Right!);
                    doubleRotate = true;
                }
                node = ReplaceChildOfNodeOrRoot(parent, node, RotateLeft(node));
                WriteBalancingStep(doubleRotate ? "right-left" : "left", node);
            }
            while (GetBalanceFactor(node) < -1)
            {
                bool doubleRotate = false;
                if (GetBalanceFactor(node.Left!) > 0)
                {
                    node.Left = RotateLeft(node.Left!);
                    doubleRotate = true;
                }
                node = ReplaceChildOfNodeOrRoot(parent, node, RotateRight(node));
                WriteBalancingStep(doubleRotate ? "left-right" : "right", node);
            }
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
        private Node<T> ReplaceChildOfNodeOrRoot(Node<T>? parent, Node<T> child, Node<T> newChild)
        {
            if (parent != null)
            {
                if (parent.Left == child)
                {
                    parent.Left = newChild;
                    return parent.Left;
                }
                parent.Right = newChild;
                return parent.Right;
            }
            _root = newChild;
            return _root;
        }
        private Node<T> RotateLeft(Node<T> node)
        {
            Node<T> right = node.Right!;
            node.Right = right!.Left;
            right.Left = node;
            CorrectHeight(node);
            CorrectHeight(right);
            return right;
        }
        private Node<T> RotateRight(Node<T> node)
        {
            Node<T> left = node.Left!;
            node.Left = left!.Right;
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
        private void WriteBalancingStep(string rotationType, Node<T> node)
        {
            Console.WriteLine($"Step {++_balancingStep} ({rotationType} rotation on {node.Value}):");
            Writer.WriteDivider();
            Console.Write(this);
            Writer.WriteDivider();
        }
    }
}
