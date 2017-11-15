using System;
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
    public class Grocery
    {
        /*
         * Main Method:
         */

        public static void Solve()
        {
            var solver = new Solver("Grocery");

            // One variable for each product:

            IntVar p1 = solver.MakeIntVar(0, 711);
            IntVar p2 = solver.MakeIntVar(0, 711);
            IntVar p3 = solver.MakeIntVar(0, 711);
            IntVar p4 = solver.MakeIntVar(0, 711);

            // Prices add up to 711:

            solver.Add(p1 + p2 + p3 + p4 == 711);

            // Product of individual prices is 711:

            solver.Add(p1*p2*p3*p4 == 711*100*100*100);

            // Symmetry breaking constraint:

            solver.Add(p1 <= p2);
            solver.Add(p2 <= p3);
            solver.Add(p3 <= p4);

            // Start Solver:

            DecisionBuilder db = solver.MakePhase(new[] {p1, p2, p3, p4}, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            solver.NewSearch(db);

            while (solver.NextSolution())
            {
                Console.WriteLine("Product 1: " + p1.Value());
                Console.WriteLine("Product 2: " + p2.Value());
                Console.WriteLine("Product 3: " + p3.Value());
                Console.WriteLine("Product 4: " + p4.Value());
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