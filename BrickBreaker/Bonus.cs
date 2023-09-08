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
    class Bonus
    {
        private Canvas canva;
        private Ellipse circle;
        private int x;
        private int y;
        private int dy = 5; // Vertical speed

        private DispatcherTimer moveTimer;

        public Bonus(Canvas grid, int x, int y)
        {
            canva = grid;

            circle = new Ellipse();
            circle.Width = 20;
            circle.Height = 20;
            circle.Fill = new SolidColorBrush(Colors.AliceBlue); // You can change the color

            this.x = x;
            this.y = y;
            Canvas.SetLeft(circle, x);
            Canvas.SetTop(circle, y);

            canva.Children.Add(circle);

            moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(20); // Adjust the interval as needed
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();
        }

        private void MoveTimer_Tick(object sender, object e)
        {
            Move();
        }

        // Move the bonus vertically
        public void Move()
        {
            y += dy;
            Canvas.SetTop(circle, y);

            // Check if the bonus is out of bounds
            if (y > canva.ActualHeight)
            {
                canva.Children.Remove(circle);
            }
            if(CollidesWithPlank())
            {
                BrickGame.TripleBalls();
            }
        }

        public bool CollidesWithPlank()
        {
            // Check for collision between bonus hitbox and plank hitbox
            
            Rect intersection = RectHelper.Intersect(new Rect(x, y, circle.Width, circle.Height), BrickGame.GetPlankHitbox());
            return (intersection.Width > 0 && intersection.Height > 0);
        }

        
    }
}
