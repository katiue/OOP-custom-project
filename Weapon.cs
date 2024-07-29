using SplashKitSDK;

namespace OOP_custom_project
{
    public class Weapon : GameObject
    {
        private string mineral2;
        private int forgedTimes;
        private Bitmap bitmap;
        private readonly Define define = new();

        public Weapon(string[] ids, string name, string desc, int stiffness, int forgedTimes, string mineral1, string mineral2) : base(ids, name, desc)
        {
            this.Mineral1 = mineral1;
            this.mineral2 = mineral2;
            this.Stiffness = stiffness;
            this.forgedTimes = forgedTimes;
            CalculateDurability();
            bitmap = LoadBitmap(mineral1, mineral2);
        }

        public int Durability { get; set; }

        public int Stiffness { get; set; }

        public int ForgedTimes
        {
            get { return forgedTimes; }
            set { forgedTimes = value; }
        }

        public string Mineral1 { get; set; }
        public string Mineral2
        {
            get { return mineral2; }
            set { mineral2 = value; }
        }

        private void CalculateDurability()
        {
            Durability = (Stiffness) * (forgedTimes + 1);
        }

        public override void Draw(double x, double y)
        {
            SplashKit.DrawBitmap(bitmap, x - (bitmap.Width/2), y - (bitmap.Height/2), SplashKit.OptionScaleBmp((double)70/ (double)bitmap.Width, (double)70/ (double)bitmap.Height));
        }
        private Bitmap LoadBitmap(string mineral1, string mineral2)
        {
            if(define.weaponMappings.TryGetValue((mineral1, mineral2), out var weaponMapping))
            {
                bitmap = new Bitmap(weaponMapping.WeaponName, weaponMapping.ImageFile);
            }
            return bitmap;
        }
    }
}
