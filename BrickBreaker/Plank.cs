using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace BrickBreaker
{
    enum Direction
    {
        Left = 0,
        Right = 1
    }

    class Plank
    {
        
        private readonly int speed = 24;

        private int x;
        private Canvas canva;
        private Rectangle rect;
        private Rect hitbox;

        public Plank(Canvas grid)
        {
            Size size = GetScreenResolutionInfo();

            #region Rectangle init

            rect = new Rectangle();
            rect.Width = 175;
            rect.Height = 35;
            rect.Fill = new SolidColorBrush(Colors.Black);

            #endregion

            #region Hitbox init

            hitbox = new Rect();
            hitbox.Width = 175;
            hitbox.Height = 35;
            hitbox.X = (int)size.Width / 2;
            hitbox.Y = (int)size.Height - 275;

            #endregion

            #region Set on Canvas

            Canvas.SetLeft(rect, size.Width / 2);
            Canvas.SetTop(rect, size.Height - 250);

            grid.Children.Add(rect);

            #endregion

            this.canva = grid;
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

        public void UpdatePosition(double mouseX)
        {
            Size size = GetScreenResolutionInfo();
            x = (int)mouseX - (int)(rect.Width / 2);

            // Ensure the plank stays within the game area
            if (x < 0)
            {
                x = 0;
            }
            else if (x + rect.Width > size.Width)
            {
                x = (int)(size.Width - rect.Width);
            }

            hitbox.X = x;
            Canvas.SetLeft(rect, x);
        }

        public Rect GetHitbox()
        {
            return hitbox;
        }
    }
}
