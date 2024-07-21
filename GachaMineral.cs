using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class GachaMineral
    {
        private Random random;
        public GachaMineral() 
        {
            random = new Random();
        }
        public List<Point2D> points()
        {
            List<Point2D> point2Ds = new List<Point2D>();
            Point2D pt = new Point2D();
            int numPoints = random.Next(2, 3);
            for (int i = 0;i < numPoints; i++)
            {
                pt.X = Pull();
                pt.Y = Pull();
                point2Ds.Add(pt);
            }
            return point2Ds;
        }
        public double Pull()
        {
            double roll = random.NextDouble();
            int dis;
            if (roll < 0.50)
            {
                dis = random.Next(20, 100 + 1);
            }
            else if (roll < 0.70)
            {
                dis = random.Next(101, 170 + 1);
            }
            else if (roll < 0.90)
            {
                dis = random.Next(171, 230 + 1);
            }
            else if (roll < 0.9975)
            {
                dis = random.Next(231, 250 + 1);
            }
            else
            {
                dis = random.Next(251, 260 + 1);
            }
            return 500 + (dis * RandomSign());
        }
        private int RandomSign()
        {
            return random.Next(0, 2) == 0 ? -1 : 1;
        }
    }
}
