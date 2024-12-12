using System.Text.RegularExpressions;

namespace Day3
{
    public class MulItOverOperations
    {
        public int MulItOverAndSum(string mullText)
        {
            var matches = Regex.Matches(mullText, @"mul\(\d+,\d+\)");
            int sumOfMul = 0;
            foreach (Match match in matches)
            {
                var numbers = Regex.Matches(match.Value, @"\d+");
                int.TryParse(numbers[0]?.Value, out int number1);
                int.TryParse(numbers[1]?.Value, out int number2);
                sumOfMul += number1 * number2;
            }
            return sumOfMul;
        }

        public int MulItOverWithControlAndSum(string mullTextWithControl)
        {
            var mulRegex = new Regex(@"mul\((\d+),(\d+)\)");
            var controlRegex = new Regex(@"do\(\)|don't\(\)");

            bool isEnabledMul = true;

            var tokens = Regex.Split(mullTextWithControl, @"(?=mul\(|do\(\)|don't\(\))");

            int sumOfMul = 0;

            foreach(var token in tokens)
            {
                Console.WriteLine($"token: {token}");

                if (controlRegex.IsMatch(token) && token.Contains("do()"))
                {
                    isEnabledMul = true;
                    continue;
                }
                else if(controlRegex.IsMatch(token) && token.Contains("don't()"))
                {
                    isEnabledMul = false;
                }

                if (isEnabledMul && mulRegex.IsMatch(token)) 
                {
                    var numbers = Regex.Matches(token, @"\d+");
                    int.TryParse(numbers[0]?.Value, out int number1);
                    int.TryParse(numbers[1]?.Value, out int number2);
                    sumOfMul += number1 * number2;
                }
            }

            return sumOfMul;
        }
    }
}
