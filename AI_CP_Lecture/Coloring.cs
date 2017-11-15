using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the graph coloring puzzle presented in CP part 1 and 3.
 *
 * To send information from one cell to another in a cellular network, the transmission ranges of antennas must overlap. 
 * Neighbouring antennas must use different frequencies in order to avoid interference problems. 
 * There is only a very limited number of frequencies available. Assign frequencies to antennas such that not two neighbouring antennas use the same frequency (visualized as colour).
 *
 * There are two Versions: as Constraint and as Optimization Problem.
 * 
 * Lecture: Introduction to Artificial Intelligence
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    internal class Coloring
    {
        /*
         * Color Names for Pretty-Printing:
         */

        private static readonly string[] Names =
        {
            "red", "blue", "green", "yellow", "orange", "black", "white", "purple"
        };

        /*
         * Create Constraint Model and Solve Coloring Problem:
         */

        public static void SolveAsConstraintProblem(int nbColors, int nbNodes, IEnumerable<Tuple<int, int>> edges)
        {
            var solver = new Solver("Coloring");

            // One Decision Variable per Node:
            IntVar[] nodes = solver.MakeIntVarArray(nbNodes, 0, nbColors - 1);

            foreach (var edge in edges)
            {
                solver.Add(nodes[edge.Item1] != nodes[edge.Item2]);
            }

            // Some Symmetry breaking
            solver.Add(nodes[0] == 0);

            /*
             * Start Solver:
             */

            DecisionBuilder db = solver.MakePhase(nodes, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            Console.WriteLine("Coloring Problem:\n\n");

            solver.NewSearch(db);

            while (solver.NextSolution())
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (i < Names.Length)
                    {
                        Console.WriteLine("Node " + i + " obtains color " + Names.GetValue(nodes[i].Value()));
                    }
                    else
                    {
                        Console.WriteLine("Node " + i + " obtains color " + nodes[i].Value());
                    }
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
         * Create Optimization Model and Solve Coloring Problem:
         */

        public static void SolveAsOptimizationProblem(int nbNodes, IEnumerable<Tuple<int, int>> edges)
        {
            var solver = new Solver("Coloring");

            // The colors for each node:
            IntVar[] nodes = solver.MakeIntVarArray(nbNodes, 0, nbNodes - 1);

            foreach (var edge in edges)
            {
                solver.Add(nodes[edge.Item1] != nodes[edge.Item2]);
            }

            // Some Symmetry breaking
            solver.Add(nodes[0] == 0);

            // The number of times each color is used:
            IntVar[] colors = solver.MakeIntVarArray(nbNodes, 0, nbNodes - 1);

            for (int i = 0; i < colors.Length; i++)
            {
                solver.Add(solver.MakeCount(nodes, i, colors[i]));
            }

            // Objective function = number of colors used:
            IntVar obj = solver.MakeSum((from j in colors select (j > 0).Var()).ToArray()).Var();

            /*
             * Start Solver:
             */

            DecisionBuilder db = solver.MakePhase(nodes, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            // Remember only the best solution found:
            SolutionCollector collector = solver.MakeBestValueSolutionCollector(false);
            collector.AddObjective(obj);

            // What to remember in addition to the objective function value:
            collector.Add(nodes);

            Console.WriteLine("Coloring Problem:\n");

            if (solver.Solve(db, collector))
            {
                // Extract best solution found:
                Assignment sol = collector.Solution(0);

                Console.WriteLine("Solution found with " + sol.ObjectiveValue() + " colors.\n");

                for (int i = 0; i < nodes.Length; i++)
                {
                    long v = sol.Value(nodes[i]);

                    if (i < Names.Length)
                    {
                        Console.WriteLine("Nodes " + i + " obtains color " + Names.GetValue(v));
                    }
                    else
                    {
                        Console.WriteLine("Nodes " + i + " obtains color " + v);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nSolutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            Console.ReadKey();
        }
    }
}