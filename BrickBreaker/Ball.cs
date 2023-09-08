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
        private int dx = 7;
        private int dy = 9;

        private Canvas canva;
        private Ellipse circle;
        private Rect hitbox;
        private DispatcherTimer MoveTimer;

        public Ball(Canvas grid)
        {
            #region Timer init
            MoveTimer = new DispatcherTimer();
            MoveTimer.Interval = TimeSpan.FromTicks(20);
            MoveTimer.Tick += MoveTimer_Tick;
            #endregion

            #region Circle init
            circle = new Ellipse();
            circle.Height = 30;
            circle.Width = 30;
            circle.Fill = new SolidColorBrush(Colors.DarkGray);
            #endregion

            #region Put ball on canvas

            Size size = GetScreenResolutionInfo();
            x = (int)size.Width / 2;
            y = (int)size.Height - 350;
            Canvas.SetLeft(circle, x);
            Canvas.SetTop(circle, y);

            grid.Children.Add(circle);
            #endregion

            #region Hitbox init

            hitbox = new Rect();
            hitbox.Width = 30;
            hitbox.Height = 30;
            hitbox.X = x;
            hitbox.Y = y;

            #endregion

            canva = grid;

            MoveTimer.Start();
        }

        private void MoveTimer_Tick(object sender, object e)
        {
            Size size = GetScreenResolutionInfo();

            x += dx;
            if (x < 0 || x > size.Width - 100)
            {
                SideCollision();
            }

            y += dy;
            if (y < 0 || y > size.Height - 250)
            {
                TopCollision();
            }

            hitbox.X = x;
            hitbox.Y = y;
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

        public void TopCollision()
        {
            dy *= -1;
        }

        public void SideCollision()
        {
            dx *= -1;
        }

        public Rect GetHitbox()
        {
            return hitbox;
        }
    }
}
