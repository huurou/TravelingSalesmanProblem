using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TravelingSalesmanProblem.Presentation.WPF.Views.Controls
{
    /// <summary>
    /// PointsCanvas.xaml の相互作用ロジック
    /// </summary>
    public partial class PointsCanvas : UserControl
    {
        #region DependencyProperty

        public IEnumerable<Point> Points
        {
            get => (IEnumerable<Point>)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register(
            "Points", typeof(IEnumerable<Point>), typeof(PointsCanvas), new FrameworkPropertyMetadata(default(IEnumerable<Point>), (d, e) =>
            {
                if (d is PointsCanvas self &&
                   e.NewValue is IEnumerable<Point> value)
                {
                    self.DrawPoints(value);
                }
            }));

        public IEnumerable<Point> Route
        {
            get => (IEnumerable<Point>)GetValue(RouteProperty);
            set => SetValue(RouteProperty, value);
        }

        // Using a DependencyProperty as the backing store for Route.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RouteProperty = DependencyProperty.Register(
            "Route", typeof(IEnumerable<Point>), typeof(PointsCanvas), new FrameworkPropertyMetadata(default(IEnumerable<Point>), (d, e) =>
            {
                if (d is PointsCanvas self &&
                    e.NewValue is IEnumerable<Point> value)
                {
                    self.DrawPoints(value);
                    self.DrawRoute(value);
                }
            }));

        #endregion DependencyProperty

        private readonly List<Ellipse> points_ = new();
        private readonly List<Line> route_ = new();
        private readonly SolidColorBrush routeBrush_ = new() { Color = Colors.DeepSkyBlue };

        public PointsCanvas() => InitializeComponent();

        private void DrawPoints(IEnumerable<Point> points)
        {
            ClearPoints();
            ClearRoute();
            foreach (var point in points)
            {
                DrawPoint(point);
            }
        }

        private void DrawPoint(Point point)
        {
            var e = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = routeBrush_,
            };
            Canvas.Children.Add(e);
            Canvas.SetLeft(e, point.X - e.Width / 2);
            Canvas.SetTop(e, point.Y - e.Width / 2);
            points_.Add(e);
        }

        private void DrawRoute(IEnumerable<Point> route)
        {
            ClearRoute();
            var r = route.ToList();
            for (var i = 0; i < r.Count; i++)
            {
                DrawPath(r[i], r[i == r.Count - 1 ? 0 : i + 1]);
            }
        }

        private void DrawPath(Point point1, Point point2)
        {
            var l = new Line
            {
                X1 = point1.X,
                Y1 = point1.Y,
                X2 = point2.X,
                Y2 = point2.Y,
                StrokeThickness = 2,
                Stroke = routeBrush_,
            };
            Canvas.Children.Add(l);
            route_.Add(l);
        }

        private void ClearPoints()
        {
            points_.ForEach(x => Canvas.Children.Remove(x));
            points_.Clear();
        }

        private void ClearRoute()
        {
            route_.ForEach(x => Canvas.Children.Remove(x));
            route_.Clear();
        }
    }
}