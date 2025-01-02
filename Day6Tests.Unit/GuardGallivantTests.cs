

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

        // Guard positions ^ > v <
        // Everytime the guard faces an obstacle he turns right 90 degrees.
        // If the guard reach the edges of the map and his on his way out he leaves the map and his travelling finishes 
            int distinctPositions = IterateGuardPatrolAndReturnDistinctPositions(mapMatrix);
            Assert.Equal(41, distinctPositions);
        }

        private int IterateGuardPatrolAndReturnDistinctPositions(char[,] mapMatrix)
        {
            List<Coordinates> obstructionsCoordinates = new List<Coordinates>();
            ObstructionsCoordinatesMapping(mapMatrix, obstructionsCoordinates);

            Coordinates guardCoordinates = new Coordinates();
            GuardStartCoordinatesMapping(mapMatrix, guardCoordinates);

            bool guardIsOnMap = true;
            while (guardIsOnMap)
            {
                guardIsOnMap = GuardMovementOnMap(mapMatrix, guardCoordinates, obstructionsCoordinates);
            }

            int distinctPositionOnMap = DistinctPositionsOnMap(mapMatrix);

            return distinctPositionOnMap;
        }

        private int DistinctPositionsOnMap(char[,] mapMatrix)
        {
            int count = 0;
            for (int x = 0; x < mapMatrix.GetLongLength(0); x++)
            {
                for (int y = 0; y < mapMatrix.GetLongLength(1); y++)
                {
                    if (mapMatrix[x, y] is 'X')
                    {
                        ++count;
                    }
                }
            }

            return count;
        }

        private bool GuardMovementOnMap(char[,] mapMatrix, Coordinates guardCoordinates, List<Coordinates> obstructionsCoordinates)
        {
            char guardFacing = mapMatrix[guardCoordinates.X, guardCoordinates.Y];

            (int, int) movementDirection = guardFacing switch
            {
                '^' => (0, 1),
                '>' => (1, 0),
                'v' => (0, -1),
                '<' => (-1, 0),
                _ => (0, 0)
            };

            int horizontalMapStart = 0;
            int horizontalMapEnding = mapMatrix.GetLength(0) - 1;

            int verticalMapStart = 0;
            int verticalMapEnding = mapMatrix.GetLength(1) - 1;

            int horizontalGuardNextMovement = guardCoordinates.X + movementDirection.Item1;
            int verticalGuardNextMovement = guardCoordinates.Y + movementDirection.Item2;

            if (
                horizontalGuardNextMovement < horizontalMapStart ||
                horizontalGuardNextMovement > horizontalMapEnding ||
                verticalGuardNextMovement < verticalMapStart ||
                verticalGuardNextMovement > verticalMapEnding
                )
            {
                mapMatrix[guardCoordinates.X, guardCoordinates.Y] = 'X';
                return false;
            }

            var obstrutionForTheMovement = obstructionsCoordinates.FirstOrDefault(oc => oc.X == horizontalGuardNextMovement && oc.Y == verticalGuardNextMovement);
            if ( obstrutionForTheMovement is Coordinates)
            {
                //guard remains on the same coordinate, but rotate it's direction 90 degrees to the right.
                mapMatrix[guardCoordinates.X, guardCoordinates.Y] = guardFacing switch
                {
                    '^' => '>',
                    '>' => 'v',
                    'v' => '<',
                    '<' => '^',
                    _ => guardFacing
                };
            }
            else
            {
                mapMatrix[guardCoordinates.X, guardCoordinates.Y] = 'X';
                guardCoordinates.X = horizontalGuardNextMovement;
                guardCoordinates.Y = verticalGuardNextMovement;
                mapMatrix[guardCoordinates.X, guardCoordinates.Y] = guardFacing;
            }

            return true;
        }

        private void GuardStartCoordinatesMapping(char[,] mapMatrix, Coordinates startGuardCoordinates)
        {
            for (int x = 0; x < mapMatrix.GetLongLength(0); x++)
            {
                for (int y = 0; y < mapMatrix.GetLongLength(1); y++)
                {
                    if (mapMatrix[x, y] is '^' or '>' or '<' or 'v')
                    {
                        startGuardCoordinates.X = x;
                        startGuardCoordinates.Y = y;
                    }
                }
            }
        }

        private void ObstructionsCoordinatesMapping(char[,] mapMatrix, List<Coordinates> obstructionsCoordinates)
        {
            for(int x = 0; x < mapMatrix.GetLongLength(0); x++)
            {
                for(int y=0; y < mapMatrix.GetLongLength(1); y++)
                {
                    if (mapMatrix[x,y] == '#')
                    {
                        obstructionsCoordinates.Add(new Coordinates { X = x, Y = y });
                    }
                }
            }
        }

        private static readonly (int dx, int dy)[] _allDirections =
        {
            (0, 1),   // Up
            //(1, 1),   // Up-Right
            (1, 0),   // Right
            //(1, -1),  // Down-Right
            (0, -1),  // Down
            //(-1, -1), // Down-Left
            (-1, 0),  // Left
            //(-1, 1)   // Up-Left
        };

        public class Coordinates 
        { 
            public int X { get; set; } 
            public int Y { get; set; }
        }

        public class Direction
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public char GuardStateDirection(Direction direction)
        {
            char value =
                direction switch
                {
                    { X: > 0 } and { Y: 0 } => '>',
                    { X: < 0 } and { Y: 0 } => '<',
                    { X: 0 } and { Y: > 0 } => '^',
                    { X: 0 } and { Y: < 0 } => 'v',
                    _ => '^'
                };

            return value;
        }
    }
}