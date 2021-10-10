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

        public void Solve(Env env)
        {
            var min = double.PositiveInfinity;
            var all = AllRoute(env.Points.ToList());
            foreach (var route in all)
            {
                var d = Point.TotalDistance(route);
                if (min > d)
                {
                    min = d;
                    BestRouteChanged?.Invoke(this, new(route, d));
                }
            }
        }

        public void Stop() => throw new NotImplementedException();

        private static IEnumerable<IEnumerable<Point>> AllRoute(List<Point> points)
        {
            var last = points[^1];
            points.RemoveAt(points.Count - 1);
            var count = points.Count;
            return Inner(points);

            IEnumerable<IEnumerable<Point>> Inner(IEnumerable<Point> points, int k = -1)
            {
                if (k == -1) k = points.Count();
                if (k == 0) yield return Enumerable.Empty<Point>();
                else
                {
                    for (var i = 0; i < points.Count(); i++)
                    {
                        var xs = points.Where((_, index) => i != index);
                        foreach (var c in Inner(xs, k - 1))
                        {
                            var cc = c.Append(points.ElementAt(i));
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

        private IEnumerable<IEnumerable<T>> Perm<T>(IEnumerable<T> items, int k = -1)
        {
            if (k == -1)
            {
                k = items.Count();
            }
            if (k == 0) yield return Enumerable.Empty<T>();
            else
            {
                for (var i = 0; i < items.Count(); i++)
                {
                    var xs = items.Where((_, index) => i != index);
                    foreach (var c in Perm(xs, k - 1))
                    {
                        yield return Before(c, items.ElementAt(i));
                    }
                }
            }

            static IEnumerable<U> Before<U>(IEnumerable<U> items, U first)
            {
                yield return first;
                foreach (var i in items)
                {
                    yield return i;
                }
            }
        }
    }
}