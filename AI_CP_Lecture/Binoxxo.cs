using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the Binoxxo puzzle presented in CP part 1 and 2.
 *
 * Fill in X or O
 *
 * Lecture: Introduction to Artificial Intelligence
 * Author: Gerda Bieri
 */

namespace AI_CP_Lecture
{
    public class Binoxxo
    {

        /*
         * Create Model and Solve Binoxxo:
         */

        public static void Solve(string[,] input)
        {
            if (input.Length != 100)
            {
                throw new ArgumentException("This is not a valid 10x10 Binoxxo Puzzle.");
            }

            const int numOfFields = 10;

            var solver = new Solver("Binoxxo");

            IEnumerable<int> fieldRange = Enumerable.Range(0, numOfFields);
            IEnumerable<int> range = Enumerable.Range(0, 1);

            // Binoxxo Board as 10x10 Matrix of Decision Variables in {X,P}:
            IntVar[,] board = solver.MakeIntVarMatrix(numOfFields, numOfFields, 0, 1);

            // Pre-Assignments:
            for (int i = 0; i < numOfFields; i++)
            {
                for (int j = 0; j < numOfFields; j++)
                {
                    if (!string.IsNullOrEmpty(input[i, j]))
                    {
                        string inputValue = Convert.ToString(input[i, j]);
                        int valueAsInt = 0;
                        if (inputValue == "X")
                        {
                            valueAsInt = 1;
                        }
                        solver.Add(board[i, j] == valueAsInt);
                    }
                }
            }


            // ---------------------------------Constraints---------------------------------
            // Alle Zeilen und Spalten müssen gleich viele 1 wi 0en haben, deshalb cellSize/2
            long fieldSum = (numOfFields / 2);
            foreach (int i in fieldRange)
            {
                solver.Add(((from j in fieldRange select board[i, j]).ToArray().Sum() == fieldSum));
                solver.Add(((from j in fieldRange select board[j, i]).ToArray().Sum() == fieldSum));
            }

            // Es dürfen nie drei oder mehr 1 oder 0 nacheinander vorkommen
            IEnumerable<int> Rangefrom0to3 = Enumerable.Range(0, 3);
            IEnumerable<int> specialRange = Enumerable.Range(0, numOfFields - 2);

            IntVar[] rowArrayFor3 = new IntVar[3];
            IntExpr expressionFromRowI;

            IntVar[] colunnArrayFor3 = new IntVar[3];
            IntExpr expressionFromColumnI;

            foreach (int i in fieldRange)
            {
                foreach (int j in specialRange)
                {
                    // Rows:
                    rowArrayFor3 = (from di in Rangefrom0to3
                                select (board[i,  j + di])).ToArray();
                    expressionFromRowI =
                        rowArrayFor3[0] + rowArrayFor3[1] + rowArrayFor3[2];
                    solver.Add(expressionFromRowI != 3);
                    solver.Add(expressionFromRowI != 0);

                    // Columns:
                    colunnArrayFor3 = (from di in Rangefrom0to3
                                   select (board[j + di, i])).ToArray();
                    expressionFromColumnI =
                        colunnArrayFor3[0] + colunnArrayFor3[1] + colunnArrayFor3[2];
                    solver.Add(expressionFromColumnI != 3);
                    solver.Add(expressionFromColumnI != 0);
                }
            }
            
            // Die Zeilen und Spalten müssen unabhängig voneinander sein

            long[] powerArray = new long[numOfFields];
            foreach (int i in fieldRange)
            {
                powerArray[i] = (long)Math.Pow(10, i);
            }

            IntVar[] rowArray;
            IntVar[] columnArray;

            IntExpr[] expressionsForRows = new IntExpr[numOfFields];
            IntExpr[] expressionsForColums = new IntExpr[numOfFields];

            foreach (int i in fieldRange)
            {
                // For each Row
                rowArray = (from di in fieldRange
                            select (board[i, di])).ToArray();
                expressionFromRowI = 
                    rowArray[0] * powerArray[0] +
                    rowArray[1] * powerArray[1] +
                    rowArray[2] * powerArray[2] +
                    rowArray[3] * powerArray[3] +
                    rowArray[4] * powerArray[4] +
                    rowArray[5] * powerArray[5] +
                    rowArray[6] * powerArray[6] +
                    rowArray[7] * powerArray[7] +
                    rowArray[8] * powerArray[8] +
                    rowArray[9] * powerArray[9];

                foreach (int j in fieldRange)
                {
                    if ((i > j))
                    {
                        solver.Add(expressionsForRows[j] != expressionFromRowI);
                    }
                }

                expressionsForRows[i] = expressionFromRowI;

                // For each Column
                columnArray = (from di in fieldRange
                            select board[di, i]).ToArray();
                expressionFromColumnI =
                    columnArray[0] * powerArray[0] +
                    columnArray[1] * powerArray[1] +
                    columnArray[2] * powerArray[2] +
                    columnArray[3] * powerArray[3] +
                    columnArray[4] * powerArray[4] +
                    columnArray[5] * powerArray[5] +
                    columnArray[6] * powerArray[6] +
                    columnArray[7] * powerArray[7] +
                    columnArray[8] * powerArray[8] +
                    columnArray[9] * powerArray[9];

                foreach (int j in fieldRange)
                {
                    if ((i > j))
                    {
                        solver.Add(expressionsForColums[j] != expressionFromColumnI);
                    }
                }
                expressionsForColums[i] = expressionFromColumnI;

            }

            // ---------------------------------Constraints finished------------------------

            // Start Solver:

            DecisionBuilder db = solver.MakePhase(board.Flatten(), Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            Console.WriteLine("Binoxxo:\n\n");

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
                    Console.Write("[{0}] ", board[i, j].Value()==1 ? "X" : "O");
                }
                Console.WriteLine();
            }
        }
    }
}