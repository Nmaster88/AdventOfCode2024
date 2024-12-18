﻿namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "File", "input.txt");
            string[] contents = File.ReadAllLines(filePath);
            char[,] content; content = new char[contents.Length, contents[0].Length];
            string wordToFind = "XMAS";

            for (int verticalPos = 0; verticalPos < contents.Length; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contents[verticalPos].Length; horizontalPos++)
                {
                    content[horizontalPos, verticalPos] = contents[verticalPos][horizontalPos];
                }
            }


        }
    }
}
