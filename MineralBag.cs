using SplashKitSDK;

namespace OOP_custom_project
{
    public class MineralBag : Item, IHaveInventory, IAmAScreen
    {
        private Mineral? showing;
        private MineralInventory _inventory;
        private DisplayMined? _display;
        private double _offsetY;
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
            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                showing = null;
            }
            // Handle input for panning
            Vector2D pos = SplashKit.MouseMovement();
            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                _offsetY += (float)pos.Y;
            }

            if(showing == null)
            {
                for (int i = 0; i < _inventory.Mineral.Count; i++)
                {
                    if (SplashKit.MouseClicked(MouseButton.LeftButton)&& SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 100 * (i % 8) + 164, 100 * (i / 8) + 64 + _offsetY, 70, 70) && pos.X == 0 && pos.Y == 0)
                    {
                        showing = _inventory.Mineral[i];
                    }
                    _display = null;
                    if (_inventory.Mineral[i] != null && (i / 8) * 100 - 200 + _offsetY > -500 && (i / 8) * 100 - 300 + _offsetY < 300)
                    {

                        _inventory.Mineral[i].Draw((i % 8) * 100 - 200, (i / 8) * 100 - 300 + _offsetY, 0.1);
                        SplashKit.DrawText(_inventory.Mineral[i].Type._name, Color.Black, "Arial", 12, (i % 8) * 100 + 165, (i / 8) * 100 + 140 + _offsetY);
                    }
                }
            }
            else
            {
                DisplayMineral(showing);
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
