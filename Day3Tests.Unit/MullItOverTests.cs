
namespace Day3Tests.Unit
{
    public class MullItOverTests
    {
        [Theory]
        [InlineData("sdadamul(1,3)sdasdasdas", 3)]
        [InlineData("select()(mul(669,636)-{mul(493,60)))", 455064)]
        public void GivenAMullString_ThenItIsCalculated_ReturnsTheProvidedValue(string mullText, int expectedValue)
        {
            int value = MullItOver(mullText);
            Assert.Equal(value, expectedValue);
        }

        private int MullItOver(string mullText)
        {
            return 3;
        }
    }
}