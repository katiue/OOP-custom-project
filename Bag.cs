using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class Bag
    {
        private MineralBag _mineralbag;
        private Database _database;
        private Game _game;
        public Bag(Game game)
        {
            _database = new Database();
            _mineralbag = new MineralBag(new string[] { "Mineral bag" }, "Mineral bag", "Bag to store mineral");
            AddMineral();
            _game = game;
        }
        private void AddMineral()
        {
            _mineralbag.Inventory.Put(_database.ImportMineralsFromExcel(@"D:\OOP-custom-project\Mineral.xlsx"));
        }
        public MineralBag MineralBag
        {
            get
            {
                return _mineralbag;
            }
        }
        public void Draw()
        {
            SplashKit.FillRectangle(Color.RGBAColor(128, 128, 128, 64), 0, 0, 1000, 700);
            _mineralbag.Draw();
            if(SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                _game.ChangeScreen("map");
            }
        }
        public void SaveFile()
        {
            _database.ExportMineralsToExcel(_mineralbag.Inventory.Mineral, @"D:\OOP-custom-project\Mineral.xlsx");
        }
    }
}
