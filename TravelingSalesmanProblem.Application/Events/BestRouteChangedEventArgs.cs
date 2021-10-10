using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Application.Envs;

namespace TravelingSalesmanProblem.Application.Events
{
    public class BestRouteChangedEventArgs : EventArgs
    {
        public List<Point> BestRoute { get; }

        public BestRouteChangedEventArgs(List<Point> bestRoute) => BestRoute = bestRoute;
    }
}