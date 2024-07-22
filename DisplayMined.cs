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
        private List<Mineral> minerals = new List<Mineral>();
        private bool showbag = false;
        private double total = 0;
        public DisplayMined(Window window, Mineral mineral)
        {
            this.window = window;
            this.mineral = mineral;
        }
        public void Drawing(MineralInventory _inventory)
        {

            if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 640, 470, 320, 160) && !showbag && total > 30000)
            {
                Point2D pt = new Point2D();
                pt.X = gacha.Pull();
                pt.Y = gacha.Pull();
                mineral.showedPoints.Add(pt);
                foreach(var m in minerals)
                {
                    _inventory.Take(m.ID[0]);
                }
                minerals.Clear();
            }

            double scaleX = (double)window.Width / (double)background.Width;
            double scaleY = (double)window.Height / (double)background.Height;
            SplashKit.DrawBitmap(background, -652, -545, SplashKit.OptionScaleBmp(scaleX, scaleY));

            mineral.Draw(300, -300, 0.3);
            Bitmap bitmap = new Bitmap("Detail", 300, 200);
            bitmap.Clear(Color.Wheat);

            SplashKit.DrawTextOnBitmap(bitmap, "Detail", Color.DarkRed, "Arial", 12, 30, 10);
            SplashKit.DrawTextOnBitmap(bitmap, "Type: " + mineral.Type._name, Color.Black, "Arial", 12, 10, 30);
            SplashKit.DrawTextOnBitmap(bitmap, "Area: " + mineral.Area, Color.Black, "Arial", 12, 10, 40);
            SplashKit.DrawTextOnBitmap(bitmap, "Stiffness: " + mineral.Type._stiffness, Color.Black, "Arial", 12, 10, 50);
            total = 0;
            foreach(var m in minerals)
            {
                total += m.Area;
            }
            SplashKit.DrawTextOnBitmap(bitmap, "Upgrade: " + total + "/30000", Color.Black, "Arial", 12, 10, 80);
            SplashKit.DrawBitmap(bitmap, 100, 150,SplashKit.OptionScaleBmp(1.5,2));

            //draw upgrade button
            Bitmap button = new Bitmap("Upgrade", @"D:\OOP-custom-project\Upgrade_button.png");
            SplashKit.DrawBitmap(button, 500,400, SplashKit.OptionScaleBmp(0.6,0.5));

            if(SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 500, 0, 500, 700) && minerals.Count < 8 && showbag && total < 30000)
                {
                    int x = (int)(SplashKit.MouseX() - 400) / 100;
                    int y = (int)SplashKit.MouseY() / 100;
                    if(y * 5 + x - 1 < _inventory.Mineral.Count)
                    {
                        foreach(var m in minerals)
                        {
                            if (m.ID[0] == _inventory.Mineral[y * 5 + x - 1].ID[0])
                            {
                                x = 99;
                                y = 99;
                                break;
                            }
                        }
                        if (y * 5 + x - 1 < _inventory.Mineral.Count)
                            minerals.Add(_inventory.Mineral[y * 5 + x - 1]);
                    }
                }
                else if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 40, 390, 400, 44))
                {
                    int checkitm = (int)(SplashKit.MouseX() - 40)/44;
                    if (checkitm < minerals.Count)
                    {
                        minerals.RemoveAt(checkitm);
                    }
                    showbag = true;
                }
                else
                {
                    showbag = false;
                }
            }
            if (showbag)
            {
                SplashKit.FillRectangle(Color.BlanchedAlmond, 500, 0, 500, 700);
                for (int j = 0; j < _inventory.Mineral.Count; j++)
                {
                    _inventory.Mineral[j].Draw((j % 5) * 100 + 50, (j / 5) * 100 - 450, 0.07);
                }
            }
            DrawMineral();
            bitmap.Dispose();
        }
        private void DrawMineral()
        {
            for(int i = 0; i < minerals.Count; i++)
            {
                minerals[i].Draw(i*50 - 437, -89, 0.045);
            }
            for(int i = minerals.Count; i < 8; i++)
            {
                Bitmap bmp= new Bitmap("Empty", @"D:\OOP-custom-project\empty_box.png");
                SplashKit.DrawBitmap(bmp, i * 50 - 50, 300, SplashKit.OptionScaleBmp(0.2,0.2));
            }
        }
    }
}
