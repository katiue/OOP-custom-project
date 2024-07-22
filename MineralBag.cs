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
        private MineralInventory _inventory;
        private DisplayMined? _display;
        private bool _isDrawing;
        private int showing;
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
            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                _isDrawing = false;
            }
            if (_isDrawing)
                DisplayMineral(_inventory.Mineral[showing]);
            for (int i = 0; i < _inventory.Mineral.Count; i++)
            {
                if (SplashKit.MouseClicked(MouseButton.LeftButton)&& SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 100 * (i % 8) + 164, 100 * (i / 8) + 64, 70, 70))
                {
                    _isDrawing = true;
                    showing = i;
                }
                if(!_isDrawing)
                {
                    _display = null;
                    _inventory.Mineral[i].Draw((i % 8) * 100 - 300, (i / 8) * 100 - 400, 0.07);
                }
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
