namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "File", "input.txt");
            string[] contents = File.ReadAllLines(filePath);
        }
    }
}
