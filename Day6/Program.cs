namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "File", "input.txt");
            string[] contentsMap = File.ReadAllLines(filePath);

            contentsMap = contentsMap.Reverse().ToArray();

            char[,] mapMatrix = new char[contentsMap.Length, contentsMap[0].Length];

            for (int verticalPos = 0; verticalPos < contentsMap.Length; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentsMap[verticalPos].Length; horizontalPos++)
                {
                    mapMatrix[horizontalPos, verticalPos] = contentsMap[verticalPos][horizontalPos];
                }
            }

            GuardGallivant guardGallivant = new GuardGallivant(mapMatrix);
            int distinctPositions = guardGallivant.IterateGuardPatrolAndReturnDistinctPositions(mapMatrix);

            Console.WriteLine($"The total of distinct positions is : {distinctPositions}");

            Console.ReadLine();
        }
    }
}
