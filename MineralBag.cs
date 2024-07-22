using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class MineralBag : Item, IHaveInventory
    {
        private Mineral? showing;
        private MineralInventory _inventory;
        private DisplayMined? _display;
        public MineralBag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new MineralInventory();
        }
        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            else
            {
                return _inventory.Fetch(id);
            }
        }
        public override string FullDescription
        {
            get
            {
                return "In the " + Name + " you can see:\n " + _inventory.Mineral;
            }
        }
        public MineralInventory Inventory
        {
            get
            {
                return _inventory;
            }
        }
        public void Draw()
        {
            Window window = SplashKit.CurrentWindow();
            if (SplashKit.KeyTyped(KeyCode.EscapeKey) || window.CloseRequested)
            {
                if(showing != null)
                    _inventory.Put(showing);
                showing = null;
            }
            if (showing != null)
                DisplayMineral(showing);
            for (int i = 0; i < _inventory.Mineral.Count; i++)
            {
                if (SplashKit.MouseClicked(MouseButton.LeftButton)&& SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 100 * (i % 8) + 164, 100 * (i / 8) + 64, 70, 70))
                {
                    showing = _inventory.Take(_inventory.Mineral[i].ID[0]);
                }
                if(showing == null)
                {
                    _display = null;
                    if (_inventory.Mineral[i] != null)
                        _inventory.Mineral[i].Draw((i % 8) * 100 - 300, (i / 8) * 100 - 400, 0.07);
                }
                SplashKit.DrawText(_inventory.Mineral[i].Type._name, Color.Black, "Arial", 12, (i % 8) * 100 + 165, (i / 8) * 100 + 140);
            }
        }
        private void DisplayMineral(Mineral mineral)
        {
            if (_display == null)
            {
                _display = new DisplayMined(SplashKit.CurrentWindow(), mineral);
            }
            _display.Drawing(_inventory);
        }
    }
}
