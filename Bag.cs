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
            SplashKit.FillRectangle(Color.RGBAColor(128, 128, 128, 64), 0, 0, 1000, 700);

            //draw selection bar
            Bitmap mineral_icon = new("mineral", @"D:\OOP-custom-project\Image\mineral_icon.png");
            SplashKit.DrawBitmap(mineral_icon, -57, -36, SplashKit.OptionScaleBmp(0.22, 0.22));
            Bitmap weapon_icon = new("weapon", @"D:\OOP-custom-project\Image\sword_icon.png");
            SplashKit.DrawBitmap(weapon_icon, -57, 33, SplashKit.OptionScaleBmp(0.22, 0.22));

            if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 30, 50, 50, 50))
            {
                _displaying = "mineral";
            }
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 30, 120, 50, 50))
            {
                _displaying = "weapon";
            }
            switch(_displaying)
            {
                case "mineral":
                    MineralBag.Draw();
                    break;
                case "weapon":
                    WeaponBag.Draw();
                    break;
            }
            if(SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                _game.ChangeScreen("starting");
            }
        }
        public void SaveFile()
        {
            Database.ExportMineralsToExcel(MineralBag.Inventory.Mineral, @"D:\OOP-custom-project\Mineral.xlsx");
            Database.ExportComponentsToExcel(WeaponBag.Inventory.WeaponList, @"D:\OOP-custom-project\Weapon.xlsx");
        }
    }
}
