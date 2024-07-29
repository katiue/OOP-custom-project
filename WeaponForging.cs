using OfficeOpenXml;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class WeaponForging : IAmAScreen
    {
        private Mineral? Beingforged1;
        private Mineral? Beingforged2;
        private readonly Game _game;
        private string id;
        private readonly Define define = new();
        private readonly Bitmap Addbox = new("Add box", @"D:\OOP-custom-project\Image\empty_box.png");
        private float _offsetY;
        private int count;
        public WeaponForging(Game game)
        {
            _game = game;
            id = IDgenerator();
        }
        public void Draw()
        {
            SplashKit.FillRectangle(Color.BlanchedAlmond, 0, 0, 1000, 700);

            //draw two add boxes
            SplashKit.DrawBitmap(Addbox, 300, 50, SplashKit.OptionScaleBmp(0.5, 0.5));
            SplashKit.DrawBitmap(Addbox, 0, 50, SplashKit.OptionScaleBmp(0.5,0.5));
            
            //draw item on boxes
            Beingforged1?.Draw(-288, -239, 0.15);

            Beingforged2?.Draw(17, -239, 0.15);

            // Handle input for panning
            Vector2D pos = SplashKit.MouseMovement();
            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                _offsetY += (float)pos.Y;
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 500, 0, 500, 700) && pos.X == 0 && pos.Y == 0)
            {
                int x = (int)(SplashKit.MouseX() - 400) / 100;
                int y = (int)(SplashKit.MouseY() - _offsetY) / 100;
                if (y * 5 + x - 1 < _game.bag.MineralBag.Inventory.Mineral.Count)
                {
                    if (Beingforged1 == null)
                        Beingforged1 =_game.bag.MineralBag.Inventory.Mineral[y * 5 + x - 1];
                    else if(_game.bag.MineralBag.Inventory.Mineral[y * 5 + x - 1].ID[0] != Beingforged1.ID[0])
                        Beingforged2 = _game.bag.MineralBag.Inventory.Mineral[y * 5 + x - 1];
                }
            }

            if(SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 55, 105, 115, 115) && Beingforged1 != null)
            {
                Beingforged1 = null;
            }
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 355, 105, 115, 115) && Beingforged2 != null)
            {
                Beingforged2 = null;
            }

            Bitmap Forgebutton = new("Forge button", @"D:\OOP-custom-project\Image\Forge_button.png");
            SplashKit.DrawBitmap(Forgebutton, -290, 50, SplashKit.OptionScaleBmp(0.5, 0.3));

            if (Beingforged1 != null && Beingforged2 != null)
            {
                Weapon plan1 = ForgeWeapon(Beingforged1, Beingforged2);
                Weapon plan2 = ForgeWeapon(Beingforged2, Beingforged1);

                if (plan1 == null && plan2 == null)
                {
                    Bitmap bitmap = DrawObtained();
                    SplashKit.DrawBitmap(bitmap, 100, 300);
                }
                else
                {
                    if (plan1 != null)
                    {
                        plan1.Draw(250, 250);
                    }
                    else
                    {
                        plan2?.Draw(250, 250);
                    }
                }

                if(SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 0, 510, 500, 150))
                {
                    if (plan1 != null)
                    {
                        _game.bag.WeaponBag.Inventory.Put(ForgeWeapon(Beingforged1, Beingforged2));
                        _game.bag.MineralBag.Inventory.Take(Beingforged1.ID[0]);
                        _game.bag.MineralBag.Inventory.Take(Beingforged2.ID[0]);
                        Beingforged1 = null;
                        Beingforged2 = null;
                        GIFprocessor GifFile = new("D:\\OOP-custom-project\\Pouring-molten-metal", 127, 0.1);
                        GifFile.ShowGifFrames(_game.window);
                    }
                    else if (plan2 != null)
                    {
                        _game.bag.WeaponBag.Inventory.Put(ForgeWeapon(Beingforged2, Beingforged1));
                        _game.bag.MineralBag.Inventory.Take(Beingforged1.ID[0]);
                        _game.bag.MineralBag.Inventory.Take(Beingforged2.ID[0]);
                        Beingforged1 = null;
                        Beingforged2 = null;
                        GIFprocessor GifFile = new("D:\\OOP-custom-project\\Pouring-molten-metal", 127, 0.1);
                        GifFile.ShowGifFrames(_game.window);
                    }
                }
            }

            if (SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                _game.ChangeScreen("starting");
                return;
            }

            count = -1;
            for (int j = 0; j < _game.bag.MineralBag.Inventory.Mineral.Count; j++)
            {
                if(_game.bag.MineralBag.Inventory.Mineral[j].Area >= 20000)
                {
                    count++;
                    if ((count / 5) * 100 - 450 + _offsetY > -500 && (count / 5) * 100 - 450 + _offsetY < 200)
                    {
                        _game.bag.MineralBag.Inventory.Mineral[count].Draw((count % 5) * 100 + 150, (count / 5) * 100 - 350 + _offsetY, 0.1);
                        SplashKit.DrawText(_game.bag.MineralBag.Inventory.Mineral[count].Type.Name, Color.Black, "Arial", 12, (count % 5) * 100 + 510, (count / 5) * 100 + 90 + _offsetY);
                    }
                }
            }
        }
        private Weapon ForgeWeapon(Mineral mineral1, Mineral mineral2)
        {
            if (define.weaponMappings.TryGetValue((mineral1.Type.Name, mineral2.Type.Name), out var weaponMapping))
            {
                int newid= int.Parse(id) + 1;
                id = newid.ToString();
                return new Weapon([id], weaponMapping.WeaponName, "", mineral1.Type.Stiffness + mineral2.Type.Stiffness, 1, mineral1.Type.Name, mineral2.Type.Name);
            }
            return null;
        }
        private static string IDgenerator()
        {
            FileInfo fileInfo = new("D:\\OOP-custom-project\\Weapon.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Weapon"] ?? throw new Exception("Worksheet 'Weapon' not found in the Excel file.");

            //use the newest id to add new mineral
            int rows = worksheet.Dimension.Rows;
            string? idCellValue = worksheet.Cells[rows, 1].Value?.ToString();
            if (rows > 1)
                idCellValue = (int.Parse(idCellValue)).ToString();
            else
                idCellValue = "1";
            return idCellValue;
        }
        private static Bitmap DrawObtained()
        {
            Bitmap obtained = new("obtained", @"D:\OOP-custom-project\Image\ObtainFrame.png");
            Bitmap baseimg = new("bitmap", obtained.Width, obtained.Height + 20);
            baseimg.Clear(Color.RGBAColor(255, 165, 0, 64));
            SplashKit.DrawBitmapOnBitmap(baseimg, obtained, 0, 0);
            SplashKit.DrawTextOnBitmap(baseimg, "Craft failed", Color.Black, "Arial", 0, 80, 60);
            SplashKit.DrawTextOnBitmap(baseimg, "Try another combination", Color.Black, "Arial", 0, 50, 70);
            return baseimg;
        }
    }
}
