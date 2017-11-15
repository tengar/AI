using System;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the cryptogram puzzle presented in CP part 1.
 *
 * The characters S, E, N ,D, M, O, R, Y stand for digits between 0 and 9. 
 * Numbers are built from digits in the usual, positional notation.
 * Repeated occurrence of the same character denote the same digit.
 * Different characters must take different digits.
 * Numbers must not start with a zero.
 * The following equation must hold: SEND + MORE = MONEY
 * 
 * Lecture: Introduction to Artificial Intelligence
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    public class Cryptogram
    {
        /*
         * Main Method:
         */

        public static void Solve()
        {

			var solver = new Solver("Cryptogram");

            // One variable for each Character:

            IntVar S = solver.MakeIntVar(0, 9);
            IntVar E = solver.MakeIntVar(0, 9);
            IntVar N = solver.MakeIntVar(0, 9);
            IntVar D = solver.MakeIntVar(0, 9);
            IntVar M = solver.MakeIntVar(0, 9);
            IntVar O = solver.MakeIntVar(0, 9);
            IntVar R = solver.MakeIntVar(0, 9);
            IntVar Y = solver.MakeIntVar(0, 9);

            IntVar[] vars = {S, E, N, D, M, O, R, Y};

            // SEND + MORE = MONEY:

            IntVar send = (S*1000 + E*100 + N*10 + D).Var();
            IntVar more = (M*1000 + O*100 + R*10 + E).Var();
            IntVar money = (M*10000 + O*1000 + N*100 + E*10 + Y).Var();

            solver.Add(send + more == money);

            // Leading characters must not be zero:

            solver.Add(S != 0);
            solver.Add(M != 0);

            // All characters take different values:
            solver.Add(vars.AllDifferent());

            // Start Solver:

            DecisionBuilder db = solver.MakePhase(vars, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            solver.NewSearch(db);

            while (solver.NextSolution())
            {
                Console.WriteLine(send.Value() + " + " + more.Value() + " = " + money.Value() + "\n");
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