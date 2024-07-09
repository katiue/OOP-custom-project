using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class Material : Item
    {
        public Material(string[] ids, string name, string desc, int stiffness)  : base(ids, name, desc)
        {
        }
    }
}
