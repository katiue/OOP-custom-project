using SplashKitSDK;

namespace OOP_custom_project
{
    public class WeaponBag : GameObject, IHaveInventory, IAmAScreen
    {
        private float _offsetY = 0;
        public WeaponBag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            Inventory = new WeaponInventory();
        }
        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            else
            {
                return Inventory.Fetch(id);
            }
        }
        public override string FullDescription
        {
            get
            {
                return "In the " + Name + " you can see:\n " + Inventory.WeaponList;
            }
        }
        public WeaponInventory Inventory { get; }
        public override void Draw()
        {
            for (int i = 0; i < Inventory.WeaponList.Count; i++)
            {
                Inventory.WeaponList[i]?.Draw((i % 8) * 150 + 210, (i / 8) * 100 + 110 + _offsetY);
                SplashKit.DrawText(Inventory.WeaponList[i].Name, Color.Black, "Arial", 12, (i % 8) * 150 + 165, (i / 8) * 100 + 145 + _offsetY);
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
