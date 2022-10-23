namespace PAA_Lab2
{
    public class Program
    {
        static readonly int rootValue = 15;
        static readonly int[] values = { 6, 1, 7, 28, 3, 27, 0, 25, 24, 23, 16, 21, 19, 10, 4 };

        static void WriteDivider()
        {
            Console.WriteLine(new string('-', 50));
        }

        static void Main(string[] args)
        {
            WriteDivider();
            Console.WriteLine("Binary tree with unsorted values:");
            WriteDivider();
            BinaryTree<int> unsortedValuesTree = new(rootValue, values);
            Console.Write(unsortedValuesTree);
            WriteDivider();

            Console.WriteLine("Binary tree with sorted values:");
            WriteDivider();
            HeapSorter.Sort(values);
            BinaryTree<int> sortedValuesTree = new(rootValue, values);
            Console.Write(sortedValuesTree);
            WriteDivider();

            Console.WriteLine("Balanced binary tree:");
            WriteDivider();
            unsortedValuesTree.Balance();
            Console.Write(unsortedValuesTree);
            WriteDivider();

            Console.ReadKey();
        }
    }
}