using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Domain.Envs;

namespace TravelingSalesmanProblem.Domain.Events
{
    internal class PointsChangedEventArgs : EventArgs
    {
        internal List<Point> Points { get; }

        internal PointsChangedEventArgs(List<Point> points) => Points = points;
    }
}