namespace Day5Tests.Unit
{
    public class UnitTest1
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

            List<string[]> pageOrderingRules = new List<string[]>();
            foreach (string line in contents.Where(t => t.Contains("|")))
            {
                pageOrderingRules.Add(line.Split('|'));
            }

            List<string[]> pageNumbersForUpdate = new List<string[]>();
            foreach (string line in contents.Where(t => t.Contains(",")))
            {
                pageNumbersForUpdate.Add(line.Split(','));
            }

            int calculateSum = CheckSum(pageOrderingRules, pageNumbersForUpdate);

            var result = 143;

            Assert.Equal(143, result);
        }

        private int CheckSum(List<string[]> pageOrderingRules, List<string[]> pageNumbersForUpdate)
        {
            int numbersForUpdateLength = pageNumbersForUpdate.Count;

            foreach (string[] numberForUpdate in pageNumbersForUpdate) 
            { 
                for (int i = 0; i < numbersForUpdateLength; i++) 
                {
                    
                }
            }
            throw new NotImplementedException();
        }

        private bool CheckRules(string[] numberForUpdates, int index, List<string[]> pageOrderingRules)
        {
            

            return true;
        }
    }
}