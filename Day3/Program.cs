﻿namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "File", "input.txt");
            string[] contents = File.ReadAllLines(filePath);

            MulItOverOperations mulItOverOperations = new MulItOverOperations();

            int sumOfMul = 0;
            foreach (string line in contents) 
            {
                sumOfMul += mulItOverOperations.MulItOverAndSum(line);
            }

            Console.WriteLine($"the sumOfMul of MulItOverAndSum is {sumOfMul}");

            int sumOfMulWithControl = 0;

            sumOfMulWithControl += mulItOverOperations.MulItOverWithControlAndSum(string.Join(" ", contents));

            Console.WriteLine($"the sumOfMul of MulItOverWithControlAndSum is {sumOfMulWithControl}");

            Console.ReadKey();
        }
    }
}
