using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class Zone
    {
        public string Name { get; set; }
        public Rectangle Area { get; set; }

        public Zone(string name, Rectangle area)
        {
            Name = name;
            Area = area;
        }
    }
}
