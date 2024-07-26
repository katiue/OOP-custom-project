using SplashKitSDK;

namespace OOP_custom_project
{
    public class WeaponBag : Item, IHaveInventory, IAmAScreen
    {
        private WeaponInventory _inventory;
        private float _offsetY = 0;
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
        public void Draw()
        {
            for (int i = 0; i < _inventory.ComponentList.Count; i++)
            {
                if (_inventory.ComponentList[i] != null)
                    _inventory.ComponentList[i].Draw((i % 8) * 150 + 210, (i / 8) * 100 + 110 + _offsetY);
                SplashKit.DrawText(_inventory.ComponentList[i].Name, Color.Black, "Arial", 12, (i % 8) * 150 + 165, (i / 8) * 100 + 145 + _offsetY);
            }

            // Handle input for panning
            Vector2D pos = SplashKit.MouseMovement();
            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                _offsetY += (float)pos.Y;
            }
        }
    }
}
