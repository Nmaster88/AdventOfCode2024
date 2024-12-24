namespace Day5
{
    public class PrintQueue
    {
        public int CheckEachPageNumbersToUpdateAndCalculateSum(Dictionary<int, List<int>> pageOrderingRules, List<int[]> pageNumbersForUpdate)
        {
            int calculateSum = 0;

            foreach (int[] numbersForUpdate in pageNumbersForUpdate)
            {
                bool sequenceIsValid = false;
                for (int i = 0; i < numbersForUpdate.Length; i++)
                {
                    var numberToCheck = numbersForUpdate[i];
                    sequenceIsValid = IsRulesForNumberSequenceValid(numbersForUpdate, i, pageOrderingRules);
                    if (sequenceIsValid is not true)
                    {
                        break;
                    }
                }

                if (sequenceIsValid)
                {
                    calculateSum += numbersForUpdate[numbersForUpdate.Length / 2];
                }
            }

            return calculateSum;
        }

        private bool IsRulesForNumberSequenceValid(int[] numbersForUpdates, int index, Dictionary<int, List<int>> pageOrderingRules)
        {
            int numberToCompare = numbersForUpdates[index];

            if (index < numbersForUpdates.Length - 1)
            {
                List<int> numberThatNeedToBeAfter = new List<int>();
                if (pageOrderingRules.TryGetValue(numberToCompare, out var valuesRetrieved))
                {
                    // The key exists, and numberThatNeedToBeAfter now contains the associated List<int>.
                    numberThatNeedToBeAfter.AddRange(valuesRetrieved);
                }
                for (int i = index + 1; i < numbersForUpdates.Length; i++)
                {
                    var numberAfter = numbersForUpdates[i];
                    bool itIsAfter = numberThatNeedToBeAfter.Contains(numberAfter);
                    if (itIsAfter is not true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
