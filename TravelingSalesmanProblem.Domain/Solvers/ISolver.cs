using System;
using TravelingSalesmanProblem.Domain.Envs;
using TravelingSalesmanProblem.Domain.Events;

namespace TravelingSalesmanProblem.Domain.Solvers
{
    internal interface ISolver
    {
        event EventHandler<BestRouteUpdatedEventArgs>? BestRouteUpdated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <returns>計算完了/未完了</returns>
        bool Solve(Env env);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>停止成功/すでに停止中</returns>
        bool Stop();
    }
}