using System;
using System.Threading;

namespace GameProgram2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random chaos = new Random(); //Summon the *Chaos Wizard*

            Console.WriteLine();

            int gridSize = chaos.Next(6, 70); //Randomize size of board    
            int AliveCell = 1;
            int DeadCell = 0;

            int[,] gridRead = new int[gridSize, gridSize]; //array for our current grid generation
            int[,] gridWrite = new int[gridSize, gridSize]; //array for storing germ children of the next generation


            for (int y = 1; y < gridSize - 1; y++)
            {
                for (int x = 1; x < gridSize - 1; x++)
                {
                    gridRead[y, x] = chaos.Next(0, 2); //Randomize the first board generation
                }

            }

            DrawGrid(gridSize, AliveCell, gridRead); //Drawing first board generation

            while (true)
            {
                LifeCalculations(gridSize, AliveCell, DeadCell, gridRead, gridWrite); //Magic Spell
                gridRead = gridWrite;
                gridWrite = new int[gridSize, gridSize];

                Console.WriteLine();

                DrawGrid(gridSize, AliveCell, gridRead);
                Thread.Sleep(50);
                //Console.ReadKey(true);
            }
        }

        private static void LifeCalculations(int gridSize, int AliveCell, int DeadCell, int[,] gridRead, int[,] gridWrite)
        {
            for (int y = 1; y < gridSize - 1; y++)
            {
                for (int x = 1; x < gridSize - 1; x++)
                {
                    int neighborSum = gridRead[y + 1, x] + //checking all adjacent cells, adding their cumulative values
                        gridRead[y + 1, x + 1] +
                        gridRead[y, x + 1] +
                        gridRead[y - 1, x + 1] +
                        gridRead[y - 1, x] +
                        gridRead[y - 1, x - 1] +
                        gridRead[y, x - 1] +
                        gridRead[y + 1, x - 1];

                    if (neighborSum == 3) //Any cell with 3 neighbors becomes alive
                    {
                        gridWrite[y, x] = AliveCell;
                    }
                    else if (neighborSum == 2) //Any cell with 2 neighbors stay the way it is
                    {
                        gridWrite[y, x] = gridRead[y, x];
                    }
                    else //Cell DIES
                    {
                        gridWrite[y, x] = DeadCell;
                    }

                }


            }
        }
        private static void DrawGrid(int gridSize, int AliveCell, int[,] grid) //Sooo readable
        {
            Console.SetCursorPosition(0, 0); //Prints from top of screen again
            for (int y = 1; y < gridSize - 1; y++)
            {
                for (int x = 1; x < gridSize - 1; x++)
                {
                    if (grid[y, x] == AliveCell)
                    {
                        Console.Write("■ "); //Living Cell
                    }
                    else
                    {
                        Console.Write("· "); //Dead Cell
                    }
                }
                Console.WriteLine();
            }
        }
    }
}