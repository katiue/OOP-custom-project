using SplashKitSDK;

namespace OOP_custom_project
{
    public class Mineral : GameObject
    {
        private readonly GachaMineral gacha = new();

        public Mineral(string []id, string name, string desc,MineralType type, List<Point2D> points) : base(id, name, desc)
        {
            Type = type;
            if(points.Count == 0)
            {
                this.Points = gacha.Points();
            }
            else
            {
                this.Points = points;
            }
        }
        public List<Point2D> Points { get; set; }
        public MineralType Type { get; set; }
        public double Area
        {
            get
            {
                return CalculateArea();
            }
        }
        private double CalculateArea()
        {
            double area = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                area += Points[i].X * Points[i + 1].Y - Points[i + 1].X * Points[i].Y;
            }
            return Math.Abs(area / 2);
        }
        public override void Draw(double x, double y, double scale)
        {
            Bitmap baseImage;
            baseImage = new Bitmap("BaseImage", 800, 800);
            baseImage.Clear(Color.White);
            List<Point2D> pts = Points;
            for (int i = 0; i < pts.Count - 1; i++)
            {
                SplashKit.FillTriangleOnBitmap(baseImage, Type.Color, 800 / 2, 800 / 2, pts[i].X, pts[i].Y, pts[i + 1].X, pts[i + 1].Y);
            }
            SplashKit.FillTriangleOnBitmap(baseImage, Type.Color, 800 / 2, 800 / 2, pts[^1].X, pts[^1].Y, pts[0].X, pts[0].Y);
            SplashKit.DrawBitmap(baseImage, x, y, SplashKit.OptionScaleBmp(scale, scale));
            baseImage.Dispose();
        }
    }
}
