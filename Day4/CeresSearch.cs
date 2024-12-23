using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace Day4
{
    public class CeresSearch
    {
        private string wordToFind = string.Empty;
        private record Coordinates(int X, int Y);
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

        public int FindWordOcurrencesOnContent(char[,] contentInput, string word)
        {
            wordToFind = word;

            if (string.IsNullOrWhiteSpace(wordToFind) || contentInput?.Length == 0)
            {
                return 0;
            }

            char wordFirstLetter = wordToFind!.First();
            int wordLenght = wordToFind.Length;
            int contentHorizontalLength = contentInput!.GetLength(0);
            int contentVerticalLength = contentInput!.GetLength(1);
            int occurrencesCount = 0;

            for (int verticalPos = 0; verticalPos < contentVerticalLength; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentHorizontalLength; horizontalPos++)
                {
                    Coordinates currentCoordinates = new Coordinates(horizontalPos, verticalPos);
                    List<(int, int)> directionsMultiplier = BuildDirectionsMultiplier(currentCoordinates, wordLenght, contentHorizontalLength, contentVerticalLength);

                    char letter = contentInput[horizontalPos, verticalPos];
                    if (letter == wordFirstLetter)
                    {
                        occurrencesCount += WordFind(contentInput, currentCoordinates, directionsMultiplier);
                    }
                }
            }
            return occurrencesCount;
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

        private int WordFind(char[,] content, Coordinates firstLetterCoordinates, List<(int, int)> directionsMultiplier)
        {
            char wordFirstLetter = wordToFind!.First();
            int contentHorizontalLength = content!.GetLength(0);
            int contentVerticalLength = content!.GetLength(1);
            int occurrencesCount = 0;

            foreach (var directionMultiplier in directionsMultiplier)
            {
                bool isLetterMatch = true;
                int horizontalPosFind = firstLetterCoordinates.X;
                int verticalPosFind = firstLetterCoordinates.Y;
                int position = 0;
                StringBuilder wordToMatch = new StringBuilder();
                wordToMatch.Append(wordFirstLetter);

                while (isLetterMatch)
                {
                    horizontalPosFind = horizontalPosFind + directionMultiplier.Item1;
                    verticalPosFind = verticalPosFind + directionMultiplier.Item2;
                    position++;
                    if ((horizontalPosFind < 0 || horizontalPosFind == contentHorizontalLength) ||
                        (verticalPosFind < 0 || verticalPosFind == contentVerticalLength) ||
                        position == wordToFind.Length)
                    {
                        isLetterMatch = false;
                        continue;
                    }
                    Coordinates letterCoordinates = new Coordinates(horizontalPosFind, verticalPosFind);
                    char letterFound = FindLetterOnCoordinates(content, letterCoordinates);
                    if (letterFound != wordToFind.ElementAt(position))
                    {
                        isLetterMatch = false;
                        continue;
                    }
                    wordToMatch.Append(letterFound);
                    string wordToMatchBuilt = wordToMatch.ToString();

                    if (wordToMatchBuilt.Length == wordToFind.Length && wordToMatchBuilt == wordToFind)
                    {
                        occurrencesCount++;
                        continue;
                    }
                }
            }

            return occurrencesCount;
        }

        private char FindLetterOnCoordinates(char[,] content, Coordinates letterCoordinates)
        {
            char letterFound = content[letterCoordinates.X, letterCoordinates.Y];
            return letterFound;
        }

        private class WordProperties
        {
            public int WordLength { get; private set; }
            public int MiddleIndex { get; private set; }
            public char MiddleLetter { get; private set; }
            public int WordLengthFromMiddle { get; private set; }

            public WordProperties(string wordToFind)
            {
                WordLength = wordToFind.Length;
                MiddleIndex = WordLength / 2;
                MiddleLetter = wordToFind[MiddleIndex];
                WordLengthFromMiddle = WordLength - MiddleIndex;
            }
        }

        public int XWordOcurrencesOnContent(char[,] content, string wordToFind)
        {
            if (content?.Length == 0)
            {
                return 0;
            }

            int contentHorizontalLength = content!.GetLength(0);
            int contentVerticalLength = content!.GetLength(1);
            int occurrencesCount = 0;

            var wordProperties = new WordProperties(wordToFind);

            if (wordProperties.WordLength % 2 == 0)
            {
                throw new ArgumentException(nameof(wordToFind));
            }

            for (int verticalPos = 0; verticalPos < contentVerticalLength; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentHorizontalLength; horizontalPos++)
                {
                    Coordinates currentCoordinates = new Coordinates(horizontalPos, verticalPos);
                    List<(int, int)> directionsMultiplier = BuildDirectionsMultiplier(currentCoordinates, wordProperties.WordLengthFromMiddle, contentHorizontalLength, contentVerticalLength);
                    if(directionsMultiplier.Count < 8)
                    {
                        continue;
                    }
                    char letter = content[horizontalPos, verticalPos];
                    if(letter != wordProperties.MiddleLetter)
                    {
                        continue;
                    }

                    occurrencesCount = GetXWordOccurrences(content, currentCoordinates, wordToFind) ? occurrencesCount + 1 : occurrencesCount;
                }
            }
            return occurrencesCount;
        }

        //Warning: For now let's just consider that the word is of a maximum length of 3. the method needs to be changed to allow different lengths
        private bool GetXWordOccurrences(char[,] content, Coordinates currentCoordinates, string wordToFind) 
        {
            var wordProperties = new WordProperties(wordToFind);

            var letter = content[currentCoordinates.X, currentCoordinates.Y];
            if(letter != wordProperties.MiddleLetter && wordProperties.WordLength != 3)
            { 
                return false;
            }

            Coordinates coordinatesToLook = null;

            StringBuilder stringBuilderOne = new StringBuilder();
            coordinatesToLook = new Coordinates(currentCoordinates.X - 1, currentCoordinates.Y + 1);
            stringBuilderOne.Append(content[coordinatesToLook.X, coordinatesToLook.Y]);
            stringBuilderOne.Append(wordProperties.MiddleLetter);
            coordinatesToLook = new Coordinates(currentCoordinates.X + 1, currentCoordinates.Y - 1);
            stringBuilderOne.Append(content[coordinatesToLook.X, coordinatesToLook.Y]);
            string wordFirstAxis = stringBuilderOne.ToString();
            string wordFirstAxisReversed = string.Concat(wordFirstAxis!.Reverse());

            StringBuilder stringBuilderTwo = new StringBuilder();
            coordinatesToLook = new Coordinates(currentCoordinates.X + 1, currentCoordinates.Y + 1);
            stringBuilderTwo.Append(content[coordinatesToLook.X, coordinatesToLook.Y]);
            stringBuilderTwo.Append(wordProperties.MiddleLetter);
            coordinatesToLook = new Coordinates(currentCoordinates.X - 1, currentCoordinates.Y - 1);
            stringBuilderTwo.Append(content[coordinatesToLook.X, coordinatesToLook.Y]);
            string wordSecondAxis = stringBuilderTwo.ToString();
            string wordSecondAxisReversed = string.Concat(wordSecondAxis!.Reverse());

            if (string.IsNullOrWhiteSpace(wordFirstAxis) || string.IsNullOrWhiteSpace(wordSecondAxis) ||
               (wordFirstAxis != wordToFind && wordFirstAxisReversed != wordToFind) ||
               (wordSecondAxis != wordToFind && wordSecondAxisReversed != wordToFind)
            )
            {
                return false;
            }

            return true;
        }
    }
}
