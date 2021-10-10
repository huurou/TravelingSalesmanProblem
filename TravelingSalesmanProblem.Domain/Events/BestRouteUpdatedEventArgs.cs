using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Domain.Envs;

namespace TravelingSalesmanProblem.Domain.Events
{
    internal class BestRouteUpdatedEventArgs : EventArgs
    {
        internal IEnumerable<Point> BestRoute { get; }

        internal double TotalDistance { get; }

        public BestRouteUpdatedEventArgs(IEnumerable<Point> bestRoute, double totalDistance)
        {
            BestRoute = bestRoute;
            TotalDistance = totalDistance;
        }
    }
}