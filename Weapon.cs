using SplashKitSDK;

namespace OOP_custom_project
{
    public class Weapon : GameObject
    {
        private string mineral1;
        private string mineral2;
        private int durability;
        private int stiffness;
        private int forgedTimes;
        private Bitmap bitmap;

        public Weapon(string[] ids, string name, string desc, int stiffness, int forgedTimes, string mineral1, string mineral2) : base(ids, name, desc)
        {
            this.mineral1 = mineral1;
            this.mineral2 = mineral2;
            this.stiffness = stiffness;
            this.forgedTimes = forgedTimes;
            CalculateDurability();
            bitmap = LoadBitmap(mineral1, mineral2);
        }

        public int Durability
        {
            get { return durability; }
            set { durability = value; }
        }

        public int Stiffness
        {
            get { return stiffness; }
            set { stiffness = value; }
        }

        public int ForgedTimes
        {
            get { return forgedTimes; }
            set { forgedTimes = value; }
        }

        public string Mineral1
        {
            get { return mineral1; }
            set { mineral1 = value; }
        }
        public string Mineral2
        {
            get { return mineral2; }
            set { mineral2 = value; }
        }

        private void CalculateDurability()
        {
            durability = (stiffness) * (forgedTimes + 1);
        }

        public void Draw(float x, float y)
        {
            SplashKit.DrawBitmap(bitmap, x - (bitmap.Width/2), y - (bitmap.Height/2), SplashKit.OptionScaleBmp((double)70/ (double)bitmap.Width, (double)70/ (double)bitmap.Height));
        }
        private Bitmap LoadBitmap(string mineral1, string mineral2)
        {
            Define define = new();
            if(define.weaponMappings.TryGetValue((mineral1, mineral2), out var weaponMapping))
            {
                bitmap = new Bitmap(weaponMapping.WeaponName, weaponMapping.ImageFile);
            }
            return bitmap;
        }
    }
}
