using Day1;

namespace Day1Tests.Unit
{
    public class HistorianHysteriaTests
    {
        [Fact]
        public void List1423AndList3452_Returns()
        {
            List<int> listOne = new List<int>() { 1, 4, 2, 3 };
            List<int> listTwo = new List<int>() { 3, 4, 5, 2 };
            HistorianHysteria historianHysteria = new HistorianHysteria();
            var result = historianHysteria.CalculateDistanceFromLists(listOne, listTwo);
            Assert.Equal(4, result);
        }
    }
}