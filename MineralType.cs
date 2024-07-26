using SplashKitSDK;

namespace OOP_custom_project
{
    public class MineralType
    {
        public int _maxquantity;
        public int _stiffness;
        public Color _color;
        public string _name;
        public string _description;
        public MineralType(string name, string desc, int stiffness,int maxquantity, Color clr)
        {
            _name = name;
            _description = desc;
            _maxquantity = maxquantity;
            _stiffness = stiffness;
            _color = clr;
        }
    }
}
