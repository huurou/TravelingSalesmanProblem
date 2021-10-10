using System;
using TravelingSalesmanProblem.Domain.Envs;
using TravelingSalesmanProblem.Domain.Events;

namespace TravelingSalesmanProblem.Domain.Solvers
{
    internal class AllSearchSolver : ISolver
    {
        public event EventHandler<BestRouteChangedEventArgs>? BestRouteChanged;

        public void Solve(Env env)
        {
            var points = env.Points;

            BestRouteChanged?.Invoke(this, new(points));
        }

        public void Stop() => throw new NotImplementedException();
    }
}