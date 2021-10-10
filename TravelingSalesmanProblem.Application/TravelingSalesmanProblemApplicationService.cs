using System;
using System.Linq;
using TravelingSalesmanProblem.Application.Events;
using TravelingSalesmanProblem.Domain.Envs;
using TravelingSalesmanProblem.Domain.Solvers;
using Point = TravelingSalesmanProblem.Application.Envs.Point;

namespace TravelingSalesmanProblem.Application
{
    public class TravelingSalesmanProblemApplicationService
    {
        public event EventHandler<PointsChangedEventArgs>? PointsChanged;
        public event EventHandler<BestRouteChangedEventArgs>? BestRouteChanged;

        private readonly Env env_ = new();
        private readonly ISolver solver_;

        public TravelingSalesmanProblemApplicationService()
        {
            solver_ = new AllSearchSolver();

            env_.PointsChanged += (s, e) => PointsChanged?.Invoke(this, new(new(e.Points.Select(x => new Point(x.X, x.Y)))));
            solver_.BestRouteChanged += (s, e) => BestRouteChanged?.Invoke(this, new(new(e.BestRoute.Select(x => new Point(x.X, x.Y)))));
        }

        public void SetEnv(int pointCount) => env_.Set(pointCount);

        public void Run() => solver_.Solve(env_);

        public void Stop() => solver_.Stop();
    }
}