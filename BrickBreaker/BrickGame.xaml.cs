using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BrickBreaker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrickGame : Page
    {
        private bool canCollideWithPlank = true;

        static Plank plank;
        Ball ball;

        List<Brick> bricks;
        static List<Ball> balls;
        static Canvas BrickGrid;

        DispatcherTimer brickCollisionChecker;
        DispatcherTimer plankCollisionChecker;

        public BrickGame()
        {
            this.InitializeComponent();
            balls = new List<Ball>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region Grid init
            BrickGrid = new Canvas();
            BrickGrid.Name = "BrickGrid";
            mainGrid.Children.Add(BrickGrid);
            Grid.SetRow(BrickGrid, 1);

            // Create a green rectangle
            Rectangle greenRectangle = new Rectangle();
            greenRectangle.Fill = new SolidColorBrush(Colors.Green);

            greenRectangle.Width = ActualWidth;
            greenRectangle.Height = ActualHeight;

            // Set the ZIndex for the rectangle (optional)
            Canvas.SetZIndex(greenRectangle, 0);

            // Add the rectangle to the Canvas
            BrickGrid.Children.Add(greenRectangle);

            
            #endregion

            plank = new Plank(BrickGrid);
            ball = new Ball(BrickGrid, (int)ActualWidth/2, (int)ActualHeight-350, 7, 9);
            bricks = new List<Brick>();

            #region Timers init

            brickCollisionChecker = new DispatcherTimer();
            brickCollisionChecker.Interval = TimeSpan.FromTicks(50);
            brickCollisionChecker.Tick += CollisionChecker_Tick;

            plankCollisionChecker = new DispatcherTimer();
            plankCollisionChecker.Interval = TimeSpan.FromTicks(150);
            plankCollisionChecker.Tick += PlankCollisionChecker_Tick;
            #endregion

            #region Bricks init
            for (int i = 0; i < 48; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (j == 12 || j == 9 || j == 5 || j == 2)
                    {
                        continue;
                    }
                    Brick brick = new Brick("Assets/StoreLogo.png", BrickGrid, i * 40, j * 40);
                    bricks.Add(brick);
                }
            }
            #endregion

            BrickGrid.PointerMoved += BrickGrid_PointerMoved; ;

            plankCollisionChecker.Start();
            brickCollisionChecker.Start();
        }

        private void BrickGrid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var position = e.GetCurrentPoint(BrickGrid).Position;
                plank.UpdatePosition(position.X);
            }
        }

        private void PlankCollisionChecker_Tick(object sender, object e)
        {
            if (canCollideWithPlank)
            {
                Rect intersection = RectHelper.Intersect(ball.GetHitbox(), plank.GetHitbox());
                if (intersection.Width > 0 && intersection.Height > 0)
                {
                    if (intersection.Width > intersection.Height)
                    {
                        ball.TopCollision();
                    }
                    else
                    {
                        ball.SideCollision();
                    }
                    // Disable plank collision temporarily
                    canCollideWithPlank = false;

                    // Start a cooldown timer
                    StartPlankCollisionCooldown();
                }
            }
        }

        private async void StartPlankCollisionCooldown()
        {
            // Wait for the cooldown period
            await Task.Delay(TimeSpan.FromMilliseconds(500)); // Adjust the cooldown duration as needed

            // Re-enable plank collision
            canCollideWithPlank = true;
        }

        public static Rect GetPlankHitbox()
        {
            return plank.GetHitbox();
        }

        

        private void CollisionChecker_Tick(object sender, object e)
        {
            foreach (Brick brick in bricks)
            {
                Rect intersection = RectHelper.Intersect(ball.GetHitbox(), brick.GetHitbox());
                if (intersection.Width > 0 && intersection.Height > 0)
                {
                    if (intersection.Width > intersection.Height)
                    {
                        ball.TopCollision();
                    }
                    else
                    {
                        ball.SideCollision();
                    }

                    brick.RemoveBrick();
                    bricks.Remove(brick);

                    break;
                }
            }
        }

        public static void TripleBalls()
        {
            // Create two additional balls and add them to the canvas
            for (int i = 0; i < 2; i++)
            {
                Ball newBall = new Ball(BrickGrid, (int)plank.GetHitbox().X, (int)plank.GetHitbox().Y - 350, 7, -9); // Adjust the starting position as needed
                balls.Add(newBall);
            }
        }
    }
}
