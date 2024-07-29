using SplashKitSDK;

namespace OOP_custom_project
{
    public class MineralType
    {
        private Color _color;
        private readonly string _description;
        public MineralType(string name, string desc, int stiffness,int maxquantity, Color clr)
        {
            Name = name;
            _description = desc;
            MaxQuantity = maxquantity;
            Stiffness = stiffness;
            _color = clr;
        }
        public int MaxQuantity { get; }
        public int Stiffness { get; }
        public Color Color
        {
            get
            {
                return _color;
            }
        }
        public string Name { get; }
        public string Description
        {
            get
            {
                return _description;
            }
        }
    }
}
