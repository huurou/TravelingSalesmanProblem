using System;
using System.Collections.Generic;
using System.Linq;
using TravelingSalesmanProblem.Domain.Events;

namespace TravelingSalesmanProblem.Domain.Envs
{
    internal class Env
    {
        internal event EventHandler<PointsChangedEventArgs>? PointsChanged;

        private const int WIDTH = 800;
        private const int HEIGHT = 500;

        private readonly Random rand_ = new();

        internal List<Point> Points { get; private set; } = new();

        internal void Set(int pointCount)
        {
            Points = new(Enumerable.Range(0, pointCount).Select(_ => new Point(rand_.Next(WIDTH), rand_.Next(HEIGHT))));
            PointsChanged?.Invoke(this, new(Points));
        }
    }
}