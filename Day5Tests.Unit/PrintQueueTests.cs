using Day5;

namespace Day5Tests.Unit
{
    public class PrintQueueTests
    {
        [Fact]
        public void OnContent_ReturnsExpectedCount_ForGivenInput()
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
    }
}