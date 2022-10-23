namespace PAA_Lab2
{
    public static class HeapSorter
    {
        public static void Sort<T>(T[] array) where T : IComparable
        {
            int n = array.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                DownHeap(array, n, i);
            }
            for (int i = n - 1; i > 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                DownHeap(array, i, 0);
            }
        }

        private static void DownHeap<T>(T[] array, int n, int i) where T : IComparable
        {
            int largest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && array[l].CompareTo(array[largest]) > 0)
            {
                largest = l;
            }

            if (r < n && array[r].CompareTo(array[largest]) > 0)
            {
                largest = r;
            }

            if (largest != i)
            {
                (array[i], array[largest]) = (array[largest], array[i]);
                DownHeap(array, n, largest);
            }
        }
    }
}
