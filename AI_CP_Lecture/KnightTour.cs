using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the knight tour puzzle presented in CP part 3.
 *
 * A knight's tour is a sequence of moves of a knight on a chessboard such that the knight visits every cell exactly once.
 * 
 * Lecture: Introduction to Artificial Intelligence
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    public class KnightTour
    {

        /*
         * Create model and solve problem:
         * Parameter determines chess board dimension
         */

        public static void Solve(int dim)
        {
            var solver = new Solver("Knight's Tour");

            /*
             * Chess board of size dim x dim: next[i] = where to jump from cell i
             * Start position = 0, end position = dim*dim (i.e. outside chessboard)
             */

            IntVar[] next = solver.MakeIntVarArray(dim*dim, 1, dim*dim);

            /*
             * Chess rules: 
             * For each cell (variable), eliminate all cells that cannot be reached from this position from the domain.
             */

            for (int i = 0; i < next.Length; i++)
            {
                next[i].SetValues(GetReachableCells(i, dim));
            }

            /*
             * Create a connected path:
             */

            solver.Add(next.AllDifferent());

            /*
             * Variables that are part of the tour:
             */

            IntVar[] active = solver.MakeBoolVarArray(next.Length);

            foreach (IntVar t in active)
            {
                solver.Add(t == 1);
            }

            /*
             * Do not visit twice the same cell:
             */

            solver.Add(solver.MakeNoCycle(next, active, null));


            /*
             * Start solver:
             */

            DecisionBuilder db = solver.MakePhase(next, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            solver.NewSearch(db);

            if (solver.NextSolution())
            {
                Console.WriteLine("The Knight's Tour starts in cell 0:\n\n");

                for (int i = 0; i < next.Length; i++)
                {
                    if (next[i].Value() != dim*dim)
                    {
                        Console.WriteLine("From cell " + i + " go to cell next[" + i + "] = " + next[i].Value());
                    }
                    else
                    {
                        Console.WriteLine("Cell " + i + " is the final cell of the knight's tour.");
                    }
                }

                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("No Knight's Tour found.\n");
            }

            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            solver.EndSearch();

            Console.ReadKey();
        }

        /*
         * Return an array of cell numbers that can be reached from the current position by the knight figure.
         */

        private static long[] GetReachableCells(int i, int dim)
        {
            var from = new List<long>();

            from.Add(i - 2*dim - 1);
            from.Add(i - 2*dim + 1);
            from.Add(i - dim - 2);
            from.Add(i - dim + 2);
            from.Add(i + dim - 2);
            from.Add(i + dim + 2);
            from.Add(i + 2*dim - 1);
            from.Add(i + 2*dim + 1);

            // Any cell can be the last in a knight's tour:
            from.Add(dim * dim);

            return from.Where(cell =>
                                (cell >= 0 &&
                                cell <= dim * dim &&
                                Math.Abs(cell/dim - i/dim) <= 2 &&
                                Math.Abs(cell%dim - i%dim) <= 2) ||
                                cell == (dim*dim)).ToArray();
        }
    }
}