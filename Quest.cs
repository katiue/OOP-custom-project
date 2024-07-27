using SplashKitSDK;
using OfficeOpenXml;


namespace OOP_custom_project
{
    public class Quest : IAmAScreen
    {
        private Define define = new Define();
        private List<Mission> _missions;
        private Game _game;
        private Mission? display;
        public Quest(Game game) 
        {
            _missions = new List<Mission>
            {
                new Mission(new string[] { "mission1" }, "Collect mineral from map", "Open map to collect 3 types of \nmineral", "In Progress", 
                new List<GameObject> { 
                    new Mineral(new string[] { IDgenerator() }, "", "", define.minerals[7], new List<Point2D>()),
                    new Mineral(new string[] { IDgenerator() }, "", "", define.minerals[8], new List<Point2D>())
                }, game => game.bag.MineralBag.Inventory.Mineral.Count >= 2),
                new Mission(new string[] { IDgenerator() }, "Upgrade minerals", "Upgrade two minerals so they have \nthe area of 15000 each", "In Progress", 
                new List<GameObject> { 
                    new Mineral(new string[] { IDgenerator() }, "", "", define.minerals[8], new List<Point2D>()) 
                }, game => game.bag.MineralBag.Inventory.Mineral.Count(mineral => mineral.Area > 15000) >= 2),
                new Mission(new string[] { IDgenerator() }, "Forge Weapon", "Forge two minerals into a sword", "In Progress", new List < GameObject > { 
                    ForgeWeapon(
                    new Mineral(new string[] { IDgenerator() }, "", "", define.minerals[7], new List<Point2D>()),
                    new Mineral(new string[] { IDgenerator() }, "", "", define.minerals[8], new List<Point2D>())) 
                }, game => game.bag.WeaponBag.Inventory.WeaponList.Count >= 1)
            };
            _game = game;
        }
        public void Draw()
        {
            CheckMission();
            SplashKit.DrawBitmap(new Bitmap("background", @"D:\OOP-custom-project\Image\Main-theme-background.png"), 0, 0);
            for (int i = 0; i < _missions.Count; i++)
            {
                Bitmap mission = new Bitmap("mission", 400, 35);
                mission.Clear(Color.RGBAColor(128, 128, 128, 128));

                SplashKit.DrawTextOnBitmap(mission, _missions[i].Name, Color.Black, "Arial", 12, 10, 10);
                SplashKit.DrawTextOnBitmap(mission, _missions[i].Status, Color.Black, "Arial", 12, 20, 25);

                SplashKit.DrawBitmap(mission, 100, 10 + i * 75, SplashKit.OptionScaleBmp(1.5,2));
                
                mission.Dispose();
            }
            //display mission detail
            if (display != null)
            {
                Bitmap detail = new Bitmap("mission detail", 300, 350);
                detail.Clear(Color.RGBAColor(128, 128, 128, 128));

                SplashKit.DrawTextOnBitmap(detail, display.Name, Color.OrangeRed, "Arial", 12, 10, 10);

                string[] paragraph = display.FullDescription.Split('\n');
                foreach (string line in paragraph)
                {
                    SplashKit.DrawTextOnBitmap(detail, line, Color.Black, "Arial", 12, 10, 10 + 15 * (Array.IndexOf(paragraph, line) + 1));
                }

                SplashKit.DrawBitmap(detail, 650, 110, SplashKit.OptionScaleBmp(1.3,1.6));

                for (int i = 0; i < display.Reward.Count; i++)
                {
                    if (display.Reward[i] is Mineral)
                    {
                        Mineral mineral = display.Reward[i] as Mineral;
                        if (mineral != null)
                            mineral.Draw(270 + 70 * i, 100, 0.1);
                    }
                    else if (display.Reward[i] is Weapon)
                    {
                        Weapon weapon = display.Reward[i] as Weapon;
                        if (weapon != null)
                            weapon.Draw(680 + 70 * i, 500);
                    }
                }

                detail.Dispose();

                Bitmap claim_button = new Bitmap("claim", 78, 20);
                claim_button.Clear(Color.RGBAColor(128, 128, 128, 128));
                if(display.Status != "Claimed")
                    SplashKit.DrawTextOnBitmap(claim_button, "Claim", Color.Black, "Arial", 12, 20, 5);
                else
                    SplashKit.DrawTextOnBitmap(claim_button, "Claimed", Color.Black, "Arial", 12, 10, 5);
                SplashKit.DrawBitmap(claim_button, 760, 620, SplashKit.OptionScaleBmp(5, 5));

                if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 605, 580, 390, 100) && display.Status == "Completed")
                {
                    display.Status = "Claimed";
                    foreach (var obj in display.Reward)
                    {
                        if (obj is Mineral)
                        {
                            _game.bag.MineralBag.Inventory.Put(obj as Mineral);
                        }
                        else if (obj is Weapon)
                        {
                            _game.bag.WeaponBag.Inventory.Put(obj as Weapon);
                        }
                    }
                }

                claim_button.Dispose();
            }
            
            if(SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                for (int i = 0; i < _missions.Count; i++)
                {
                    if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 100, 10 + i * 75, 400, 35))
                    {
                        display = _missions[i];
                    }
                }
            }

            if(SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                _game.ChangeScreen("starting");
            }

        }
        private void CheckMission()
        {
            foreach (Mission mission in _missions)
            {
                if (mission.CheckCondition(_game, mission.Condition) && mission.Status != "Claimed")
                {
                    mission.Status = "Completed";
                }
            }
        }
        private string IDgenerator()
        {
            FileInfo fileInfo = new FileInfo("D:\\OOP-custom-project\\Mineral.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Minerals"];

                if (worksheet == null)
                    throw new Exception("Worksheet 'Minerals' not found in the Excel file.");

                //use the newest id to add new mineral
                int rows = worksheet.Dimension.Rows;
                string? idCellValue = worksheet.Cells[rows, 1].Value?.ToString();
                if (rows > 1)
                    idCellValue = (int.Parse(idCellValue)).ToString();
                else
                    idCellValue = "1";
                return idCellValue;
            }
        }
        private Weapon ForgeWeapon(Mineral mineral1, Mineral mineral2)
        {
            if (define.weaponMappings.TryGetValue((mineral1.Type._name, mineral2.Type._name), out var weaponMapping))
            {
                return new Weapon(new string[] { IDgenerator() }, weaponMapping.WeaponName, "", mineral1.Type._stiffness + mineral2.Type._stiffness, 1, mineral1.Type._name, mineral2.Type._name);
            }
            return null;
        }
    }
}
