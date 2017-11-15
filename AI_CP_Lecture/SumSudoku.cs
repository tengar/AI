using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the SumSudoku puzzle
 *
 * Fill in numbers from 1 to 9 so that each row, column and 3x3 block contains each number exactly once.
 *
 * Exercise: Sum Frame Sudoku
 * Sudoku Template by: Marc Pouly
 * Author: Samuel Tenga
 * 
 */

namespace AI_CP_Lecture
{
    public class SumSudoku
    {
        /*
         * Create Model and Solve Sudoku:
         */

        public static void Solve(Tuple<int[], int[], int[], int[]> mytupel)
        {
			//TOP,RIGHT,BUTTOM,LEFT
           /* if (input.Length != 36)
            {
                throw new ArgumentException("This is not a valid 4x9 SumSudoku Puzzle.");
            }*/

            const int cellSize = 3;
            const int boardSize = cellSize*cellSize;

            var solver = new Solver("SumSodoku");

            IEnumerable<int> cell = Enumerable.Range(0, cellSize);
            IEnumerable<int> range = Enumerable.Range(0, boardSize);

			
            // Sudoku Board as 9x9 Matrix of Decision Variables in {1..9}:
            IntVar[,] board = solver.MakeIntVarMatrix(boardSize, boardSize, 1, boardSize);

			//all The new SumSudoku Roles
			foreach (int columnRow in range)
			{
                solver.Add(board[columnRow, 0] + board[columnRow, 1] + board[columnRow, 2] == mytupel.Item1[columnRow]); //TOP
                solver.Add(board[6, columnRow] + board[7, columnRow] + board[8, columnRow] == mytupel.Item2[columnRow]); //RIGHT
                solver.Add(board[columnRow, 6] + board[columnRow, 7] + board[columnRow, 8] == mytupel.Item3[columnRow]); //BOTTOM
                solver.Add(board[0, columnRow] + board[1, columnRow] + board[2, columnRow] == mytupel.Item4[columnRow]); //LEFT
                                                                                                                        
            }

			// Each Row / Column contains only different values: 
			foreach (int row in range)
            {
                // Rows:
                solver.Add((from column in range select board[row, column]).ToArray().AllDifferent());

                // Columns:
                solver.Add((from column in range select board[column, row]).ToArray().AllDifferent());
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

            Console.WriteLine("SumSudoku:\n\n");

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