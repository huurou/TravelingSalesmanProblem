using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Application.Envs;

namespace TravelingSalesmanProblem.Application.Events
{
    public class PointsChangedEventArgs : EventArgs
    {
        public List<Point> Points { get; }

        public PointsChangedEventArgs(List<Point> points) => Points = points;
    }
}