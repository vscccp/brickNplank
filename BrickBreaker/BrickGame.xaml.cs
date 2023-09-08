using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BrickBreaker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrickGame : Page
    {
        Plank plank;
        Ball ball;


        public BrickGame()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.CharacterReceived += CoreWindow_CharacterReceived;
        }

        private void CoreWindow_CharacterReceived(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.CharacterReceivedEventArgs args)
        {
            switch (args.KeyCode)
            {
                case 97:
                    plank.Move(Direction.Left);
                    break;
                case 100:
                    plank.Move(Direction.Right);
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            plank = new Plank(BrickGrid);
            ball = new Ball(BrickGrid);

            for(int i = 0; i < 48; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (j == 12 || j == 9 || j == 5 || j == 2)
                    {
                        continue;
                    }
                    Brick brick = new Brick("Assets/StoreLogo.png", BrickGrid, i * 40, j * 40);
                }
            }
        }
    }
}
