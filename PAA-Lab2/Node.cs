namespace PAA_Lab2
{
    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
            Height = 1;
        }

        public T Value;
        public Node<T>? Left;
        public Node<T>? Right;
        public int Height;

        public void CorrectHeight()
        {
            Height = 1 + Math.Max(GetHeight(Left), GetHeight(Right));
        }
        public int GetBalanceFactor()
        {
            return GetHeight(Right) - GetHeight(Left);
        }
        private static int GetHeight(Node<T>? node)
        {
            return node == null ? 0 : node.Height;
        }
    }
}
