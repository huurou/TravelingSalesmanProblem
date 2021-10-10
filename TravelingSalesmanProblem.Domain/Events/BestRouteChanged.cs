using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Domain.Envs;

namespace TravelingSalesmanProblem.Domain.Events
{
    internal class BestRouteChangedEventArgs : EventArgs
    {
        internal List<Point> BestRoute { get; }

        internal BestRouteChangedEventArgs(List<Point> bestRoute) => BestRoute = bestRoute;
    }
}