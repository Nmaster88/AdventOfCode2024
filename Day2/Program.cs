namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "File", "input.txt");
            string[] contents = File.ReadAllLines(filePath);

            RedNosedReport redNosedReport = new RedNosedReport();
            int count = redNosedReport.SafeRedNosedCount(contents);

            Console.WriteLine($"the count of safeRedNosed is {count}");

            int countWithTolerantFault = redNosedReport.SafeRedNosedWithTolerantFaultCount(contents);

            Console.WriteLine($"the countWithTolerantFault of safeRedNosed is {countWithTolerantFault}");

            Console.ReadKey();
        }
    }
}
