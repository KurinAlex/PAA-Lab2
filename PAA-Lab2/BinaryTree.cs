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
            int balancingStep = 0;
            BalanceInternal(_root, null, ref balancingStep);
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
            node.CorrectHeight();
        }
        private void BalanceInternal(Node<T> node, Node<T>? parent, ref int balancingStep)
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
            node.CorrectHeight();
            right.CorrectHeight();
            return right;
        }
        private Node<T> RotateRight(Node<T> node)
        {
            Node<T> left = node.Left!;
            node.Left = left!.Right;
            left.Right = node;
            node.CorrectHeight();
            left.CorrectHeight();
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
        private void WriteBalancingStep(string rotationType, Node<T> node, ref int balancingStep)
        {
            Console.WriteLine($"Step {++balancingStep} ({rotationType} rotation on {node.Value}):");
            Writer.WriteDivider();
            Console.Write(this);
            Writer.WriteDivider();
        }
    }
}
