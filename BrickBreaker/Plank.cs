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
        
        private readonly int speed = 12;

        private int x;
        private Canvas canva;
        private Rectangle rect;

        public Plank(Canvas grid)
        {
            #region Testing rect
            rect = new Rectangle();
            rect.Width = 175;
            rect.Height = 35;
            rect.Fill = new SolidColorBrush(Colors.Black);
            #endregion

            Size size = GetScreenResolutionInfo();
            Canvas.SetLeft(rect, size.Width / 2);
            Canvas.SetTop(rect, size.Height - 250);

            grid.Children.Add(rect);
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

        public void Move(Direction dir)
        {
            Size size = GetScreenResolutionInfo();

            switch (dir)
            {
                case Direction.Left:
                    x -= speed;
                    if(x < 0)
                    {
                        x = 0;
                    }
                    Canvas.SetLeft(rect, x);
                    break;
                case Direction.Right:
                    x += speed;
                    if(x > size.Width)
                    {
                        x = (int)size.Width;
                    }
                    Canvas.SetLeft(rect, x);
                    break;
            }
        }
    }
}
