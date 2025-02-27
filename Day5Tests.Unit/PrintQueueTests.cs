using Day5;

namespace Day5Tests.Unit
{
    public class PrintQueueTests
    {
        [Fact]
        public void CheckEachPageNumbersToUpdateAndCalculateSum_ReturnsExpectedCount_ForGivenInput()
        {
            string[] contents =
            {
                "47|53",
                "97|13",
                "97|61",
                "97|47",
                "75|29",
                "61|13",
                "75|53",
                "29|13",
                "97|29",
                "53|29",
                "61|53",
                "97|53",
                "61|29",
                "47|13",
                "75|47",
                "97|75",
                "47|61",
                "75|61",
                "47|29",
                "75|13",
                "53|13",
                "",
                "75,47,61,53,29",
                "97,61,53,29,13",
                "75,29,13",
                "75,97,47,61,53",
                "61,13,29",
                "97,13,75,29,47"
            };

            Dictionary<int,List<int>> pageOrderingRules = new Dictionary<int, List<int>>();
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

            Assert.Equal(143, calculateSum);
        }

        [Fact]
        public void CheckEachIncorrectlyOrderPageNumbersToUpdateAndCalculateSum_ReturnsExpectedCount_ForGivenInput()
        {
            string[] contents =
            {
                "47|53",
                "97|13",
                "97|61",
                "97|47",
                "75|29",
                "61|13",
                "75|53",
                "29|13",
                "97|29",
                "53|29",
                "61|53",
                "97|53",
                "61|29",
                "47|13",
                "75|47",
                "97|75",
                "47|61",
                "75|61",
                "47|29",
                "75|13",
                "53|13",
                "",
                "75,47,61,53,29",
                "97,61,53,29,13",
                "75,29,13",
                "75,97,47,61,53",
                "61,13,29",
                "97,13,75,29,47"
            };

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

            int calculateSum = printQueue.CheckEachIncorrectlyOrderPageNumbersToUpdateAndCalculateSum(pageOrderingRules, pageNumbersForUpdate);

            Assert.Equal(123, calculateSum);
        }

        [Fact]
        public void NumberSequenceInvalidFixed_ReturnsExpectValue_ForInvalidSequence()
        {
            string[] contents =
{
                "47|53",
                "97|13",
                "97|61",
                "97|47",
                "75|29",
                "61|13",
                "75|53",
                "29|13",
                "97|29",
                "53|29",
                "61|53",
                "97|53",
                "61|29",
                "47|13",
                "75|47",
                "97|75",
                "47|61",
                "75|61",
                "47|29",
                "75|13",
                "53|13",
                "",
                "75,47,61,53,29",
                "97,61,53,29,13",
                "75,29,13",
                "75,97,47,61,53",
                "61,13,29",
                "97,13,75,29,47"
            };

            Dictionary<int, List<int>> pageOrderingRules = new Dictionary<int, List<int>>();
            pageOrderingRules = contents.Where(t => t.Contains("|"))
                .Select(entry => entry.Split('|'))
                .GroupBy(parts => parts[0], parts => parts[1])
                .ToDictionary(group => int.Parse(group.Key), group => group.Select(int.Parse).ToList());

            PrintQueue printQueue = new PrintQueue();

            int[] numberSequenceOne = [75, 97, 47, 61, 53];
            int[] resultOne = printQueue.NumberSequenceInvalidFixed(numberSequenceOne, 0, pageOrderingRules);
            int[] expectedResultOne = [97, 75, 47, 61, 53];
            Assert.Equal(expectedResultOne, resultOne);

            int[] numberSequenceTwo = [61, 13, 29];
            int[] resultTwo = printQueue.NumberSequenceInvalidFixed(numberSequenceTwo, 0, pageOrderingRules);
            int[] expectedResultTwo = [61, 29, 13];
            Assert.Equal(expectedResultTwo, resultTwo);

            int[] numberSequenceThree = [97, 13, 75, 29, 47];
            int[] resultThree = printQueue.NumberSequenceInvalidFixed(numberSequenceThree, 0, pageOrderingRules);
            int[] expectedResultThree = [97, 75, 47, 29, 13];
            Assert.Equal(expectedResultThree, resultThree);
        }
    }
}