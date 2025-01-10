namespace Day6
{
    public class GuardGallivant
    {
        public int HorizontalMapStart { get; init; }
        public int VerticalMapStart { get; init; }
        public int HorizontalMapEnding { get; init; }
        public int VerticalMapEnding { get; init; }

        public GuardGallivant(char[,] mapMatrix)
        {
            HorizontalMapStart = 0;
            HorizontalMapEnding = mapMatrix.GetLength(0) - 1;
            VerticalMapStart = 0;
            VerticalMapEnding = mapMatrix.GetLength(1) - 1;
        }

        public GuardGallivant() { }

        /// <summary>
        /// Part II
        /// </summary>
        /// <param name="mapMatrix"></param>
        /// <returns></returns>
        public int IterateGuardPatrolAndReturnDistinctExtraObstructionsForLoopStuck(char[,] mapMatrix)
        {
            List<Coordinates> obstructionsCoordinates = new List<Coordinates>();
            ObstructionsCoordinatesMapping(mapMatrix, obstructionsCoordinates);

            Guard guardCoordinates = new Guard();
            GuardStartCoordinatesMapping(mapMatrix, guardCoordinates);

            List<Coordinates> distinctObstructionForInfiniteLoops = new List<Coordinates>();
            bool guardIsOnMap = true;
            while (guardIsOnMap)
            {
                guardIsOnMap = GuardMovementOnMap(mapMatrix, guardCoordinates, obstructionsCoordinates);

                List<CellMovement> adjacentCells = GetAdjacentMovementCells(mapMatrix, guardCoordinates);

                for (int i = 0; i < adjacentCells.Count; i++)
                {
                    var currentCell = adjacentCells[i];
                    var nextCell = adjacentCells[(i + 1) % adjacentCells.Count];
                    if ((currentCell.Content is not '|' && currentCell.Content is not '-') ||
                        (nextCell.Content is not '|' && nextCell.Content is not '-'))
                    {
                        continue;
                    }
                    bool IsLoopMovement = CheckForMovementLoop(mapMatrix, currentCell, nextCell);

                    if(IsLoopMovement)
                    {
                        (int, int) movementDirection = GuardMovementDirection(guardCoordinates.Content);

                        Coordinates obstruction = new Coordinates();
                        obstruction.X = guardCoordinates.X + movementDirection.Item1;
                        obstruction.Y = guardCoordinates.Y + movementDirection.Item2;

                        distinctObstructionForInfiniteLoops.Add(obstruction);
                        Console.WriteLine("Found a loop!");
                    }
                }

            }

            //int distinctExtraObstructionsOnMap = 6;
            //return distinctExtraObstructionsOnMap;
            return distinctObstructionForInfiniteLoops.Count;
        }

        private bool CheckForMovementLoop(char[,] mapMatrix, CellMovement cellOne, CellMovement cellTwo)
        {
            if ((cellOne.Content is not '|' || cellOne.Content is not '-') &&
                (cellTwo.Content is not '|' || cellTwo.Content is not '-') &&
                (cellOne.Content == cellTwo.Content))
            {
                return false;
            }

            (bool, int, int) EvaluatePathOne = EvaluatePath(mapMatrix, cellOne, cellTwo);
            (bool, int, int) EvaluatePathTwo = EvaluatePath(mapMatrix, cellTwo, cellOne);

            if((EvaluatePathOne.Item1 is not true || EvaluatePathTwo.Item1 is not true) &&
                (EvaluatePathOne.Item2 != EvaluatePathTwo.Item3 || (EvaluatePathTwo.Item3 != EvaluatePathOne.Item2)))
            {
                return false;
            }

            return true;
        }

        private (bool, int, int) EvaluatePath(char[,] mapMatrix, CellMovement cellOne, CellMovement cellTwo)
        {
            char cellOneContent = cellOne.Content;
            char cellTwoContent = cellTwo.Content;
            int incrementCellOne = 0;
            int incrementCellTwo = 0;

            while (cellOne.Content != '+' && cellOne.Content == cellOneContent)
            {
                int newX = cellOne.X + cellOne.xMultiplier;
                int newY = cellOne.Y + cellOne.yMultiplier;
                if (newX >= HorizontalMapStart && newX <= HorizontalMapEnding &&
                    newY >= VerticalMapStart && newY <= VerticalMapEnding)
                {
                    cellOne.Content = mapMatrix[newX, newY];
                    cellOne.X = newX;
                    cellOne.Y = newY;
                    ++incrementCellOne;
                }
            }
            if (cellOne.Content != '+')
            {
                return (false, incrementCellOne, 0);
            }
            do
            {
                int newX = cellOne.X + cellTwo.xMultiplier;
                int newY = cellOne.Y + cellTwo.yMultiplier;
                if (newX >= HorizontalMapStart && newX <= HorizontalMapEnding &&
                    newY >= VerticalMapStart && newY <= VerticalMapEnding)
                {
                    cellOne.Content = mapMatrix[newX, newY];
                    cellOne.X = newX;
                    cellOne.Y = newY;
                    ++incrementCellTwo;
                }
            }
            while (cellOne.Content != '+' && cellOne.Content == cellTwoContent);
            if (cellOne.Content != '+')
            {
                return (false, incrementCellOne, incrementCellTwo);
            }

            return (true, incrementCellOne, incrementCellTwo);
        }

        private List<CellMovement> GetAdjacentMovementCells(char[,] mapMatrix, Guard guardCoordinates)
        {
            List<CellMovement> adjacentCellsMovement = new List<CellMovement>();

            int[,] directions = GetDirections();
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = guardCoordinates.X + directions[i, 0];
                int newY = guardCoordinates.Y + directions[i, 1];
                if (newX >= HorizontalMapStart && newX <= HorizontalMapEnding &&
                    newY >= VerticalMapStart && newY <= VerticalMapEnding)
                {
                    CellMovement adjacentCellMovement = new CellMovement();
                    adjacentCellMovement.X = newX;
                    adjacentCellMovement.Y = newY;
                    adjacentCellMovement.Content = mapMatrix[newX, newY];
                    adjacentCellMovement.xMultiplier = directions[i, 0];
                    adjacentCellMovement.yMultiplier = directions[i, 1];

                    adjacentCellsMovement.Add(adjacentCellMovement);
                }
            }

            return adjacentCellsMovement;
        }

        private static int[,] GetDirections()
        {
            return new int[,]
            { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
        }

        private bool GuardMovementOnMap(char[,] mapMatrix, Guard guardCoordinates, List<Coordinates> obstructionsCoordinates)
        {
            char guardFacing = mapMatrix[guardCoordinates.X, guardCoordinates.Y];
            (int, int) movementDirection = GuardMovementDirection(guardFacing);

            Coordinates guardNextMovement = new Coordinates();
            guardNextMovement.X = guardCoordinates.X + movementDirection.Item1;
            guardNextMovement.Y = guardCoordinates.Y + movementDirection.Item2;

            if (
                guardNextMovement.X < HorizontalMapStart ||
                guardNextMovement.X > HorizontalMapEnding ||
                guardNextMovement.Y < VerticalMapStart ||
                guardNextMovement.Y > VerticalMapEnding
                )
            {
                var trailingMovement = GetTrailingMovement(guardFacing);
                mapMatrix[guardCoordinates.X, guardCoordinates.Y] = trailingMovement;
                return false;
            }

            var obstrutionForTheMovement = obstructionsCoordinates.FirstOrDefault(oc => oc.X == guardNextMovement.X && oc.Y == guardNextMovement.Y);
            if (obstrutionForTheMovement is Coordinates)
            {
                GuardRotating(mapMatrix, guardCoordinates);
            }
            else if (guardCoordinates.Rotated)
            {
                UpdateMapTrailingMovement(mapMatrix, guardNextMovement, guardFacing);
                UpdateMapTrailingMovement(mapMatrix, guardCoordinates, '+');
                UpdateGuardMovement(guardCoordinates, guardNextMovement);
            }
            else
            {
                char trailingMovement = GetTrailingMovement(guardFacing);
                UpdateMapTrailingMovement(mapMatrix, guardCoordinates, trailingMovement);
                UpdateGuardMovement(guardCoordinates, guardNextMovement);
                UpdateMapTrailingMovement(mapMatrix, guardNextMovement, guardFacing);
            }

            return true;
        }

        private static char GetTrailingMovement(char guardFacing)
        {
            return guardFacing switch
            {
                '^' => '|',
                '>' => '-',
                'v' => '|',
                '<' => '-',
                _ => guardFacing
            };
        }

        private void UpdateGuardMovement(Guard guardCoordinates, Coordinates nextMovement)
        {
            guardCoordinates.X = nextMovement.X;
            guardCoordinates.Y = nextMovement.Y;
            guardCoordinates.Rotated = false;
        }

        private void UpdateMapTrailingMovement(char[,] mapMatrix,  Coordinates coordinates, char cellUpdate)
        {
            mapMatrix[coordinates.X, coordinates.Y] = cellUpdate;
        }

        private void GuardRotating(char[,] mapMatrix, Guard guardCoordinates)
        {
            char guardFacing = mapMatrix[guardCoordinates.X, guardCoordinates.Y];
            //guard remains on the same coordinate, but rotate it's direction 90 degrees to the right.
            mapMatrix[guardCoordinates.X, guardCoordinates.Y] = guardFacing switch
            {
                '^' => '>',
                '>' => 'v',
                'v' => '<',
                '<' => '^',
                _ => guardFacing
            };

            guardCoordinates.Rotated = true;
        }

        private static (int, int) GuardMovementDirection(char guardFacing)
        {
            return guardFacing switch
            {
                '^' => (0, 1),
                '>' => (1, 0),
                'v' => (0, -1),
                '<' => (-1, 0),
                _ => (0, 0)
            };
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
