using System;
using TravelingSalesmanProblem.Domain.Envs;
using TravelingSalesmanProblem.Domain.Events;

namespace TravelingSalesmanProblem.Domain.Solvers
{
    internal interface ISolver
    {
        event EventHandler<BestRouteChangedEventArgs>? BestRouteChanged;

        void Solve(Env env);

        void Stop();
    }
}