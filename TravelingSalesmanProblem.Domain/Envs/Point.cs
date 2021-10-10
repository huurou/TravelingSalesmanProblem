using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesmanProblem.Domain.Envs
{
    internal struct Point
    {
        internal int X { get; }
        internal int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal static double TotalDistance(IEnumerable<Point> points)
        {
            var total = 0D;
            for (var i = 0; i < points.Count(); i++)
            {
                total += i == points.Count() - 1
                    ? Distance(points.ElementAt(i), points.ElementAt(0))
                    : Distance(points.ElementAt(i), points.ElementAt(i + 1));
            }
            return total;
        }

        internal static double Distance(Point p1, Point p2) => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));

        public override string ToString() => $"({X,3},{Y,3})";
    }
}