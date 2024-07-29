using SplashKitSDK;
namespace OOP_custom_project
{
    public class GameObject : IdentifiableObject
    {
        private readonly string _description;
        public GameObject(string[] ids,string name , string desc) : base(ids)
        {
            ID = ids;
            _description = desc;
            Name = name;
        }
        public string[] ID { get; }
        public string Name { get; }
        public string ShortDescription
        {
            get
            {
                return $"{Name} ({FirstID})";
            }
        }
        public virtual string FullDescription
        {
            get
            {
                return _description;
            }
        }
        public virtual void Draw()
        {
            SplashKit.DrawText(ShortDescription, Color.Black, 0, 0);
        }
        public virtual void Draw(double x,double y)
        {
            SplashKit.DrawText(ShortDescription, Color.Black, 0, 0);
        }
        public virtual void Draw(double x, double y, double scale)
        {
            SplashKit.DrawText(ShortDescription, Color.Black, 0, 0);
        }
    }
}
