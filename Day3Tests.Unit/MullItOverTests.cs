
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using Day3;

namespace Day3Tests.Unit
{
    public class MullItOverTests
    {
        [Theory]
        [InlineData("sdadamul(1,3)sdasdasdas", 3)]
        [InlineData("select()(mul(669,636)-{mul(493,60)))", 455064)]
        public void GivenAMullString_ThenItIsCalculatedWithMulItOverAndSum_ReturnsTheProvidedValue(string mullText, int expectedValue)
        {
            MulItOverOperations mulItOverOperations = new MulItOverOperations();
            int value = mulItOverOperations.MulItOverAndSum(mullText);
            Assert.Equal(value, expectedValue);
        }

        [Theory]
        [InlineData("sdadamul(1,3)sddon't()asdamul(1,3)sdado()smul(1,3)", 6)]
        [InlineData("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", 48)]
        public void GivenAMullString_ThenItIsCalculatedWithMulItOverWithControlAndSum_ReturnsTheProvidedValue(string mullText, int expectedValue)
        {
            MulItOverOperations mulItOverOperations = new MulItOverOperations();
            int value = mulItOverOperations.MulItOverWithControlAndSum(mullText);
            Assert.Equal(value, expectedValue);
        }
    }
}