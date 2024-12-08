
using Day2;

namespace Day2Tests.Unit
{
    public class RedNosedReportTests
    {
        [Theory]
        [InlineData("1 2 3 4 5")]
        [InlineData("1 3 5 7 9")]
        [InlineData("7 4 3")]
        public void GivenTheFollowingSafeInputs_ThenSafeRedNose_ReturnsTrue(string value)
        {
            RedNosedReport redNosedReport = new RedNosedReport();
            bool result = redNosedReport.SafeRedNosed(value);
            Assert.True(result);
        }

        [Theory]
        [InlineData("1 4 8 10 20")]
        [InlineData("1 3 1 3 1")]
        [InlineData("7 1")]
        public void GivenTheFollowingUnSafeInputs_ThenSafeRedNose_ReturnsTrue(string value)
        {
            RedNosedReport redNosedReport = new RedNosedReport();
            bool result = redNosedReport.SafeRedNosed(value);
            Assert.False(result);
        }

        [Fact]
        public void GivenTheFollowingSafeInputs_ThenSafeRedNosedCount_ReturnsThree()
        {
            string[] content = { "1 2 3 4 5", "1 3 5 7 9", "7 4 3" };

            RedNosedReport redNosedReport = new RedNosedReport();
            int count = redNosedReport.SafeRedNosedCount(content);

            Assert.Equal(3, count);
        }
    }
}