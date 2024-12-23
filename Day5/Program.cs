namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "File", "input.txt");
            string[] contents = File.ReadAllLines(filePath);

            List<string> pageOrderingRules = new List<string>();
            foreach (string line in contents.Where(t => t.Contains("|")))
            {
                pageOrderingRules.Add(line);
            }

            List<string> pageNumbersForUpdate = new List<string>();
            foreach (string line in contents.Where(t => t.Contains(",")))
            {
                pageNumbersForUpdate.Add(line);
            }

            Console.WriteLine("Hello, World!");
        }
    }
}
