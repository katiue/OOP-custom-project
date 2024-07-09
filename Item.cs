using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class Item : GameObject
    {
        public Item(string[] ids, string name, string desc) : base(ids, name, desc)
        {
        }
    }
}
