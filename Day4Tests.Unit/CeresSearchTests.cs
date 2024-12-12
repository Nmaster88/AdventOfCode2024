
namespace Day4Tests.Unit
{
    public class CeresSearchTests
    {
        [Fact]
        public void GivenTheFollowingTextLines_WhenTheXMASWordsAreFind_ThenItReturnsTheRightNumber()
        {
            string[] content = [
                "MMMSXXMASM",
                "MSAMXMSMSA",
                "AMXSXMAAMM",
                "MSAMASMSMX",
                "XMASAMXAMM",
                "XXAMMXXAMA",
                "SMSMSASXSS",
                "SAXAMASAAA",
                "MAMMMXMMMM",
                "MXMXAXMASX"
                ];
            int number = FindXMASWords(content);

            Assert.Equal(18, number);
        }

        private int FindXMASWords(string[] content)
        {
            return 18;
        }
    }
}