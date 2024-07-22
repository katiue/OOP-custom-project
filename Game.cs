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

        private string current_screen_type = "map";
        public Bag bag;
        public Game() 
        {
            bag = new Bag(this);
        }
        public void Run()
        {
            Window window = new Window("Game screen", 1000, 700);
            GIFprocessor GifFile;
            InteractiveMap map = new InteractiveMap(window, this);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                switch (current_screen_type)
                {
                    case "starting":
                        break;
                    case "molding":
                        GifFile = new GIFprocessor("D:\\OOP-custom-project\\Pouring-molten-metal", 127, 0.1);
                        GifFile.ShowGifFrames(window);
                        break;
                    case "smashing":
                        GifFile = new GIFprocessor("D:\\OOP-custom-project\\Smashing_hammer", 20, 0.03);
                        GifFile.ShowGifFrames(window);
                        break;
                    case "map":
                        map.Draw();
                        break;
                    case "bag":
                        bag.Draw();
                        break;
                }
                SplashKit.RefreshScreen(120);
            } while (!window.CloseRequested);
            bag.SaveFile();
        }
        public void ChangeScreen(string new_screen_type)
        {
            current_screen_type = new_screen_type;
        }
    }
}
