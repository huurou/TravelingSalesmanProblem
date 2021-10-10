using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Application.Envs;

namespace TravelingSalesmanProblem.Application.Events
{
    public class BestRouteUpdatedEventArgs : EventArgs
    {
        public List<Point> BestRoute { get; }
        public double TotalDistance { get; }

        public BestRouteUpdatedEventArgs(List<Point> bestRoute, double totalDistance)
        {
            BestRoute = bestRoute;
            TotalDistance = totalDistance;
        }
    }
}