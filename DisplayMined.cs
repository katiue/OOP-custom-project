using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class DisplayMined
    {
        private Window window;
        private Bitmap background = new Bitmap("background", @"D:\OOP-custom-project\forging_background.jpg");
        private Mineral mineral { get; set; }
        private GachaMineral gacha = new GachaMineral();
        public DisplayMined(Window window, Mineral mineral)
        {
            this.window = window;
            this.mineral = mineral;
        }
        private void Draw()
        {
            double scaleX = (double)window.Width / (double)background.Width;
            double scaleY = (double)window.Height / (double)background.Height;
            SplashKit.DrawBitmap(background, -652, -545, SplashKit.OptionScaleBmp(scaleX, scaleY));

            mineral.Draw(300,-300,0.3);
            SplashKit.DrawText("Area: " + mineral.Area, Color.White, "Arial", 12, 50, 250);
        }
        public void Drawing()
        {
            /*if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Point2D pt = new Point2D();
                pt.X = gacha.Pull();
                pt.Y = gacha.Pull();
                mineral.showedPoints.Add(pt);
            }*/
            Draw();
        }
    }
}
