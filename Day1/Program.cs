namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "File", "input.txt");
            string[] contents = File.ReadAllLines(filePath);

            List<int> listOne = new List<int>(); 
            List<int> listTwo = new List<int>(); 

            foreach(string content in contents) 
            {
                var arr = content.Split("  ");
                listOne.Add(int.Parse(arr[0]));
                listTwo.Add(int.Parse(arr[1]));
            }

            HistorianHysteria historianHysteria = new HistorianHysteria();
            var value = historianHysteria.CalculateDistanceFromLists(listOne, listTwo);

            Console.WriteLine($"The calculatred distance from lists is {value}");

            var valueTwo = historianHysteria.SimilarityScoreFromLists(listOne, listTwo);

            Console.WriteLine($"The similarity score between the lists is {valueTwo}");

            Console.ReadKey();
        }
    }
}
