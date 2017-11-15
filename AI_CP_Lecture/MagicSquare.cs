using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the grocery store puzzle presented in CP part 1.
 *
 * A kid goes into a grocery store and buys four items.
 * The cashier charges $7.11, the kid pays and is about to leave when the cashier calls the kid back and says: 
 * «Hold on, I multiplied the four items instead of adding them. 
 * I'll try again. Hah, with adding them the price still comes to $7.11.» 
 * What were the prices of the four items?
 * 
 * Lecture: Introduction to Artificial Intelligence
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    public class MagicSquare
    {
        /*
         * Main Method:
         */
        

        public static void Solve(int size)
        {
            var solver = new Solver("Grocery");
            //magic constant = [n * (n^2 + 1)] / 2
            int magicsum = (size * ( size *size +1))/2;
            //Console.WriteLine(magicsum);
            // One variable for each product:
            int cellSize = size;
            //const int boardSize = cellSize * cellSize;
            int maxValue = cellSize * cellSize;

            //Define board with a value between 1 and maxValue for all cells
            IntVar[,] board = solver.MakeIntVarMatrix(cellSize, cellSize, 1, maxValue);

            // Prices add up to 711:
            IEnumerable<int> cell = Enumerable.Range(0, cellSize);

            // 3 x 3 Matrix
            //All values are different
            solver.Add((from column in cell from row in cell select board[column, row]).ToArray().AllDifferent());

            //horizontal
            int boardsize = board.GetLength(0);
            
            for (int column = 0; column < boardsize; column++) { 
                //Horizontal sums
                solver.Add((from row in cell select board[column, row]).ToArray().Sum() == magicsum);
                //Vertical sums
                solver.Add((from row in cell select board[ row, column]).ToArray().Sum() == magicsum);
                //top left to bottom right
                solver.Add((from row in cell select board[row, row]).ToArray().Sum() == magicsum);
                //bottom left to top right
                solver.Add((from row in cell select board[boardsize - 1 - row, row]).ToArray().Sum() == magicsum);
            }

            //Only one Solution 
           
            // Start Solver:

            DecisionBuilder db = solver.MakePhase(board.Flatten(), Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            Console.WriteLine("MagicSuqare(" + size + "x" + size + "):\n\n");

            solver.NewSearch(db);

            //find only one solution
            solver.NextSolution();
            PrintSolution(board);
            Console.WriteLine();
           



            Console.WriteLine("Only the first solution is printed.");
          //  Console.WriteLine("\nSolutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            solver.EndSearch();

            Console.ReadKey();
        }


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