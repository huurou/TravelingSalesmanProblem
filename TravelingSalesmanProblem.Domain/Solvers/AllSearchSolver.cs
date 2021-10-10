using System;
using System.Collections.Generic;
using System.Linq;
using TravelingSalesmanProblem.Domain.Envs;
using TravelingSalesmanProblem.Domain.Events;

namespace TravelingSalesmanProblem.Domain.Solvers
{
    /// <summary>
    /// 虱潰し法 O((n-1)!/2) = O(n!)
    /// </summary>
    internal class AllSearchSolver : ISolver
    {
        public event EventHandler<BestRouteChangedEventArgs>? BestRouteChanged;

        private bool stopReqested_ = true;

        public bool Solve(Env env)
        {
            stopReqested_ = false;
            var min = double.PositiveInfinity;
            var all = AllRoute(env.Points.ToList());
            foreach (var route in all)
            {
                if (stopReqested_) return false;
                var d = Point.TotalDistance(route.ToList());
                if (min > d)
                {
                    min = d;
                    BestRouteChanged?.Invoke(this, new(route, d));
                }
            }
            if (stopReqested_) return false;
            stopReqested_ = true;
            return true;
        }

        public bool Stop()
        {
            if (stopReqested_) return false;
            else
            {
                stopReqested_ = true;
                return true;
            }
        }

        private IEnumerable<IEnumerable<Point>> AllRoute(List<Point> points)
        {
            var last = points[^1];
            points.RemoveAt(points.Count - 1);
            var count = points.Count;
            return Inner(points);

            IEnumerable<IEnumerable<Point>> Inner(List<Point> points, int k = -1)
            {
                if (stopReqested_) yield break;
                if (k == -1) k = points.Count;
                if (k == 0) yield return Enumerable.Empty<Point>();
                else
                {
                    for (var i = 0; i < points.Count; i++)
                    {
                        var xs = points.Where((_, index) => i != index).ToList();
                        foreach (var c in Inner(xs, k - 1))
                        {
                            var cc = c.Append(points[i]);
                            if (k == count)
                            {
                                if (cc.ElementAt(0).Id < cc.Last().Id) yield return cc.Append(last);
                            }
                            else yield return cc;
                        }
                    }
                }
            }
        }
    }
}