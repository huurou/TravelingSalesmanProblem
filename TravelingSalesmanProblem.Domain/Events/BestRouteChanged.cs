using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Domain.Envs;

namespace TravelingSalesmanProblem.Domain.Events
{
    internal class BestRouteChangedEventArgs : EventArgs
    {
        internal IEnumerable<Point> BestRoute { get; }

        internal double TotalDistance { get; }

        public BestRouteChangedEventArgs(IEnumerable<Point> bestRoute, double totalDistance)
        {
            BestRoute = bestRoute;
            TotalDistance = totalDistance;
        }
    }
}