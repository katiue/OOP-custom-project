using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class WeaponBag : Item, IHaveInventory
    {
        private Weapon? showing;
        private WeaponInventory _inventory;
        private WeaponForging? _forging;
        public WeaponBag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new WeaponInventory();
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
                return "In the " + Name + " you can see:\n " + _inventory.ComponentList;
            }
        }
        public WeaponInventory Inventory
        {
            get
            {
                return _inventory;
            }
        }
        public void Draw(Game game)
        {
            Window window = SplashKit.CurrentWindow();
            if (SplashKit.KeyTyped(KeyCode.EscapeKey) || window.CloseRequested)
            {
                if (showing != null)
                    _inventory.Put(showing);
                showing = null;
            }
            if (showing != null)
                ForgeComponent(showing, game);
            for (int i = 0; i < _inventory.ComponentList.Count; i++)
            {
                if (SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 100 * (i % 8) + 164, 100 * (i / 8) + 64, 70, 70))
                {
                    showing = _inventory.Take(_inventory.ComponentList[i].ID[0]);
                }
                if (showing == null)
                {
                    _forging = null;
                    if (_inventory.ComponentList[i] != null)
                        _inventory.ComponentList[i].Draw((i % 8) * 100 + 210, (i / 8) * 100 + 110, 0.07);
                    SplashKit.DrawText(_inventory.ComponentList[i].Name, Color.Black, "Arial", 12, (i % 8) * 100 + 165, (i / 8) * 100 + 140);
                }
            }
        }
        private void ForgeComponent(Weapon component, Game game)
        {
            if (_forging == null)
            {
                _forging = new WeaponForging(game);
            }
            _forging.Draw();
        }
    }
}
