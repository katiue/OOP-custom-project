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

        private string current_screen_type = "bag";
        public Bag bag;
        public Window window = new Window("Game screen", 1000, 700);
        public WeaponForging forging;
        public Game() 
        {
            bag = new Bag(this);
            forging = new WeaponForging(this);
        }
        public void Run()
        {
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
                    case "map":
                        map.Draw();
                        break;
                    case "bag":
                        bag.Draw();
                        break;
                    case "forging":
                        forging.Draw();
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
