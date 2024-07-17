using SplashKitSDK;
using System;

namespace OOP_custom_project
{
    public class Testing
    {
        private Bitmap _mapImage;
        private float _zoom = 1.0f;
        private Bitmap Layer;
        private float _offsetY, _offsetX = 0;
        public Testing()
        {
            _mapImage = new Bitmap("Map", @"D:\OOP-custom-project\map.webp");
            Layer = new Bitmap("Layer", (int)(_mapImage.Width*_zoom), (int)(_mapImage.Height * _zoom));
        }

        public void MakeMapObjectLayer()
        {
            Window window = new Window("Game screen", 1090, 700);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                Layer = new Bitmap("Layer", (int)(_mapImage.Width * _zoom), (int)(_mapImage.Height * _zoom));
                // Create a new bitmap with a transparent background
                Bitmap baseImage = new Bitmap("BaseImage", _mapImage.Width, _mapImage.Height);
                baseImage.Clear(Color.RGBAColor(0, 0, 0, 0));
                // Load the original image

                Bitmap originalImg = SplashKit.LoadBitmap("OverlayImage", "D:\\OOP-custom-project\\map.webp");
                /*int a = originalImg.Width;
                int b = originalImg.Height;

                // Create a new bitmap with scaled dimensions
                Bitmap scaledImg = new Bitmap("Scaled Image", (int)_zoom * a, (int)_zoom * b);


                // Draw the scaled image onto the new bitmap
                scaledImg.Clear(Color.Black); // Clear the surface with transparent color
                originalImg.DrawBitmap(scaledImg, 0, 0, SplashKit.OptionScaleBmp(_zoom, _zoom));*/

                SplashKit.DrawBitmap(originalImg, _offsetX, _offsetY);

                // Handle input for zooming
                Vector2D scroll = SplashKit.MouseWheelScroll();
                // Calculate the new zoom level0.0f < 3f))
                _zoom += (float)scroll.Y / 10.0f;

                // Handle input for panning
                Vector2D pos = SplashKit.MouseMovement();
                if (SplashKit.MouseDown(MouseButton.LeftButton))
                {
                    _offsetX += (float)pos.X;
                    _offsetY += (float)pos.Y;
                }
                Console.WriteLine(_offsetX + " " + _offsetY);
                // Free resources
                //originalImg.Free();
                //scaledImg.Free();

                SplashKit.RefreshScreen(120);
            } while (!window.CloseRequested);
        }
    }
}
