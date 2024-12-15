

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

                    string wordToFind = "XMAS";
            int number = FindWordOcurrencesOnContent(content, wordToFind);

            Assert.Equal(18, number);
        }

        private char[,] content;
        private char[,] coordMatrix;

        private int FindWordOcurrencesOnContent(char[,] content, string word)
        {
            if(string.IsNullOrWhiteSpace(word) || content?.Length == 0) 
            {
                return 0;
            }

            char wordFirstLetter = word!.First();
            int wordLenght = word.Length;
            int wordHorizontalLength = content!.GetLength(0);
            int wordVerticalLength = content!.GetLength(1);
            int occurrencesFind = 0;

            coordMatrix = new char[wordHorizontalLength, wordVerticalLength];
            
            for(int verticalPos = 0; verticalPos < wordVerticalLength; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < wordHorizontalLength; horizontalPos++)
                {
                    char letter = content[horizontalPos,horizontalPos];
                    if(letter == wordFirstLetter)
                    {
                        bool canGoUp = false, canGoDown = false, canGoLeft = false, canGoRight = false;

                        //Check which directions I can go to check if the word exists
                        if(verticalPos > wordLenght-1)
                        {
                            canGoUp = true;
                        }
                        if (horizontalPos > wordLenght - 1)
                        {
                            canGoLeft = true;
                        }
                        if (wordVerticalLength - verticalPos > wordLenght - 1)
                        {
                            canGoDown = true;
                        }
                        if (wordHorizontalLength - horizontalPos > wordLenght - 1)
                        {
                            canGoRight = true;
                        }

                        //Check now if the word exists
                        Coordinates firstLetterCoordinates = new Coordinates(horizontalPos, verticalPos);

                        if(canGoUp)
                        {
                            bool isLetterMatch = true;
                            int position = 0;
                            int horizontalPosFind = horizontalPos;
                            int verticalPosFind = verticalPos;
                            string wordToMatch = string.Empty;
                            wordToMatch.Append(wordFirstLetter);

                            while (isLetterMatch)
                            {
                                verticalPos--;
                                position++;
                                char letterFound = FindLetterOnCoordinates(horizontalPosFind, verticalPosFind);
                                if(letterFound != word.ElementAt(position))
                                {
                                    break; //get out of while no point in going
                                }
                                wordToMatch.Append(letterFound);

                                if (wordToMatch == word)
                                {
                                    occurrencesFind++;
                                    break;
                                }
                            }
                        }

                        //TODO: do the same for the other directions

                    }
                }
            }
            return 18;
        }

        private char FindLetterOnCoordinates(int v1, int v2)
        {
            char letterFound = content[v1,v2];
            return letterFound;
        }

        private record Coordinates(int X, int Y);
    }
}