namespace PAA_Lab2
{
    public class Program
    {
        static readonly int rootValue = 15;
        static readonly int[] values = { 6, 1, 7, 28, 3, 27, 0, 25, 24, 23, 16, 21, 19, 10, 4 };

        static void Main(string[] args)
        {
            Writer.WriteDivider();
            Console.WriteLine("Binary tree with unsorted values:");
            Writer.WriteDivider();
            BinaryTree<int> unsortedValuesTree = new(rootValue, values);
            Console.Write(unsortedValuesTree);
            Writer.WriteDivider();

            Console.WriteLine("Binary tree with sorted values:");
            Writer.WriteDivider();
            HeapSorter.Sort(values);
            BinaryTree<int> sortedValuesTree = new(rootValue, values);
            Console.Write(sortedValuesTree);
            Writer.WriteDivider();

            Console.WriteLine("Balancing binary tree:");
            Writer.WriteDivider();
            unsortedValuesTree.Balance();

            Console.WriteLine("Balanced binary tree:");
            Writer.WriteDivider();
            Console.Write(unsortedValuesTree);
            Writer.WriteDivider();

            Console.ReadKey();
        }
    }
}