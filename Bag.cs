using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class Bag : IAmAScreen
    {
        private MineralBag _mineralbag;
        private WeaponBag _componentbag;
        private Database _database;
        private Game _game;
        private string _displaying = "mineral";
        public Bag(Game game)
        {
            _database = new Database();
            _mineralbag = new MineralBag(new string[] { "Mineral bag" }, "Mineral bag", "Bag to store mineral");
            _componentbag = new WeaponBag(new string[] { "Component bag" }, "Component bag", "Bag to store component");
            AddMineral();
            _game = game;
        }
        private void AddMineral()
        {
            _mineralbag.Inventory.Put(_database.ImportMineralsFromExcel(@"D:\OOP-custom-project\Mineral.xlsx"));
            _componentbag.Inventory.Put(_database.ImportComponentsFromExcel(@"D:\OOP-custom-project\Weapon.xlsx"));
        }
        public MineralBag MineralBag
        {
            get
            {
                return _mineralbag;
            }
        }
        public WeaponBag ComponentBag
        {
            get
            {
                return _componentbag;
            }
        }
        public void Draw()
        {
            SplashKit.FillRectangle(Color.RGBAColor(128, 128, 128, 64), 0, 0, 1000, 700);

            //draw selection bar
            Bitmap mineral_icon = new Bitmap("mineral", @"D:\OOP-custom-project\Image\mineral_icon.png");
            SplashKit.DrawBitmap(mineral_icon, -57, -36, SplashKit.OptionScaleBmp(0.22, 0.22));
            Bitmap weapon_icon = new Bitmap("weapon", @"D:\OOP-custom-project\Image\sword_icon.png");
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
                    _mineralbag.Draw();
                    break;
                case "weapon":
                    _componentbag.Draw();
                    break;
            }
            if(SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                _game.ChangeScreen("starting");
            }
        }
        public void SaveFile()
        {
            _database.ExportMineralsToExcel(_mineralbag.Inventory.Mineral, @"D:\OOP-custom-project\Mineral.xlsx");
            _database.ExportComponentsToExcel(_componentbag.Inventory.ComponentList, @"D:\OOP-custom-project\Weapon.xlsx");
        }
    }
}
