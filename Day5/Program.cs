namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "File", "input.txt");
            string[] contents = File.ReadAllLines(filePath);

            Dictionary<int, List<int>> pageOrderingRules = new Dictionary<int, List<int>>();
            pageOrderingRules = contents.Where(t => t.Contains("|"))
                .Select(entry => entry.Split('|'))
                .GroupBy(parts => parts[0], parts => parts[1])
                .ToDictionary(group => int.Parse(group.Key), group => group.Select(int.Parse).ToList());

            List<int[]> pageNumbersForUpdate = new List<int[]>();
            foreach (string line in contents.Where(t => t.Contains(",")))
            {
                pageNumbersForUpdate.Add(line.Split(',').Select(int.Parse).ToArray());
            }

            PrintQueue printQueue = new PrintQueue();
            int calculateSum = printQueue.CheckEachPageNumbersToUpdateAndCalculateSum(pageOrderingRules, pageNumbersForUpdate);

            Console.WriteLine($"The sum of the print queue is : {calculateSum}");

            int calculateSumIncorrectlyOrdered = printQueue.CheckEachIncorrectlyOrderPageNumbersToUpdateAndCalculateSum(pageOrderingRules, pageNumbersForUpdate);

            Console.WriteLine($"The sum of incorrect sequences of the print queue is : {calculateSumIncorrectlyOrdered}");

            Console.ReadLine();
        }
    }
}
