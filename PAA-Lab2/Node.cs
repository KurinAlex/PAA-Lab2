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
    }
}
