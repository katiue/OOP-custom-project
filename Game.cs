using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class Game
    {

        public string current_screen_type = "see map";
        public Game() { }
        public void Run()
        {
            Window window = new Window("Game screen", 1000, 700);
            GIFprocessor GifFile;
            CustomShape shape = new CustomShape(window);
            InteractiveMap map = new InteractiveMap(window, this);

            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                switch (current_screen_type)
                {
                    case "starting":
                        shape.Draw(Color.Blue);
                        break;
                    case "molding":
                        GifFile = new GIFprocessor("D:\\OOP-custom-project\\Pouring-molten-metal", 127, 0.1);
                        GifFile.ShowGifFrames(window);
                        break;
                    case "smashing":
                        GifFile = new GIFprocessor("D:\\OOP-custom-project\\Smashing_hammer", 20, 0.03);
                        GifFile.ShowGifFrames(window);
                        break;
                    case "drawing":
                        shape.Drawing();
                        break;
                    case "see map":
                        map.Draw();
                        if(map.ChangeScreen != "")
                        {
                            ChangeScreen(map.ChangeScreen);
                        }
                        break;
                }

                SplashKit.RefreshScreen(60);

            } while (!window.CloseRequested);
        }
        public void ChangeScreen(string new_screen_type)
        {
            current_screen_type = new_screen_type;
        }
    }
}
