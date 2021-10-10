using System;
using System.Collections.Generic;
using TravelingSalesmanProblem.Domain.Envs;
using TravelingSalesmanProblem.Domain.Events;

namespace TravelingSalesmanProblem.Domain.Solvers
{
    internal class BitDpSolver : ISolver
    {
        // dpの定義
        // dp[S][v]:=頂点0からスタートして、{0,1,2,…,n-1} の部分集合 S を巡回する |S|! 通りの経路のうち最短のものの距離。
        // ただし、最後に頂点 v に到達した時のみを考える。
        //
        // dpの初期化
        // dp[0][0]=0
        // その他は全てINF
        //
        // dpの更新
        // dp[S∪v][v]=min(dp[S][u]+cost(u,v)) u∈S
        //
        // 答え
        // dp[{0,1,...,n-1}][0]
        public event EventHandler<BestRouteUpdatedEventArgs>? BestRouteUpdated;

        public bool Solve(Env env)
        {
            var points = env.Points;
            var count = points.Count;
            // 部分集合S’を通ってvにいる時の後に通る最短経路長
            var dp = new (double d, (long x, int y) xy)[1L << count, count];
            for (var i = 0; i < dp.GetLength(0); i++)
            {
                for (var j = 0; j < dp.GetLength(1); j++)
                {
                    dp[i, j] = (double.PositiveInfinity, (0, 0));
                }
            }
            dp[0, 0] = (0, (0, 0));
            for (var S = 0L; S < dp.GetLongLength(0); S++)
            {
                for (var v = 0; v < dp.GetLength(1); v++)
                {
                    for (var u = 0; u < dp.GetLength(1); u++)
                    {
                        // u含まない場合を除外
                        if (S != 0 && (S & (1L << u)) == 0 ||
                            // vはすでに訪れているはず
                            (S & (1L << v)) != 0 ||
                            // 同じ場所を訪問することはできない
                            v == u) continue;
                        // dp[S][v]にはbitで表された集合Sの順列のうち、
                        // 末項がvであるものの最小コストが代入されているため、
                        // そのような順列のなかで、これまで記録されていた最小コストよりも
                        // 少ないコストの順列を見つけることができた場合
                        // そのコストを記録しています。
                        var val = dp[S, u].d + Point.Distance(points[v], points[u]);
                        if (dp[S | (1L << v), v].d > val)
                        {
                            dp[S | (1L << v), v] = (val, (S, u));
                        }
                    }
                }
            }
            var (d, xy) = dp[(1L << count) - 1, 0];
            var (x, y) = xy;
            var bestRoute = new List<Point> { Capacity = count };
            while (true)
            {
                bestRoute.Add(points[y]);
                if (x == 0 && y == 0) break;
                (x, y) = dp[x, y].xy;
            }
            BestRouteUpdated?.Invoke(this, new(bestRoute, d));
            return true;
        }

        public bool Stop() => false;
    }
}