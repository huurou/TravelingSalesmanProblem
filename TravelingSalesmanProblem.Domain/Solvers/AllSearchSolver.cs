using System;
using System.Collections.Generic;
using System.Linq;
using TravelingSalesmanProblem.Domain.Envs;
using TravelingSalesmanProblem.Domain.Events;

namespace TravelingSalesmanProblem.Domain.Solvers
{
    /// <summary>
    /// 虱潰し法 O(n!)
    /// </summary>
    internal class AllSearchSolver : ISolver
    {
        public event EventHandler<BestRouteChangedEventArgs>? BestRouteChanged;

        public void Solve(Env env)
        {
            var points = env.Points;
            var min = double.PositiveInfinity;
            var perm = Perm(points);
            foreach (var element in perm)
            {
                var d = Point.TotalDistance(element);
                if (min > d)
                {
                    min = d;
                    BestRouteChanged?.Invoke(this, new(element, d));
                }
            }
        }

        public void Stop() => throw new NotImplementedException();

        private IEnumerable<IEnumerable<T>> Perm<T>(IEnumerable<T> items, int? k = null)
        {
            if (k == null) k = items.Count();
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