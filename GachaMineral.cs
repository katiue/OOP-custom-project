using SplashKitSDK;

namespace OOP_custom_project
{
    public class GachaMineral
    {
        private readonly Random random;
        public GachaMineral() 
        {
            random = new Random();
        }
        public List<Point2D> Points()
        {
            List<Point2D> point2Ds = [];
            Point2D pt = new();
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
            if (roll < 0.40)
            {
                dis = random.Next(20, 100 + 1);
            }
            else if (roll < 0.65)
            {
                dis = random.Next(101, 170 + 1);
            }
            else if (roll < 0.85)
            {
                dis = random.Next(171, 230 + 1);
            }
            else if (roll < 0.95)
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
