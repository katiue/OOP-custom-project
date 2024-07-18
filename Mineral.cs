using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class Mineral : Item
    {
        public int _maxquantity;
        public int _stiffness;
        public Color _color;
        public Mineral(string[] ids, string name, string desc, int stiffness,int maxquantity, Color clr)  : base(ids, name, desc)
        {
            _maxquantity = maxquantity;
            _stiffness = stiffness;
            _color = clr;
        }
    }
}
