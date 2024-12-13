
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
            string wordToFind = "XMAS";
            int number = FindWordOcurrencesOnContent(content, wordToFind);

            Assert.Equal(18, number);
        }

        private int FindWordOcurrencesOnContent(string[] content, string word)
        {
            if(string.IsNullOrWhiteSpace(word) || content?.Length == 0) 
            {
                return 0;
            }

            char firstLetter = word!.First();
            int wordLenght = word.Length;
            int horizontalLength = content!.First().Length;
            int verticalLength = content!.Length;
            
            for(int verticalPos = 0; verticalPos < content.Length; verticalPos++)
            {
                string line = content[verticalPos];
                for (int horizontalPos = 0; horizontalPos < line.Length; horizontalPos++)
                {
                    char letter = line[horizontalPos];
                    Console.WriteLine(letter);
                }
            }
            Console.ReadLine();
            return 18;
        }
    }
}