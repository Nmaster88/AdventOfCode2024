
using System.Text;

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

            content = new char[contentInput.Length, contentInput[0].Length];

            for (int verticalPos = 0; verticalPos < contentInput.Length; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentInput[verticalPos].Length; horizontalPos++)
                {
                    content[horizontalPos, verticalPos] = contentInput[verticalPos][horizontalPos];
                }
            }

            wordToFind = "XMAS";
            int number = FindWordOcurrencesOnContent(content);

            Assert.Equal(18, number);
        }

        private char[,] content;
        private char[,] coordMatrix;
        private string wordToFind = string.Empty;
        private int occurrencesFind = 0;
        private record Coordinates(int X, int Y);

        private record AllowedDirections(bool Up, bool Right, bool Down, bool Left);

        private int FindWordOcurrencesOnContent(char[,] content)
        {
            if(string.IsNullOrWhiteSpace(wordToFind) || content?.Length == 0) 
            {
                return 0;
            }

            char wordFirstLetter = wordToFind!.First();
            int wordLenght = wordToFind.Length;
            int contentHorizontalLength = content!.GetLength(0);
            int contentVerticalLength = content!.GetLength(1);

            coordMatrix = new char[contentHorizontalLength, contentVerticalLength];
            
            for(int verticalPos = 0; verticalPos < contentVerticalLength; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentHorizontalLength; horizontalPos++)
                {
                    Coordinates currentCoordinates = new Coordinates(horizontalPos, verticalPos);
                    List<(int, int)> directionsMultiplier = BuildDirectionsMultiplier(currentCoordinates, wordLenght, contentHorizontalLength, contentVerticalLength);

                    char letter = content[horizontalPos,horizontalPos];
                    if(letter == wordFirstLetter)
                    {
                        Coordinates firstLetterFromWordCoordinates = new Coordinates(horizontalPos, verticalPos);

                        WordFind(firstLetterFromWordCoordinates, directionsMultiplier);
                    }
                }
            }
            return 18;
        }

        private List<(int, int)> BuildDirectionsMultiplier(Coordinates currentCoordinates, int wordLength, int contentHorizontalLength, int contentVerticalLength)
        {
            int wordLastIndex = wordLength - 1;

            return AllDirections.Where(direction =>
            {
                int endX = currentCoordinates.X + direction.dx * wordLastIndex;
                int endY = currentCoordinates.Y + direction.dy * wordLastIndex;

                return endX >= 0 && endX < contentHorizontalLength && endY >= 0 && endY < contentVerticalLength;
            }).ToList();
        }

        private static readonly (int dx, int dy)[] AllDirections =
        {
            (0, 1),   // Up
            (1, 1),   // Up-Right
            (1, 0),   // Right
            (1, -1),  // Down-Right
            (0, -1),  // Down
            (-1, -1), // Down-Left
            (-1, 0),  // Left
            (-1, 1)   // Up-Left
        };

        private void WordFind(Coordinates firstLetterCoordinates, List<(int, int)> directionsMultiplier)
        {
            char wordFirstLetter = wordToFind!.First();
            StringBuilder wordToMatch = new StringBuilder();
            wordToMatch.Append(wordFirstLetter);
            int contentHorizontalLength = content!.GetLength(0);
            int contentVerticalLength = content!.GetLength(1);

            foreach (var directionMultiplier in directionsMultiplier) 
            {
                bool isLetterMatch = true;
                int horizontalPosFind = firstLetterCoordinates.X;
                int verticalPosFind = firstLetterCoordinates.Y;

                while (isLetterMatch)
                {
                    horizontalPosFind = horizontalPosFind + directionMultiplier.Item1;
                    verticalPosFind = verticalPosFind + directionMultiplier.Item2;
                    if(horizontalPosFind < 0 && horizontalPosFind >= contentHorizontalLength && verticalPosFind < 0 && verticalPosFind >= contentVerticalLength)
                    {
                        break;
                    }

                    int position = 0;
                    position++;
                    char letterFound = FindLetterOnCoordinates(horizontalPosFind, verticalPosFind);
                    if (letterFound != wordToFind.ElementAt(position))
                    {
                        break;
                    }
                    wordToMatch.Append(letterFound);

                    if (wordToMatch.ToString() == wordToFind)
                    {
                        occurrencesFind++;
                        break;
                    }
                }
            }
        }

        private char FindLetterOnCoordinates(int v1, int v2)
        {
            char letterFound = content[v1,v2];
            return letterFound;
        }
    }
}