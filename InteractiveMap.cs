using System.Collections.Generic;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class InteractiveMap
    {
        private Bitmap _mapImage;
        private float _zoom;
        private float _offsetX, _offsetY;
        private List<MapObject> _zones;
        private Window window;

        public InteractiveMap(Window window)
        {
            _mapImage = new Bitmap("Map", @"D:\OOP-custom-project\map.webp");
            _zoom = 1.7f;
            _offsetX = 0.0f;
            _offsetY = 0.0f;


            // Initialize zones
            _zones = new List<MapObject>
        {
            new MapObject("D:\\OOP-custom-project\\Mining_removedbg\\", new Rectangle() { X = 100, Y = 100, Width = 100, Height = 100 }, 49, 0.04),
            new MapObject("D:\\OOP-custom-project\\Mining_removedbg\\", new Rectangle() { X = 300, Y = 300, Width = 100, Height = 100 }, 49, 0.04)
            // Add more zones as needed
        };
            this.window = window;
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
                    X = zone.Area.X / Math.Max(2.5f, _zoom) - (230*(_zoom-1.7)) + _offsetX,
                    Y = zone.Area.Y / Math.Max(2.5f, _zoom) - (130 * (_zoom-1.7)) + _offsetY,
                    Width = zone.Area.Width / Math.Max(2.5f, _zoom),
                    Height = zone.Area.Height / Math.Max(2.5f, _zoom)
                };

                zone.ShowGifFrames(window);
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
                        X = zone.Area.X / Math.Max(2.5f, _zoom) - (230 * (_zoom - 1.7)) + _offsetX,
                        Y = zone.Area.Y / Math.Max(2.5f, _zoom) - (130 * (_zoom - 1.7)) + _offsetY,
                        Width = zone.Area.Width / Math.Max(2.5f, _zoom),
                        Height = zone.Area.Height / Math.Max(2.5f, _zoom)
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
           // Prevent moving too far to the left or top
            if (_offsetX > 1080) _offsetX = 1080;
            if (_offsetY > 810) _offsetY = 810;

            // Prevent moving too far to the right or bottom
            if (_offsetX < -1160) _offsetX = -1160;
            if (_offsetY < -920) _offsetY = -920;
        }

        private void ShowZoneDetails(MapObject zone)
        {
            Font font = new Font("Arial", "Arial.ttf");
            SplashKit.DisplayDialog(zone.Name, "Details about " + zone.Name, font, 12);
        }
    }
}
