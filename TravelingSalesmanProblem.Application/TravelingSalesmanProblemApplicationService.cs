using System;
using System.Linq;
using System.Threading.Tasks;
using TravelingSalesmanProblem.Application.Enums;
using TravelingSalesmanProblem.Application.Events;
using TravelingSalesmanProblem.Domain.Envs;
using TravelingSalesmanProblem.Domain.Solvers;
using static TravelingSalesmanProblem.Application.Enums.SolverType;
using Point = TravelingSalesmanProblem.Application.Envs.Point;

namespace TravelingSalesmanProblem.Application
{
    public class TravelingSalesmanProblemApplicationService
    {
        public event EventHandler<PointsChangedEventArgs>? PointsChanged;
        public event EventHandler<BestRouteUpdatedEventArgs>? BestRouteUpdated;

        private readonly Env env_ = new();
        private ISolver solver_ = new AllSearchSolver();

        public TravelingSalesmanProblemApplicationService()
        {
            env_.PointsChanged += (s, e) => PointsChanged?.Invoke(this, new(new(e.Points.Select(x => new Point(x.X, x.Y)))));
            solver_.BestRouteUpdated += (s, e) => BestRouteUpdated?.Invoke(this, new(new(e.BestRoute.Select(x => new Point(x.X, x.Y))), e.TotalDistance));
        }

        public void SetEnv(int pointCount) => env_.Set(pointCount);

        public async Task<bool> Solve() => await Task.Run(() => solver_.Solve(env_));

        public bool Stop() => solver_.Stop();

        public void SelectSolver(SolverType type)
        {
            solver_ = type switch
            {
                AllSearch => new AllSearchSolver(),
                BitDp => new BitDpSolver(),
                _ => throw new NotImplementedException(),
            };
            solver_.BestRouteUpdated += (s, e) => BestRouteUpdated?.Invoke(this, new(new(e.BestRoute.Select(x => new Point(x.X, x.Y))), e.TotalDistance));
        }
    }
}