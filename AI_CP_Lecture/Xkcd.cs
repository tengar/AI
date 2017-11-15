using System;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the XKCD restaurant puzzle presented in CP part 3.
 * 
 * As constraint problem:
 * Given a set of items (e.g. appetizers) with weights (e.g. prices) and a total weight N (e.g. $15.05). Select items such that the sum of the chosen items equals N. 
 * 
 * As optimization problem:
 * Given a set of items with weights and a maximum weight N. Select items such that the sum of the chosen items is as close as possible to N without exceeding N.
 * 
 * Lecture: Introduction to Artificial Intelligence
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    public class Xkcd
    {
        /*
         * Main Method:
         */

        public static void Solve()
        {
            var solver = new Solver("XKCD");

            /*
             * Appetizer:
             */

            IntVar fruit = solver.MakeIntVar(0, 10, "fruit");
            IntVar fries = solver.MakeIntVar(0, 10, "fries");
            IntVar salad = solver.MakeIntVar(0, 10, "salad");
            IntVar wings = solver.MakeIntVar(0, 10, "wings");
            IntVar sticks = solver.MakeIntVar(0, 10, "sticks");
            IntVar plate = solver.MakeIntVar(0, 10, "plate");

            IntVar[] items = {fruit, fries, salad, wings, sticks, plate};

            int[] prices = {215, 275, 335, 355, 420, 580};

            ConstraintModel(solver, items, prices);

            OptimizationModel(solver, items, prices);
        }

        /*
         * Xkcd Puzzle as Constraint Problem:
         */

        private static void ConstraintModel(Solver solver, IntVar[] items, int[] prices)
        {
            /*
             * Constraints:
             */

            solver.Add(solver.MakeScalProd(items, prices) == 1505);

            /*
             * Start Solver:
             */

            DecisionBuilder db = solver.MakePhase(items, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            Console.WriteLine("XKCD Constraint Problem:\n\n");

            solver.NewSearch(db);

            while (solver.NextSolution())
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Console.WriteLine("Appetizer " + items[i].Name() + " is ordered " + items[i].Value() + " times.");
                }

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
         * Xkcd Puzzle as Optimization Problem:
         */

        private static void OptimizationModel(Solver solver, IntVar[] items, int[] prices)
        {
            /*
             * Objective Function:
             */

            IntVar obj = solver.MakeScalProd(items, prices).Var();

            /*
             * Constraints:
             */

            solver.Add(obj < 2000);

            /*
             * Start Solver:
             */

            DecisionBuilder db = solver.MakePhase(items, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            SolutionCollector col = solver.MakeBestValueSolutionCollector(true);
            col.AddObjective(obj);
            col.Add(items);

            Console.WriteLine("Xkcd Optimization Problem:\n");

            if (solver.Solve(db, col))
            {
                Assignment sol = col.Solution(0);
                Console.WriteLine("Maximum value found: " + sol.ObjectiveValue() + "\n");

                for (int i = 0; i < items.Length; i++)
                {
                    Console.WriteLine("Appetizer " + items[i].Name() + " is ordered " + sol.Value(items[i]) + " times.");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nSolutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            solver.EndSearch();

            Console.ReadKey();
        }
    }
}