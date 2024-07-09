using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class Mineral : Item
    {
        public Mineral(string[] ids, string name, string desc, int stiffness)  : base(ids, name, desc)
        {
        }
    }
}
