
using Day4;

namespace Day4Tests.Unit
{
    public class CeresSearchTests
    {
        [Fact]
        public void GivenTheFollowingTextLines_WhenTheXMASWordsAreFind_ThenItReturnsTheRightNumber()
        {
            string[] contentInput = [
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

            CeresSearch ceresSearch = new CeresSearch();

            char[,] content = new char[contentInput.Length, contentInput[0].Length];

            for (int verticalPos = 0; verticalPos < contentInput.Length; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentInput[verticalPos].Length; horizontalPos++)
                {
                    content[horizontalPos, verticalPos] = contentInput[verticalPos][horizontalPos];
                }
            }
            string wordToFind = "XMAS";
            int number = ceresSearch.FindWordOcurrencesOnContent(content, wordToFind);

            Assert.Equal(18, number);
        }

        public void GivenTheFollowingTextLines_WhenTheX_MASWordsAreFind_ThenItReturnsTheRightNumber()
        {
            string[] contentInput = [
                ".M.S......",
                "..A..MSMS.",
                ".M.S.MAA..",
                "..A.ASMSM.",
                ".M.S.M....",
                "..........",
                "S.S.S.S.S.",
                ".A.A.A.A..",
                "M.M.M.M.M.",
                ".........."
            ];

            CeresSearch ceresSearch = new CeresSearch();

            char[,] content = new char[contentInput.Length, contentInput[0].Length];

            for (int verticalPos = 0; verticalPos < contentInput.Length; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentInput[verticalPos].Length; horizontalPos++)
                {
                    content[horizontalPos, verticalPos] = contentInput[verticalPos][horizontalPos];
                }
            }

            int number = ceresSearch.FindXMASOcurrencesOnContent(content, wordToFind);

            Assert.Equal(9, number);
        }
    }
}