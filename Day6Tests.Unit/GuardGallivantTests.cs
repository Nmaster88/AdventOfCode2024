

using Day6;

namespace Day6Tests.Unit
{
    public class GuardGallivantTests
    {
        [Fact]
        public void GivenAMap_WhenGuardGallivantPatrolIsExecuted_ReturnTheNumberOfDistinctPositions()
        {
            string[] contentsMap =
            {
                "....#.....",
                ".........#",
                "..........",
                "..#.......",
                ".......#..",
                "..........",
                ".#..^.....",
                "........#.",
                "#.........",
                "......#..."
            };

            contentsMap = contentsMap.Reverse().ToArray();

            char[,] mapMatrix = new char[contentsMap.Length, contentsMap[0].Length];

            for (int verticalPos = 0; verticalPos < contentsMap.Length; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentsMap[verticalPos].Length; horizontalPos++)
                {
                    mapMatrix[horizontalPos, verticalPos] = contentsMap[verticalPos][horizontalPos];
                }
            }

            GuardGallivant guardGallivant = new GuardGallivant();
            int distinctPositions = guardGallivant.IterateGuardPatrolAndReturnDistinctPositions(mapMatrix);

            Assert.Equal(41, distinctPositions);
        }

        [Fact]
        public void GivenAMap_WhenGuardGallivantPatrolIsExecuted_ReturnTheNumberOfDistinctExtraObstructionsForLoopStuck()
        {
            string[] contentsMap =
            {
                "....#.....",
                ".........#",
                "..........",
                "..#.......",
                ".......#..",
                "..........",
                ".#..^.....",
                "........#.",
                "#.........",
                "......#..."
            };

            contentsMap = contentsMap.Reverse().ToArray();

            char[,] mapMatrix = new char[contentsMap.Length, contentsMap[0].Length];

            for (int verticalPos = 0; verticalPos < contentsMap.Length; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < contentsMap[verticalPos].Length; horizontalPos++)
                {
                    mapMatrix[horizontalPos, verticalPos] = contentsMap[verticalPos][horizontalPos];
                }
            }

            GuardGallivant guardGallivant = new GuardGallivant(mapMatrix);
            int distinctPositions = guardGallivant.IterateGuardPatrolAndReturnDistinctExtraObstructionsForLoopStuck(mapMatrix);

            Assert.Equal(6, distinctPositions);
        }
    }
}