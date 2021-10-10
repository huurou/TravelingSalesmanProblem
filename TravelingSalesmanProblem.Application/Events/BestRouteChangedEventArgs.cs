using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Application.Envs;

namespace TravelingSalesmanProblem.Application.Events
{
    public class BestRouteChangedEventArgs : EventArgs
    {
        public List<Point> BestRoute { get; }
        public double TotalDistance { get; }

        public BestRouteChangedEventArgs(List<Point> bestRoute, double totalDistance)
        {
            BestRoute = bestRoute;
            TotalDistance = totalDistance;
        }
    }
}