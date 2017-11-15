using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the travelling salesman puzzle presented in CP part 3.
 *
 * Given a list of cities (points of interest) and the distances (costs) between each pair of cities. 
 * What is the shortest possible route that visits each city exactly once and returns to the origin city ?
 * There are two ways of getting input to this model: adjacency matrices or XML benchmark files.
 * 
 * Lecture: Introduction to Artificial Intelligence
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    /*
     * Implementation of the Travelling Salesman Problem (TSP) using the OR-Tools routing library.
     */

    public class Tsp
    {
        /*
         * Number of Vehicles:
         */

        public static void Solve(int vehicles, string pathToXml = null)
        {
            /*
             * Add custom distance function
             */

            var dist = (pathToXml == null) ? new Distance() : new XmlDistance(pathToXml);

            /*
             * Generate constraint model
             */

			// Third argument defines depot, i.e. start-end node for round trip.
            var model = new  RoutingModel(dist.MapSize(), vehicles, 0);

            model.SetCost(dist);

            /*
             * This modification forces all Vehicles to visit at least one city.
             */

            /*for (int i = 0; i < Vehicles; i++) {

                IntVar first = model.NextVar(model.Start(i));
                first.SetMax(dist.MapSize() - 1);
            }

            /*
             * Solve problem and display solution
             */

            Assignment assignment = model.Solve();

            if (assignment != null)
            {
                Console.WriteLine("Total Distance: " + assignment.ObjectiveValue() + "\n");

                for (int i = 0; i < vehicles; i++)
                {
                    /*
                     * Display Round Trip:
                     */

                    Console.WriteLine("Round Trip for Vehicle " + i + "\n");

                    for (long node = model.Start(i); node < model.End(i); node = model.Next(assignment, node))
                    {
                        Console.Write(node + " -> ");
                    }

                    Console.WriteLine(model.Start(i) + "\n");

                    /*
                     * Display individual Section Distances for Verification:
                     */

                    var source = (int) model.Start(i);

                    while (source < model.End(i))
                    {
                        var target = (int) model.Next(assignment, source);

                        if (source < dist.MapSize() && target < dist.MapSize())
                        {
                            Console.WriteLine("From " + source + " travel to " + target + " -> distance = " +
                                              dist.Run(source, target));
                        }
                        else if (source < dist.MapSize())
                        {
                            Console.WriteLine("From " + source + " travel to 0 -> distance = " + dist.Run(source, 0));
                        }

                        source = target;
                    }

                    Console.WriteLine("\n");
                }
            }

            Console.ReadKey();
        }

        /*
         * Customized distance functions
         */

        private class Distance : NodeEvaluator2
        {
            protected long[,] Costs =
            {
                {0, 1, 2, 4},
                {3, 0, 1, 4},
                {8, 2, 0, 6},
                {9, 4, 3, 0}
            };

            public override long Run(int i, int j)
            {
                return Costs[i, j];
            }

            public int MapSize()
            {
                return Costs.GetLength(0);
            }
        }

        private class XmlDistance : Distance
        {
            public XmlDistance(String path)
            {
                Parse(path);
            }

            public override long Run(int i, int j)
            {
                return Costs[i, j];
            }

            /*
             * This method parses XML input documents for TSP benchmark examples.
             * http://www.iwr.uni-heidelberg.de/groups/comopt/software/TSPLIB95/XML-TSPLIB/instances/
             */

            private void Parse(String file)
            {
                XDocument doc = XDocument.Load(file);

                XElement[] vertices = doc.Descendants("vertex").ToArray();
                var result = new long[vertices.Length, vertices.Length];

                for (int i = 0; i < vertices.Length; i++)
                {
                    XElement[] edges = vertices[i].Descendants("edge").ToArray();

                    foreach (XElement edge in edges)
                    {

                        int target = Convert.ToInt32(edge.Value);
                        decimal dec = Decimal.Parse(edge.Attribute("cost").Value, NumberStyles.Float, CultureInfo.InvariantCulture);
                        result[i, target] = (long)dec;
                    }
                }

                Costs = result;
            }
        }
    }
}