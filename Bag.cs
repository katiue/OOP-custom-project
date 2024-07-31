using SplashKitSDK;

namespace OOP_custom_project
{
    public class Bag : IAmAScreen
    {
        private readonly Game _game;
        private string _displaying = "mineral";
        public Bag(Game game)
        {
            MineralBag = new MineralBag(["Mineral bag"], "Mineral bag", "Bag to store mineral");
            WeaponBag = new WeaponBag(["Component bag"], "Component bag", "Bag to store component");
            Updatedatabase();
            _game = game;
        }
        private void Updatedatabase()
        {
            MineralBag.Inventory.Put(Database.ImportMineralsFromExcel(@"D:\OOP-custom-project\Mineral.xlsx"));
            WeaponBag.Inventory.Put(Database.ImportComponentsFromExcel(@"D:\OOP-custom-project\Weapon.xlsx"));
        }
        public MineralBag MineralBag { get; }
        public WeaponBag WeaponBag { get; }
        public void Draw()
        {
            DrawBackground();
            DrawIcons();
            HandleInput();
            DrawCurrentBag();
        }
        private void DrawBackground()
        {
            SplashKit.FillRectangle(Color.RGBAColor(128, 128, 128, 64), 0, 0, 1000, 700);
        }
        private void DrawIcons()
        {
            DrawIcon("mineral", @"D:\OOP-custom-project\Image\mineral_icon.png", -57, -36);
            DrawIcon("weapon", @"D:\OOP-custom-project\Image\sword_icon.png", -57, 33);
        }
        private void DrawIcon(string name, string filePath, int x, int y)
        {
            Bitmap icon = new(name, filePath);
            SplashKit.DrawBitmap(icon, x, y, SplashKit.OptionScaleBmp(0.22, 0.22));
        }
        private void HandleInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 30, 50, 50, 50))
                {
                    _displaying = "mineral";
                }
                else if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 30, 120, 50, 50))
                {
                    _displaying = "weapon";
                }
            }
            if (SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                _game.ChangeScreen("starting");
            }
        }
        private void DrawCurrentBag()
        {
            switch (_displaying)
            {
                case "mineral":
                    MineralBag.Draw();
                    break;
                case "weapon":
                    WeaponBag.Draw();
                    break;
            }
        }

        public void SaveFile()
        {
            Database.ExportMineralsToExcel(MineralBag.Inventory.Mineral, @"D:\OOP-custom-project\Mineral.xlsx");
            Database.ExportComponentsToExcel(WeaponBag.Inventory.WeaponList, @"D:\OOP-custom-project\Weapon.xlsx");
        }
    }
}
