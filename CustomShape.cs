using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class CustomShape
    {
        private int[,] MultiplePointAvoid;
        private Window window;
        private DrawingOptions drawingOptions = new DrawingOptions();
        private List<Point2D> Points { get; }
        private bool filled = false;

        public CustomShape(Window window)
        {
            Points = new List<Point2D>();
            MultiplePointAvoid = new int[window.Width, window.Height];
            this.window = window;
            drawingOptions.LineWidth = 5;
            drawingOptions.Dest = window;
        }

        public void AddPoint(int x, int y)
        {
            Points.Add(SplashKit.PointAt(x, y));
        }

        public void Draw(Color color)
        {
            if(filled)
            {
                for(int i = 0; i< Points.Count - 2; i++)
                {
                    SplashKit.FillTriangle(color, window.Width/2, window.Height/2, Points[i + 1].X, Points[i + 1].Y, Points[i+2].X, Points[i+2].Y);
                }
            }
            else
            {
                if (Points.Count > 1) // At least 2 points needed to form a shape
                {
                    for (int i = 0; i < Points.Count - 1; i++)
                    {
                        SplashKit.DrawLine(Color.Red, Points[i].X, Points[i].Y, Points[i + 1].X, Points[i + 1].Y, drawingOptions);
                    }
                }
            }
        }

        public void Drawing()
        {
            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                Point2D pos = SplashKit.MousePosition();
                if (pos.X < 0 || pos.Y < 0 || pos.X >= window.Width || pos.Y >= window.Height)
                {
                    return;
                }
                if (MultiplePointAvoid[(int)pos.X, (int)pos.Y] == 0)
                {
                    MultiplePointAvoid[(int)pos.X, (int)pos.Y]++;
                    AddPoint((int)pos.X, (int)pos.Y);
                }
            }
            if (SplashKit.MouseClicked(MouseButton.RightButton))
            {
                Point2D pos = SplashKit.MousePosition();
                if (pos.X < 0 || pos.Y < 0 || pos.X >= window.Width || pos.Y >= window.Height)
                {
                    return;
                }
                MatchesPoints((int)pos.X, (int)pos.Y);
            }
            Draw(Color.Black);
        }
        private void MatchesPoints(int x, int y)
        {
            List<Point2D> copypoint = new List<Point2D>();
            for (int i = 0; i < Points.Count; i++)
            {
                copypoint.Add(Points[i]);
            }
            int limit = Points.Count;
            int currentX;
            int currentY;
            for (int i = 0; i < limit - 1; i++)
            {
                currentX = (int)Points[i].X;
                currentY = (int)Points[i].Y;
                while(currentX != Points[i + 1].X || currentY != Points[i + 1].Y)
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
                    if (MultiplePointAvoid[currentX, currentY] == 0)
                    {
                        MultiplePointAvoid[currentX, currentY] = 1;
                        AddPoint(currentX, currentY);
                    }
                }
            }
            filled = true;
        }
    }
}
