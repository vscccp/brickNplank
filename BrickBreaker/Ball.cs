using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace BrickBreaker
{
    class Ball
    {
        private int x;
        private int y;
        private int dx = 20;
        private int dy = 20;

        private Canvas canva;
        private Ellipse circle;
        private DispatcherTimer MoveTimer;

        public Ball(Canvas grid)
        {
            MoveTimer = new DispatcherTimer();
            MoveTimer.Interval = TimeSpan.FromTicks(20);
            MoveTimer.Tick += MoveTimer_Tick;

            circle = new Ellipse();
            circle.Height = 50;
            circle.Width = 50;
            circle.Fill = new SolidColorBrush(Colors.DarkGray);

            Size size = GetScreenResolutionInfo();

            x = (int)size.Width / 2;
            y = (int)size.Height - 350;
            Canvas.SetLeft(circle, x);
            Canvas.SetTop(circle, y);

            grid.Children.Add(circle);
            canva = grid;

            MoveTimer.Start();
        }

        private void MoveTimer_Tick(object sender, object e)
        {
            Size size = GetScreenResolutionInfo();

            x += dx;
            if (x < 0)
            {
                sideCollision();
            }    
            if (x > size.Width)
            {
                sideCollision();
            }

            y += dy;
            if (y < 0)
            {
                topCollision();
            }
            if (y > size.Height)
            {
                topCollision();
            }

            Canvas.SetLeft(circle, x);
            Canvas.SetTop(circle, y);
        }

        public static Size GetScreenResolutionInfo()
        {
            var applicationView = ApplicationView.GetForCurrentView();
            var displayInformation = DisplayInformation.GetForCurrentView();
            var bounds = applicationView.VisibleBounds;
            var scale = displayInformation.RawPixelsPerViewPixel;
            var size = new Size(bounds.Width * scale, bounds.Height * scale);
            return size;
        }

        public void topCollision()
        {
            dx *= -1;
            dy *= -1;
        }

        public void sideCollision()
        {
            dx *= -1;
            dy *= -1;
        }
    }
}
