using SplashKitSDK;
using OfficeOpenXml;


namespace OOP_custom_project
{
    public class Quest : IAmAScreen
    {
        private const string MineralFilePath = "D:\\OOP-custom-project\\Mineral.xlsx";
        private const string WeaponFilePath = "D:\\OOP-custom-project\\Weapon.xlsx";
        private readonly Define define = new();
        private readonly List<Mission> _missions;
        private readonly Game _game;
        private Mission? display;
        public Quest(Game game) 
        {
            _missions =
            [
                new(["mission1"], "Collect mineral from map", "Open map to collect 3 types of \nmineral", "In Progress", 
                [ 
                    new Mineral([IDgenerator("mineral")], "", "", define.minerals[7], []),
                    new Mineral([IDgenerator("mineral")], "", "", define.minerals[8], [])
                ], game => game.bag.MineralBag.Inventory.Mineral.Count >= 2),
                new(["mission2"], "Upgrade minerals", "Upgrade two minerals so they have \nthe area of 15000 each", "In Progress", 
                [ 
                    new Mineral([IDgenerator("mineral")], "", "", define.minerals[8], []) 
                ], game => game.bag.MineralBag.Inventory.Mineral.Count(mineral => mineral.Area > 15000) >= 2),
                new(["mission3"], "Forge Weapon", "Forge two minerals into a sword", "In Progress", [ 
                    ForgeWeapon(
                    new Mineral([IDgenerator("mineral")], "", "", define.minerals[7], []),
                    new Mineral([IDgenerator("mineral")], "", "", define.minerals[8], [])) 
                ], game => game.bag.WeaponBag.Inventory.WeaponList.Count >= 1)
            ];
            _game = game;
        }
        public void Draw()
        {
            CheckMission();
            SplashKit.DrawBitmap(new Bitmap("background", @"D:\OOP-custom-project\Image\Main-theme-background.png"), 0, 0);
            for (int i = 0; i < _missions.Count; i++)
            {
                Bitmap mission = new("mission", 400, 35);
                mission.Clear(Color.RGBAColor(128, 128, 128, 128));

                SplashKit.DrawTextOnBitmap(mission, _missions[i].Name, Color.Black, "Arial", 12, 10, 10);
                SplashKit.DrawTextOnBitmap(mission, _missions[i].Status, Color.Black, "Arial", 12, 20, 25);

                SplashKit.DrawBitmap(mission, 100, 10 + i * 75, SplashKit.OptionScaleBmp(1.5,2));
                
                mission.Dispose();
            }
            //display mission detail
            if (display != null)
            {
                Bitmap detail = new("mission detail", 300, 350);
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
                        mineral?.Draw(270 + 70 * i, 100, 0.1);
                    }
                    else if (display.Reward[i] is Weapon)
                    {
                        Weapon weapon = display.Reward[i] as Weapon;
                        weapon?.Draw(680 + 70 * i, 500);
                    }
                }

                detail.Dispose();

                Bitmap claim_button = new("claim", 78, 20);
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
                if (Mission.CheckCondition(_game, mission.Condition) && mission.Status != "Claimed")
                {
                    mission.Status = "Completed";
                }
            }
        }
        private static string IDgenerator(string type)
        {
            string FilePath = "";
            string WorkSheet = "";

            switch(type)
            {
                case "mineral":
                    FilePath = MineralFilePath;
                    WorkSheet = "Minerals";
                    break;
                case "weapon":
                    FilePath = WeaponFilePath;
                    WorkSheet = "Weapon";
                    break;
            }

            FileInfo fileInfo = new(FilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[WorkSheet] ?? throw new Exception($"Worksheet {WorkSheet} not found in the Excel file.");

            //use the newest id to add new mineral
            int rows = worksheet.Dimension.Rows;
            string? idCellValue = worksheet.Cells[rows, 1].Value?.ToString();
            if (rows > 1)
                idCellValue = (int.Parse(idCellValue)).ToString();
            else
                idCellValue = "1";
            return idCellValue;
        }
        private Weapon ForgeWeapon(Mineral mineral1, Mineral mineral2)
        {
            if (define.weaponMappings.TryGetValue((mineral1.Type.Name, mineral2.Type.Name), out var weaponMapping))
            {
                return new Weapon([IDgenerator("weapon")], weaponMapping.WeaponName, "", mineral1.Type.Stiffness + mineral2.Type.Stiffness, 1, mineral1.Type.Name, mineral2.Type.Name);
            }
            return null;
        }
    }
}
