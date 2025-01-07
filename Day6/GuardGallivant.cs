
namespace Day6
{
    public class GuardGallivant
    {
        public class Coordinates
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private Coordinates _guardStartCoordinates;

        /// <summary>
        /// Part II
        /// </summary>
        /// <param name="mapMatrix"></param>
        /// <returns></returns>
        public int IterateGuardPatrolAndReturnDistinctExtraObstructionsForLoopStuck(char[,] mapMatrix)
        {
            List<Coordinates> obstructionsCoordinates = new List<Coordinates>();
            ObstructionsCoordinatesMapping(mapMatrix, obstructionsCoordinates);

            Coordinates guardCoordinates = new Coordinates();
            GuardStartCoordinatesMapping(mapMatrix, guardCoordinates);

            _guardStartCoordinates = new Coordinates();
            _guardStartCoordinates.X = guardCoordinates.X;
            _guardStartCoordinates.Y = guardCoordinates.Y;

            List<Coordinates> distinctObstructionForInfiniteLoops = new List<Coordinates>();
            bool guardIsOnMap = true;
            while (guardIsOnMap)
            {
                guardIsOnMap = GuardMovementOnMap(mapMatrix, guardCoordinates, obstructionsCoordinates, distinctObstructionForInfiniteLoops);
            }

            //TODO
            int distinctExtraObstructionsOnMap = 6;

            return distinctExtraObstructionsOnMap;
        }

        private bool GuardMovementOnMap(char[,] mapMatrix, Coordinates guardCoordinates, List<Coordinates> obstructionsCoordinates, List<Coordinates> distinctObstructionForInfiniteLoops)
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
                //mapMatrix[guardCoordinates.X, guardCoordinates.Y] = 'X';
                return false;
            }

            var obstrutionForTheMovement = obstructionsCoordinates.FirstOrDefault(oc => oc.X == horizontalGuardNextMovement && oc.Y == verticalGuardNextMovement);
            if (obstrutionForTheMovement is Coordinates)
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
                var trailingMovement = guardFacing switch
                {
                    '^' => '|',
                    '>' => '-',
                    'v' => '|',
                    '<' => '-',
                    _ => guardFacing
                };

                mapMatrix[guardCoordinates.X, guardCoordinates.Y] = trailingMovement;
                guardCoordinates.X = horizontalGuardNextMovement;
                guardCoordinates.Y = verticalGuardNextMovement;
                mapMatrix[guardCoordinates.X, guardCoordinates.Y] = guardFacing;
            }

            return true;
        }

        /// <summary>
        /// Part I
        /// </summary>
        /// <param name="mapMatrix"></param>
        /// <returns></returns>
        public int IterateGuardPatrolAndReturnDistinctPositions(char[,] mapMatrix)
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
            for (int x = 0; x < mapMatrix.GetLongLength(0); x++)
            {
                for (int y = 0; y < mapMatrix.GetLongLength(1); y++)
                {
                    if (mapMatrix[x, y] == '#')
                    {
                        obstructionsCoordinates.Add(new Coordinates { X = x, Y = y });
                    }
                }
            }
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
            if (obstrutionForTheMovement is Coordinates)
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
    }
}
