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
    class Brick
    {
        const int width = 40;
        const int height = 40;

        private Canvas canva;
        private Image img;
        private Rect hitbox;

        public Brick(string picPath, Canvas grid, int x, int y)
        {
            hitbox = new Rect();
            hitbox.Width = 50;
            hitbox.Height = 50;
            hitbox.X = x;
            hitbox.Y = y;

            #region Image initialization
            img = new Image();
            img.Width = width;
            img.Height = height;

            img.Source = new BitmapImage(new Uri("ms-appx:///" + picPath));
            #endregion

            #region Testing rect
            //Rectangle img = new Rectangle();
            //img.Width = 100;
            //img.Height = 50;
            //img.Fill = new SolidColorBrush(Colors.Black);
            #endregion

            #region Adding to canva
            Canvas.SetLeft(img, x);
            Canvas.SetTop(img, y);

            grid.Children.Add(img);
            #endregion

            this.canva = grid;
        }

        public void RemoveBrick()
        {
            canva.Children.Remove(img);
            DropBonus();
        }

        public void DropBonus()
        {
            if(RollForDropBonus())
            {
                // Determine the position to drop the bonus (you can adjust this logic)
                int bonusX = (int)hitbox.X + width / 2;
                int bonusY = (int)hitbox.Y + height / 2;

                // Create a bonus object and add it to the canvas
                Bonus bonus = new Bonus(canva, bonusX, bonusY);
            }
        }

        private static bool RollForDropBonus()
        {
            Random random = new Random();
            return random.Next(100) < 10;
        }



        public Rect GetHitbox()
        {
            return hitbox;
        }
    }
}
