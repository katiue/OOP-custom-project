using System.Collections.Generic;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class InteractiveMap
    {
        private Bitmap _mapImage;
        private float _zoom;
        private float _offsetX, _offsetY;
        private List<Zone> _zones;
        private const int WindowWidth = 1000;
        private const int WindowHeight = 700;

        public InteractiveMap()
        {
            _mapImage = new Bitmap("Map", @"D:\OOP-custom-project\map.webp");
            _zoom = 1.7f;
            _offsetX = 0.0f;
            _offsetY = 0.0f;

            // Initialize zones
            _zones = new List<Zone>
        {
            new Zone("Zone 1", new Rectangle() { X = 100, Y = 100, Width = 200, Height = 200 }),
            new Zone("Zone 2", new Rectangle() { X = 300, Y = 300, Width = 200, Height = 200 })
            // Add more zones as needed
        };
        }

        public void Draw()
        {
            Bitmap mapImage = new Bitmap("Background", @"D:\OOP-custom-project\background.png");
            SplashKit.DrawBitmap(mapImage, _offsetX-120, _offsetY-120, SplashKit.OptionScaleBmp(2.2, 1.5));
            SplashKit.DrawBitmap(_mapImage, _offsetX, _offsetY, SplashKit.OptionScaleBmp(_zoom, _zoom));
            foreach (var zone in _zones)
            {
                Rectangle scaledArea = new Rectangle()
                {
                    X = (zone.Area.X * _zoom) + _offsetX,
                    Y = (zone.Area.Y * _zoom) + _offsetY,
                    Width = zone.Area.Width * _zoom,
                    Height = zone.Area.Height * _zoom
                };

                SplashKit.FillRectangle(Color.Green, scaledArea);
            }
        }

        public void Update()
        {
            // Handle input for zooming
            Vector2D scroll = SplashKit.MouseWheelScroll();
            // Calculate the new zoom level
            if ((_zoom + (float)scroll.Y / 10.0f > 1.7f) && (_zoom + (float)scroll.Y / 10.0f < 3f))
            {
                _zoom += (float)scroll.Y / 10.0f;
            }
            Console.WriteLine(_offsetX + " " + _offsetY + " " + _zoom);

            // Handle input for panning
            Vector2D pos = SplashKit.MouseMovement();
            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                _offsetX += (float)pos.X;
                _offsetY += (float)pos.Y;

                // Ensure the map stays within bounds
            }
            LimitOffsets();

            // Check for mouse clicks
            if (SplashKit.MouseClicked(MouseButton.RightButton))
            {
                float mouseX = SplashKit.MouseX() - _offsetX;
                float mouseY = SplashKit.MouseY() - _offsetY;

                foreach (var zone in _zones)
                {
                    Rectangle scaledArea = new Rectangle()
                    {
                        X = (zone.Area.X * _zoom) + _offsetX,
                        Y = (zone.Area.Y * _zoom) + _offsetY,
                        Width = zone.Area.Width * _zoom,
                        Height = zone.Area.Height * _zoom
                    };

                    if (SplashKit.PointInRectangle(mouseX, mouseY, scaledArea.X, scaledArea.Y, scaledArea.Width, scaledArea.Height))
                    {
                        ShowZoneDetails(zone);
                        break;
                    }
                }
            }
        }
        private void LimitOffsets()
        {
            float scaledMapWidth = _mapImage.Width * _zoom;
            float scaledMapHeight = _mapImage.Height * _zoom;

            float maxOffsetX = (WindowWidth - scaledMapWidth)/2;
            float maxOffsetY = (WindowHeight - scaledMapHeight)/2;

           // Prevent moving too far to the left or top
            if (_offsetX > 1080) _offsetX = 1080;
            if (_offsetY > 810) _offsetY = 810;

            // Prevent moving too far to the right or bottom
            if (_offsetX < -1160) _offsetX = -1160;
            if (_offsetY < -920) _offsetY = -920;
        }

        private void ShowZoneDetails(Zone zone)
        {
            Font font = new Font("Arial", "Arial.ttf");
            SplashKit.DisplayDialog(zone.Name, "Details about " + zone.Name, font, 12);
        }
    }
}
