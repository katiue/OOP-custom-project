using SplashKitSDK;

namespace OOP_custom_project
{
    public class Weapon : GameObject
    {
        private Bitmap bitmap;
        private readonly Define define = new();

        public Weapon(string[] ids, string name, string desc, int stiffness, int forgedTimes, string mineral1, string mineral2) : base(ids, name, desc)
        {
            Mineral1 = mineral1;
            Mineral2 = mineral2;
            Stiffness = stiffness;
            ForgedTimes = forgedTimes;
            CalculateDurability();
            bitmap = LoadBitmap(mineral1, mineral2);
        }

        public int Durability { get; set; }

        public int Stiffness { get; set; }

        public int ForgedTimes { get; set; }

        public string Mineral1 { get; set; }
        public string Mineral2 { get; set; }

        private void CalculateDurability()
        {
            Durability = (Stiffness) * (ForgedTimes + 1);
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
