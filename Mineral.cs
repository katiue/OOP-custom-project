using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class Mineral : GameObject
    {
        private int[,] MultiplePointAvoid;
        private Window window = SplashKit.CurrentWindow();
        private MineralType _type;
        private GachaMineral gacha = new();
        private List<Point2D> Points;
        private List<Point2D> ShowedPoints { get; set; }
        public Mineral(string []id, string name, string desc,MineralType type) : base(id, name, desc)
        {
            if(window != null)
                MultiplePointAvoid = new int[window.Width, window.Width];
            else
                MultiplePointAvoid = new int[1000, 1000];
            _type = type;

            Points = gacha.points();
            ShowedPoints = MatchesPoints();
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
                return CalculateArea();
            }
        }
        public List<Point2D> showedPoints
        {
            get
            {
                return ShowedPoints;
            }
            set
            {
                ShowedPoints = value;
            }
        }
        private double CalculateArea()
        {
            double area = 0;
            for (int i = 0; i < ShowedPoints.Count - 1; i++)
            {
                area += ShowedPoints[i].X * ShowedPoints[i + 1].Y - ShowedPoints[i + 1].X * ShowedPoints[i].Y;
            }
            area += ShowedPoints[ShowedPoints.Count - 1].X * ShowedPoints[0].Y - ShowedPoints[0].X * ShowedPoints[ShowedPoints.Count - 1].Y;
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
                        if (currentX < window.Width && currentY < window.Height && MultiplePointAvoid[currentX, currentY] == 0)
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
            if (window != null)
            {
                baseImage = new Bitmap("BaseImage", window.Width, window.Width);
                baseImage.Clear(Color.White);
                List<Point2D> pts = showedPoints;
                for (int i = 0; i < pts.Count - 1; i++)
                {
                    SplashKit.FillTriangleOnBitmap(baseImage, _type._color, window.Width / 2, window.Width / 2, pts[i].X, pts[i].Y, pts[i + 1].X, pts[i + 1].Y);
                }
                SplashKit.FillTriangleOnBitmap(baseImage, _type._color, window.Width / 2, window.Width / 2, pts[pts.Count - 1].X, pts[pts.Count - 1].Y, pts[0].X, pts[0].Y);
                SplashKit.DrawBitmap(baseImage, x, y, SplashKit.OptionScaleBmp(scale, scale));
                baseImage.Dispose();
            }
            else
            {
                baseImage = new Bitmap("BaseImage", 1000, 1000);
                baseImage.Clear(Color.White);
                List<Point2D> pts = showedPoints;
                for (int i = 0; i < pts.Count - 1; i++)
                {
                    SplashKit.FillTriangleOnBitmap(baseImage, _type._color, 1000 / 2, 1000 / 2, pts[i].X, pts[i].Y, pts[i + 1].X, pts[i + 1].Y);
                }
                SplashKit.FillTriangleOnBitmap(baseImage, _type._color, 1000 / 2, 1000 / 2, pts[pts.Count - 1].X, pts[pts.Count - 1].Y, pts[0].X, pts[0].Y);
                SplashKit.DrawBitmap(baseImage, x, y, SplashKit.OptionScaleBmp(scale, scale));
                baseImage.Dispose();
            }
        }
    }
}
