using System;
using System.Collections.Generic;

namespace AI_CP_Lecture
{
    public class Starter
    {

        /*
         * Start Examples: 
         */

        static void Main(string[] args)
        {

            //Coloring.SolveAsConstraintProblem(3, 5, Graph);
            //Coloring.SolveAsOptimizationProblem(5, Graph);

            // Cryptogram.Solve();
            //MagicSquare. over 5 takes a very long time
            MagicSquare.Solve(3);
            MagicSquare.Solve(4);
            //  MagicSquare.Solve(5);
            //  MagicSquare.Solve(6);
            // MagicSquare.Solve(7);
            //   Grocery.Solve();

            //KnightTour.Solve(6);

            //    Sudoku.Solve(Sudoku1);
            //    Sudoku.Solve(Sudoku2);
            
            SumSudoku.Solve(SumFrameSudoku1);

            

            //Tsp.Solve(2);
            //Tsp.Solve(2, "../../data/burma14.xml");
            //Tsp.Solve(3, "../../data/att48.xml");

            //Xkcd.Solve();

        }

        /*
         * Binoxxo Puzzles:
         */

        public static readonly string[,] Binoxxo1 =
        {
            {"", "X", "", "", "", "", "", "", "", ""},
            {"", "", "", "O", "", "", "", "", "", ""},
            {"", "X", "X", "", "", "", "", "", "", ""},
            {"", "", "", "", "O", "O", "", "", "", "O"},
            {"X", "", "", "", "", "", "X", "X", "", ""},
            {"", "X", "", "", "X", "", "", "", "", ""},
            {"", "", "", "O", "", "", "X", "", "", ""},
            {"", "O", "", "", "", "", "", "O", "", "O"},
            {"", "", "", "", "O", "", "", "", "", ""},
            {"O", "", "", "", "", "", "", "", "", "O"}
        };

        public static readonly string[,] Binoxxo2 =
        {
            {"", "", "O", "O", "", "", "", "", "", ""},
            {"", "", "", "O", "", "", "", "", "", ""},
            {"O", "", "", "", "X", "", "", "X", "", ""},
            {"", "", "", "", "", "O", "", "", "", ""},
            {"O", "", "", "O", "", "", "", "", "", ""},
            {"", "", "", "O", "", "", "", "", "X", ""},
            {"", "", "", "", "", "", "", "X", "X", ""},
            {"X", "", "", "", "", "", "", "X", "", ""},
            {"", "", "", "", "", "O", "", "", "", "O"},
            {"", "", "", "", "X", "", "O", "", "", ""}
        };

        /*
         * Sudoku Puzzle:
         */

        public static readonly string[,] Sudoku1 =
        {
            {"1", "", "", "", "3", "", "", "8", ""},
            {"", "6", "", "4", "", "", "", "", ""},
            {"", "", "4", "", "", "9", "3", "", ""},
            {"", "4", "5", "", "", "6", "", "", "7"},
            {"9", "", "", "", "", "5", "", "", ""},
            {"", "", "8", "", "", "3", "", "2", ""},
            {"", "", "", "", "", "", "9", "5", "6"},
            {"", "2", "", "", "", "", "", "", ""},
            {"", "", "7", "", "", "8", "", "1", ""}
        };

		public static readonly string[,] SumSudoku1 =
		{
			{"15", "18", "12", "11", "21", "13", "15", "17", "13"},//top (left to right)
			{"22", "8", "15", "22", "12", "11", "15", "13", "17"},//right (top to bottom)
			{"15", "9", "21", "10", "16", "19", "13", "15", "17"},//bottom (left to right)
			{"8", "15", "22", "11", "13", "21", "18", "19", "8"}//left (top to bottom)
		};

		public static readonly string[,] Sudoku2 =
        {
            {"4", "", "8", "", "", "", "", "", ""},
            {"", "", "", "1", "7", "", "", "", ""},
            {"", "", "", "", "8", "", "", "3", "2"},
            {"", "", "6", "", "", "8", "2", "5", ""},
            {"", "9", "", "", "", "", "", "8", ""},
            {"", "3", "7", "6", "", "", "9", "", ""},
            {"2", "7", "", "", "5", "", "", "", ""},
            {"", "", "", "", "1", "4", "", "", ""},
            {"", "", "", "", "", "", "6", "", "4"}
        };

        /*
         * Fabian's Xmas Puzzle:
         */

        public static readonly string[,] Xmas1 =
        {
            {"2", " ", " ", " ", "1"},
            {"2", " ", "4", "3", " "},
            {" ", "2", " ", "1", " "},
            {" ", "1", " ", "3", " "},
            {"1", " ", " ", " ", " "}
        };

        public static readonly string[,] Xmas2 =
        {
            {"1", " ", " ", " ", "2", " ", " ", "1"},
            {" ", "1", "2", " ", "3", " ", " ", "1"},
            {" ", "2", " ", "1", " ", " ", " ", "0"},
            {" ", "2", "1", " ", " ", "2", "3", "1"},
            {" ", " ", " ", "2", " ", " ", " ", " "},
            {"1", " ", " ", " ", "4", "3", " ", " "},
            {" ", "1", " ", " ", "4", " ", "3", " "},
            {"1", " ", " ", "2", " ", "2", " ", "1"}
        };

        /*
         * Sum Frame Sudoku Puzzle:
         */

        public static readonly Tuple<int[], int[], int[], int[]> SumFrameSudoku1 =
            new Tuple<int[], int[], int[], int[]>(
                // Top:
                new[] { 21, 12, 12, 13, 14, 18, 10, 19, 16 },
                // Right:
                new[] { 20, 15, 10, 22, 8, 15, 17, 15, 13 },
                // Bottom:
                new[] { 17, 9, 19, 18, 13, 14, 23, 15, 7 },
                // Left:
                new[] { 12, 12, 21, 14, 14, 17, 14, 9, 22 });

        public static readonly Tuple<int[], int[], int[], int[]> SumFrameSudoku2 =
            new Tuple<int[], int[], int[], int[]>(
                // Top:
                new[] { 12, 18, 15, 21, 6, 18, 13, 15, 17 },
                // Right:
                new[] { 18, 20, 7, 16, 15, 14, 16, 18, 11 },
                // Bottom:
                new[] { 18, 6, 21, 16, 20, 9, 12, 20, 13 },
                // Left:
                new[] { 13, 9, 23, 11, 21, 13, 16, 15, 14 });

        /*
         * Knapsack:
         */

        public static readonly Tuple<int, int[], int[], string[]> Knapsack1 =
            new Tuple<int, int[], int[], string[]>(
                // Total knapsack capacity:
                9,
                // Item weights:
                new[] { 4, 3, 2 },
                // Item values:
                new[] { 15, 10, 7 },
                // Items names:
                new[] { "Whiskey", "Perfume", "Cigarettes" });


        public static readonly Tuple<int, int[], int[], string[]> Knapsack2 =
            new Tuple<int, int[], int[], string[]>(
                // Total knapsack capacity:
                29,
                // Item weights:
                new[] { 4, 3, 2, 6 },
                // Item values:
                new[] { 12, 8, 2, 15 },
                // Items names:
                new[] { "Whiskey", "Perfume", "Corned Beef", "Riffle" });

        /*
         * Graph:
         */

        public static readonly Tuple<int, int>[] Graph =
        {
            new Tuple<int, int>(4, 1),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(4, 0),
            new Tuple<int, int>(1, 3),
            new Tuple<int, int>(3, 0),
            new Tuple<int, int>(3, 2)
        };

        /*
         * Random Distance Matrices: 
         */

        public static long[,] GenerateRandomDistanceMatrix(int dim)
        {
            var costs = new long[dim + 1, dim + 1];
            var rand = new Random();
            for (int i = 0; i < dim + 1; i++)
            {
                for (int j = 0; j < dim + 1; j++)
                {
                    costs[i, j] = (i == j) ? 0 : rand.Next(1000);
                }
            }

            return FloydWarshall(costs);
        }

        /*
         * Floyd-Warshall Algorithm for shortest distances:
         */

        private static long[,] FloydWarshall(long[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int v = 0; v < matrix.GetLength(0); v++)
                {
                    for (int w = 0; w < matrix.GetLength(0); w++)
                    {
                        if (matrix[v, w] > matrix[v, i] + matrix[i, w])
                        {
                            matrix[v, w] = matrix[v, i] + matrix[i, w];
                        }
                    }
                }
            }

            return matrix;
        }

        /*
         * Nonogram Puzzle:
         */

        public static readonly Tuple<List<int>[], List<int>[]> Nonogram1 = new Tuple<List<int>[], List<int>[]>(
            // Rows:
            new[]
            {
                new List<int> {3, 4, 2, 1},
                new List<int> {2, 1, 1, 1, 2, 1},
                new List<int> {1, 1, 1, 2, 1},
                new List<int> {1, 4, 4},
                new List<int> {1, 1, 1, 1, 2},
                new List<int> {2, 1, 1, 1, 1, 2},
                new List<int> {3, 1, 1, 1, 2},
                new List<int> {1, 1},
                new List<int> {4, 3, 4},
                new List<int> {1, 1, 1, 2, 1, 1},
                new List<int> {1, 1, 1, 1, 1, 1},
                new List<int> {4, 1, 1, 4},
                new List<int> {1, 1, 1, 1, 1, 1},
                new List<int> {1, 1, 1, 2, 1, 1},
                new List<int> {1, 1, 3, 1, 1}
            },

            // Columns:
            new[]
            {
                new List<int> {5, 7},
                new List<int> {2, 2, 1, 1},
                new List<int> {1, 1, 1, 1},
                new List<int> {2, 2, 7},
                new List<int> {1},
                new List<int> {7, 7},
                new List<int> {1, 1, 1, 1},
                new List<int> {1, 1, 2, 2},
                new List<int> {7, 5},
                new List<int> {1},
                new List<int> {7, 7},
                new List<int> {4, 1, 1},
                new List<int> {4, 1, 1},
                new List<int> {7, 7}
            });


        public static readonly Tuple<List<int>[], List<int>[]> Nonogram2 = new Tuple<List<int>[], List<int>[]>(
            // Rows:
            new[]
            {
                new List<int> {2, 2},
                new List<int> {1, 3, 1, 4},
                new List<int> {3, 3, 1, 1},
                new List<int> {2, 2, 2},
                new List<int> {1, 3, 1},
                new List<int> {1, 1, 1, 2, 2},
                new List<int> {1, 1, 2},
                new List<int> {2, 1, 5},
                new List<int> {5, 3, 3},
                new List<int> {4, 4},
                new List<int> {1, 2, 1},
                new List<int> {2, 3, 2},
                new List<int> {2, 2, 2},
                new List<int> {3, 3},
                new List<int> {6}
            },

            // Columns:
            new[]
            {
                new List<int> {1, 5},
                new List<int> {4, 2},
                new List<int> {1, 1, 1},
                new List<int> {2, 2},
                new List<int> {1, 5},
                new List<int> {2, 1, 2, 4},
                new List<int> {1, 1, 2},
                new List<int> {4, 2, 3},
                new List<int> {1, 2, 2, 2, 1},
                new List<int> {5, 1, 1},
                new List<int> {3, 1, 2, 2},
                new List<int> {3, 4, 1},
                new List<int> {1, 4, 2},
                new List<int> {1, 2, 2, 2},
                new List<int> {2, 2, 3}
            });
    }
}
