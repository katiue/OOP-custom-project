using SplashKitSDK;

namespace OOP_custom_project
{
    public class Mineral : GameObject
    {
        private int[,] MultiplePointAvoid;
        private Window window = SplashKit.CurrentWindow();
        private MineralType _type;
        private double _area;
        private GachaMineral gacha = new();
        private List<Point2D> Points;
        public Mineral(string []id, string name, string desc,MineralType type, List<Point2D> points) : base(id, name, desc)
        {
            MultiplePointAvoid = new int[800, 800];
            _type = type;
            if(points.Count == 0)
            {
                Points = gacha.points();
            }
            else
            {
                Points = points;
            }
            _area = CalculateArea();
        }
        public List<Point2D> points
        {
            get
            {
                return Points;
            }
            set
            {
                Points = value;
            }
        }
        public MineralType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        public double Area
        {
            get
            {
                return _area;
            }
        }
        private double CalculateArea()
        {
            double area = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                area += Points[i].X * Points[i + 1].Y - Points[i + 1].X * Points[i].Y;
            }
            area += Points[Points.Count - 1].X * Points[0].Y - Points[0].X * Points[Points.Count - 1].Y;
            return Math.Abs(area / 2);
        }
        private List<Point2D> MatchesPoints()
        {
            List<Point2D> points = new();
            for (int i = 0; i < Points.Count; i++)
            {
                points.Add(Points[i]);
            }
            int currentX;
            int currentY;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                currentX = (int)Points[i].X;
                currentY = (int)Points[i].Y;
                while (currentX != Points[i + 1].X || currentY != Points[i + 1].Y)
                {
                    if (currentX < (int)Points[i + 1].X)
                    {
                        currentX++;
                    }
                    if (currentX > (int)Points[i + 1].X)
                    {
                        currentX--;
                    }
                    if (currentY < (int)Points[i + 1].Y)
                    {
                        currentY++;
                    }
                    if (currentY > (int)Points[i + 1].Y)
                    {
                        currentY--;
                    }
                    if(window != null)
                    {
                        if (currentX < 800 && currentY < 800 && MultiplePointAvoid[currentX, currentY] == 0)
                        {
                            MultiplePointAvoid[currentX, currentY] = 1;
                            points.Add(SplashKit.PointAt(currentX, currentY));
                        }
                    }
                }
            }
            return points;
        }
        public void Draw(double x, double y, double scale)
        {
            Bitmap baseImage;
            baseImage = new Bitmap("BaseImage", 800, 800);
            baseImage.Clear(Color.White);
            List<Point2D> pts = Points;
            for (int i = 0; i < pts.Count - 1; i++)
            {
                SplashKit.FillTriangleOnBitmap(baseImage, _type._color, 800 / 2, 800 / 2, pts[i].X, pts[i].Y, pts[i + 1].X, pts[i + 1].Y);
            }
            SplashKit.FillTriangleOnBitmap(baseImage, _type._color, 800 / 2, 800 / 2, pts[pts.Count - 1].X, pts[pts.Count - 1].Y, pts[0].X, pts[0].Y);
            SplashKit.DrawBitmap(baseImage, x, y, SplashKit.OptionScaleBmp(scale, scale));
            baseImage.Dispose();
        }
    }
}
