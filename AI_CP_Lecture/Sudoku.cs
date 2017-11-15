using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the Sudoku puzzle presented in CP part 1 and 2.
 *
 * Fill in numbers from 1 to 9 so that each row, column and 3x3 block contains each number exactly once.
 *
 * Lecture: Introduction to Artificial Intelligence
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    public class Sudoku
    {

        /*
         * Create Model and Solve Sudoku:
         */

        public static void Solve(string[,] input)
        {
            if (input.Length != 81)
            {
                throw new ArgumentException("This is not a valid 9x9 Sudoku Puzzle.");
            }

            const int cellSize = 3;
            const int boardSize = cellSize*cellSize;

            var solver = new Solver("Sudoku");

            IEnumerable<int> cell = Enumerable.Range(0, cellSize);
            IEnumerable<int> range = Enumerable.Range(0, boardSize);

            // Sudoku Board as 9x9 Matrix of Decision Variables in {1..9}:
            IntVar[,] board = solver.MakeIntVarMatrix(boardSize, boardSize, 1, boardSize);

            // Pre-Assignments:
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (!string.IsNullOrEmpty(input[i, j]))
                    {
                        int number = Convert.ToInt32(input[i, j]);
                        solver.Add(board[i, j] == number);
                    }
                }
            }

            // Each Row / Column contains only different values: 
            foreach (int i in range)
            {
                // Rows:
                solver.Add((from j in range select board[i, j]).ToArray().AllDifferent());

                // Columns:
                solver.Add((from j in range select board[j, i]).ToArray().AllDifferent());
            }


            // Each Sub-Matrix contains only different values:
            foreach (int i in cell)
            {
                foreach (int j in cell)
                {
                    solver.Add(
                        (from di in cell from dj in cell select board[i*cellSize + di, j*cellSize + dj]).ToArray()
                            .AllDifferent());
                }
            }

            // Start Solver:

            DecisionBuilder db = solver.MakePhase(board.Flatten(), Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            Console.WriteLine("Sudoku:\n\n");

            solver.NewSearch(db);

            while (solver.NextSolution())
            {
                PrintSolution(board);
                Console.WriteLine();
            }

            Console.WriteLine("\nSolutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            solver.EndSearch();

            Console.ReadKey();
        }

        /*
         * Print Game Board:
         */

        private static void PrintSolution(IntVar[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write("[{0}] ", board[i, j].Value());
                }
                Console.WriteLine();
            }
        }
    }
}