namespace PAA_Lab2
{
    public static class Writer
    {
        private const int DividerLength = 50;
        private static readonly string s_divider = new('-', DividerLength);

        public static void WriteDivider()
        {
            Console.WriteLine(s_divider);
        }
    }
}
