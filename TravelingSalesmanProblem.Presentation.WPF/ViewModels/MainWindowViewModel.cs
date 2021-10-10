using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TravelingSalesmanProblem.Application;

namespace TravelingSalesmanProblem.Presentation.WPF.ViewModels
{
    internal class MainWindowViewModel : NotifycationObject
    {
        #region BindingProperty

        private readonly TravelingSalesmanProblemApplicationService appService_ = new();

        private ObservableCollection<Point> points_ = new();
        private ObservableCollection<Point> route_ = new();
        private double totalDistance_;
        private int pointCount_ = 10;
        private string state_ = "";

        public ObservableCollection<Point> Points { get => points_; set => SetProperty(ref points_, value); }
        public ObservableCollection<Point> Route { get => route_; set => SetProperty(ref route_, value); }
        public double TotalDistance { get => totalDistance_; set => SetProperty(ref totalDistance_, value); }
        public int PointCount { get => pointCount_; set => SetProperty(ref pointCount_, value); }
        public string State { get => state_; set => SetProperty(ref state_, value); }

        #endregion BindingProperty

        #region BindingCommand

        private DelegateCommand? newEnvCmd_;
        private DelegateCommand? solveCmd_;
        private DelegateCommand? stopCmd_;

        public DelegateCommand NewEnvCmd => newEnvCmd_ ??= new(() => appService_.SetEnv(PointCount));
        public DelegateCommand SolveCmd => solveCmd_ ??= new(Solve);
        public DelegateCommand StopCmd => stopCmd_ ??= new(Stop);

        #endregion BindingCommand

        internal MainWindowViewModel()
        {
            appService_.PointsChanged += (s, e) => Points = new(e.Points.Select(x => new Point(x.X, x.Y)));
            appService_.BestRouteChanged += (s, e) =>
            {
                Route = new(e.BestRoute.Select(x => new Point(x.X, x.Y)));
                TotalDistance = e.TotalDistance;
                State = $"{e.TotalDistance}\n{State}";
            };
        }

        private void Solve()
        {
            appService_.Run();
            State = $"Finish!\n{State}";
        }

        private void Stop()
        {
            appService_.Stop();
            State = $"Stop!\n{State}";
        }

        internal void OnContentRendered() => appService_.SetEnv(PointCount);
    }
}