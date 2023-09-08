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
    class Brick
    {
        const int width = 40;
        const int height = 40;

        Canvas canva;
        Image img;

        public Brick(string picPath, Canvas grid, int x, int y)
        {
            img = new Image();
            img.Width = width;
            img.Height = height;

            img.Source = new BitmapImage(new Uri("ms-appx:///" + picPath));

            #region Testing rect
            //Rectangle img = new Rectangle();
            //img.Width = 100;
            //img.Height = 50;
            //img.Fill = new SolidColorBrush(Colors.Black);
            #endregion

            Canvas.SetLeft(img, x);
            Canvas.SetTop(img, y);

            grid.Children.Add(img);
            this.canva = grid;
        }

        public void dropBonus()
        {
            // TODO: after brick is destroyed drop a bonus
        }
    }
}
