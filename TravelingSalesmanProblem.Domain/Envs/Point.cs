using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelingSalesmanProblem.Domain.Envs
{
    internal struct Point
    {
        internal int Id { get; }
        internal int X { get; }
        internal int Y { get; }

        public Point(int id, int x, int y)
        {
            Id = id;
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
            return Math.Round(total, 3);
        }

        internal static double Distance(Point p1, Point p2) => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));

        public override string ToString() => $"({Id} {X,3},{Y,3})";
    }
}