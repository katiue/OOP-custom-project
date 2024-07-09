using System;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class Program
    {
        public static void Main()
        {
            string current_screen_type = "drawing";
            Window window = new Window("Game screen", 1000, 700);
            GIFprocessor GifFile;
            CustomShape shape = new CustomShape(window);

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
                }

                SplashKit.RefreshScreen(60);

            } while (!window.CloseRequested);

            window.Close();
        }
    }
}
