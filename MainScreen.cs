using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class MainScreen : IAmAScreen
    {
        private Game _game;
        public MainScreen(Game game) 
        {
            _game = game;
        }
        public void Draw()
        {
            DrawSelectionBar();
        }
        private void DrawSelectionBar() 
        {
            Bitmap selecticon = new Bitmap("map", @"D:\OOP-custom-project\Image\Game_map_icon.png");
            SplashKit.DrawBitmap(selecticon, 0, -45, SplashKit.OptionScaleBmp(0.5,0.5));

            selecticon = new Bitmap("bag", @"D:\OOP-custom-project\Image\bag_icon.png");
            SplashKit.DrawBitmap(selecticon, 100, -63, SplashKit.OptionScaleBmp(0.5, 0.5));

            selecticon = new Bitmap("forging", @"D:\OOP-custom-project\Image\Forging_icon.webp");
            SplashKit.DrawBitmap(selecticon, 70, -200, SplashKit.OptionScaleBmp(0.2, 0.2));

            if(SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                if(SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 50, 10, 100, 100))
                {
                    _game.ChangeScreen("map");
                }
                else if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 150, 10, 100, 100))
                {
                    _game.ChangeScreen("bag");
                }
                else if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 270, 10, 100, 100))
                {
                    _game.ChangeScreen("forging");
                }
            }
        }
    }
}
