using System.Runtime.CompilerServices;

namespace Day2
{
    public class RedNosedReport
    {
        private const int _minDistance = 1;
        private const int _maxDistance = 3;

        public bool SafeRedNosed(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            List<int> noseValues = value.Split(" ").Select(n => int.Parse(n!)).ToList();

            return IsSafeReport(noseValues);
        }

        public int SafeRedNosedCount(string[] content)
        {
            int count = 0;
            foreach (string s in content) 
            {
                if(SafeRedNosed(s)) 
                {
                    count++;
                }
            }
            return count;
        }

        public bool SafeRedNosedWithTolerateFault(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            List<int> noseValues = value.Split(" ").Select(n => int.Parse(n!)).ToList();

            if(IsSafeReport(noseValues))
            {
                return true;
            }

            for (int i = 0; i < noseValues.Count; i++)
            {
                var reducedValues = noseValues.Where((_, index) => index != i).ToList();
                if (IsSafeReport(reducedValues))
                    return true;
            }

            return false;
        }

        private bool IsSafeReport(List<int> reportValues)
        {
            bool isIncreasing = true;
            int prevValue = reportValues[0];

            for (int i = 1; i < reportValues.Count; i++)
            {
                int currValue = reportValues[i];

                if (i == 1 && currValue <= prevValue)
                {
                    isIncreasing = false;
                }
                else if (i == 1 && currValue >= prevValue)
                {
                    isIncreasing = true;
                }

                if (isIncreasing && currValue <= prevValue)
                {
                    return false;
                }
                else if (!isIncreasing && currValue >= prevValue)
                {
                    return false;
                }

                if (Math.Abs(currValue - prevValue) < _minDistance || Math.Abs(currValue - prevValue) > _maxDistance)
                {
                    return false;
                }

                prevValue = currValue;
            }

            return true;
        }

        public int SafeRedNosedWithTolerantFaultCount(string[] content)
        {
            int count = 0;
            foreach (string s in content)
            {
                if (SafeRedNosedWithTolerateFault(s))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
