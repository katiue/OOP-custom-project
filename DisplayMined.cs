using SplashKitSDK;

namespace OOP_custom_project
{
    public class DisplayMined
    {
        private readonly Window window;
        private readonly Bitmap background = new("upgarding background", @"D:\OOP-custom-project\Image\forging_background.jpg");
        private readonly GachaMineral gacha = new();
        private readonly List<Mineral> minerals = [];
        private bool showbag = false;
        private double total = 0;
        private float _offsetY;
        public DisplayMined(Window window, Mineral mineral)
        {
            this.window = window;
            Mineral = mineral;
        }
        private Mineral Mineral { get; set; }
        public void Drawing(MineralInventory _inventory)
        {
            // Handle input for panning
            Vector2D pos = SplashKit.MouseMovement();
            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                _offsetY += (float)pos.Y;
            }

            double scaleX = (double)window.Width / (double)background.Width;
            double scaleY = (double)window.Height / (double)background.Height;
            SplashKit.DrawBitmap(background, -652, -545, SplashKit.OptionScaleBmp(scaleX, scaleY));

            Mineral.Draw(400, -200, 0.3);

            Bitmap bitmap = new("Detail", 300, 200);
            bitmap.Clear(Color.Wheat);

            SplashKit.DrawTextOnBitmap(bitmap, "Detail", Color.DarkRed, "Arial", 12, 30, 10);
            SplashKit.DrawTextOnBitmap(bitmap, "Type: " + Mineral.Type.Name, Color.Black, "Arial", 12, 10, 30);
            SplashKit.DrawTextOnBitmap(bitmap, "Area: " + Mineral.Area, Color.Black, "Arial", 12, 10, 40);
            SplashKit.DrawTextOnBitmap(bitmap, "Stiffness: " + Mineral.Type.Stiffness, Color.Black, "Arial", 12, 10, 50);

            total = 0;
            foreach(var m in minerals)
            {
                total += m.Area;
            }
            
            SplashKit.DrawTextOnBitmap(bitmap, "Upgrade: " + total + "/10000", Color.Black, "Arial", 12, 10, 80);
            SplashKit.DrawBitmap(bitmap, 100, 150,SplashKit.OptionScaleBmp(1.5,2));

            //draw upgrade button
            Bitmap button = new("Upgrade", @"D:\OOP-custom-project\Image\Upgrade_button.png");
            SplashKit.DrawBitmap(button, 500,400, SplashKit.OptionScaleBmp(0.6,0.5));

            if(SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                //Upgrade mineral when 10000 value is met
                if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 640, 470, 320, 160) && !showbag && total >= 10000)
                {
                    for(int i = 0; i < total /10000; i++)
                    {
                        Point2D pt = new()
                        {
                            X = gacha.Pull(),
                            Y = gacha.Pull()
                        };
                        Mineral.Points.Add(pt);
                    }
                    foreach (var m in minerals)
                    {
                        _inventory.Take(m.ID[0]);
                    }
                    minerals.Clear();
                    GIFprocessor gif = new(@"D:\OOP-custom-project\Smashing_hammer\", 19, 0.03);
                    gif.ShowGifFrames(window);
                }
                //click to add the into mineral bar
                else if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 500, 0, 500, 700) && minerals.Count < 8 && showbag && total < 10000 && pos.X == 0 && pos.Y == 0)
                {
                    int x = (int)(SplashKit.MouseX() - 400) / 100;
                    int y = (int)(SplashKit.MouseY() - _offsetY) / 100;
                    if(y * 5 + x - 1 < _inventory.Mineral.Count)
                    {
                        foreach(var m in minerals)
                        {
                            if (m.ID[0] == _inventory.Mineral[y * 5 + x - 1].ID[0])
                            {
                                x = 9999;
                                y = 9999;
                                break;
                            }
                        }
                        if (y * 5 + x - 1 < _inventory.Mineral.Count)
                        {
                            minerals.Add(_inventory.Mineral[y * 5 + x - 1]);
                        }
                    }
                }
                //click on the add mineral bar
                else if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 40, 390, 400, 44))
                {
                    int checkitm = (int)(SplashKit.MouseX() - 40)/44;
                    if (checkitm < minerals.Count)
                    {
                        minerals.RemoveAt(checkitm);
                    }
                    showbag = true;
                }
                //close bag
                else if(!SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 500, 0, 500, 700))
                {
                    showbag = false;
                }
            }
            if (showbag)
            {
                SplashKit.FillRectangle(Color.BlanchedAlmond, 500, 0, 500, 700);
                for (int j = 0; j < _inventory.Mineral.Count; j++)
                {
                    if((j / 5) * 100 - 350 + _offsetY> -500 && (j / 5) * 100 - 350 + _offsetY < 300)
                    _inventory.Mineral[j].Draw((j % 5) * 100 + 150, (j / 5) * 100 - 350 + _offsetY, 0.1);
                }
            }
            DrawMineral();
            bitmap.Dispose();
        }
        private void DrawMineral()
        {
            for(int i = 0; i < minerals.Count; i++)
            {
                minerals[i].Draw(i*50 - 337, 10, 0.06);
            }
            for(int i = minerals.Count; i < 8; i++)
            {
                Bitmap bmp= new("Empty", @"D:\OOP-custom-project\Image\empty_box.png");
                SplashKit.DrawBitmap(bmp, i * 50 - 50, 300, SplashKit.OptionScaleBmp(0.2,0.2));
            }
        }
    }
}
