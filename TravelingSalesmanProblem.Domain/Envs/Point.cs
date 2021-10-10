using System;

namespace TravelingSalesmanProblem.Domain.Envs
{
    internal struct Point
    {
        internal int X { get; }
        internal int Y { get; }

        internal Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal static double Distance(Point p1, Point p2) => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }
}