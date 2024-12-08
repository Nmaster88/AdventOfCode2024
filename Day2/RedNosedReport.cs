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

            bool isIncreasing = true;
            int prevValue = noseValues[0];

            for (int i = 1; i < noseValues.Count; i++)
            {
                int currValue = noseValues[i];

                if(i == 1 && currValue <= prevValue)
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

            bool isIncreasing = true, tolerateFaultUsed = false;
            int prevValue = noseValues[0];

            for (int i = 1; i < noseValues.Count; i++)
            {
                int currValue = noseValues[i];
                int distance = Math.Abs(currValue - prevValue);

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

                if (InvalidDistance(distance))
                {
                    return false;
                }

                prevValue = currValue;
            }

            return true;
        }

        private bool ValidDistance(int distance) => (distance >= _minDistance && distance <= _maxDistance);
        private bool InvalidDistance(int distance) => (distance < _minDistance || distance > _maxDistance);
    }
}
