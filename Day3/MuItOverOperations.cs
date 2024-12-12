using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


            foreach (var token in tokens)
            {
                if (controlRegex.IsMatch(token) && token.Contains("do()"))
                {
                    isEnabledMul = true;
                    continue;
                }
                else if (controlRegex.IsMatch(token) && token.Contains("don't()"))
                {
                    isEnabledMul = false;
                    continue;
                }

                if (isEnabledMul && mulRegex.IsMatch(token))
                {
                    sumOfMul += DigitCalculation(token);
                }
            }

            return sumOfMul;
        }

        private MulControlStates MulControlStateTransition(bool isDoState,bool isDontState)
        {
            MulControlStates state = MulControlStates.None;

            if (isDoState && !isDontState)
            {
                state = MulControlStates.DoFirst;
            }
            else if (!isDoState && isDontState)
            {
                state = MulControlStates.DontFirst;
            }
            else if (isDoState && isDontState && state == MulControlStates.DoFirst)
            {
                state = MulControlStates.DontSecond;
            }
            else if (isDoState && isDontState && state == MulControlStates.DontFirst)
            {
                state = MulControlStates.DoSecond;
            }

            return state;
        }

        private enum MulControlStates
        {
            None,
            DontFirst,
            DoFirst,
            DontSecond,
            DoSecond
        }

        private int DigitCalculation(string text)
        {
            var numbers = Regex.Matches(text, @"\d+");
            int.TryParse(numbers[0]?.Value, out int number1);
            int.TryParse(numbers[1]?.Value, out int number2);
            return number1 * number2;
        }
    }
}
