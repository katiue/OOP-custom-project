using SplashKitSDK;
namespace OOP_custom_project
{
    public class GameObject : IdentifiableObject
    {
        private string[] _ids;
        private string _name;
        private string _description;
        public GameObject(string[] ids,string name , string desc) : base(ids)
        {
            _ids = ids;
            _description = desc;
            _name = name;
        }
        public string[] ID
        {
            get
            {
                return _ids;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public string ShortDescription
        {
            get
            {
                return $"{_name} ({FirstID})";
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
