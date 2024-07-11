using System.Collections.Generic;
using System.Threading;
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
        private MapObject? AddingObject;

        public InteractiveMap(Window window)
        {
            _mapImage = new Bitmap("Map", @"D:\OOP-custom-project\map.webp");
            _zoom = 1.7f;
            _offsetX = 0.0f;
            _offsetY = 0.0f;
            AddingObject = null;

            // Initialize zones
            _zones = new List<MapObject>
        {
            new MapObject("D:\\OOP-custom-project\\Mining_removedbg2\\", new Rectangle() { X = 100, Y = 100, Width = 200, Height = 200 }, 49, 0.04),
            new MapObject("D:\\OOP-custom-project\\Mining_removedbg2\\", new Rectangle() { X = 300, Y = 300, Width = 200, Height = 200 }, 49, 0.04)
            // Add more zones as needed
        };
            this.window = window;
        }

        public void Draw()
        {
            int _currentFrame = 0;
            DateTime _lastFrameTime = DateTime.Now;
            double _frameDuration = 0.04;
            while(_currentFrame > -1)
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                Bitmap mapImage = new Bitmap("Background", @"D:\OOP-custom-project\background.png");
                SplashKit.DrawBitmap(mapImage, _offsetX-120, _offsetY-120, SplashKit.OptionScaleBmp(2.2, 1.5));
                SplashKit.DrawBitmap(_mapImage, _offsetX, _offsetY, SplashKit.OptionScaleBmp(_zoom, _zoom));

                if (window.CloseRequested)
                    break;

                // Calculate time elapsed and update the frame
                DateTime now = DateTime.Now;
                double elapsedSeconds = (now - _lastFrameTime).TotalSeconds;

                if (elapsedSeconds >= _frameDuration)
                {
                    _currentFrame++;
                    _lastFrameTime = now;
                }

                foreach (var zone in _zones)
                {
                    Rectangle scaledArea = new Rectangle()
                    {
                        X = zone.Area.X / Math.Max(2.5f, _zoom) - (230*(_zoom-1.7)) + _offsetX,
                        Y = zone.Area.Y / Math.Max(2.5f, _zoom) - (130 * (_zoom-1.7)) + _offsetY,
                        Width = zone.Area.Width / Math.Max(2.5f, _zoom),
                        Height = zone.Area.Height / Math.Max(2.5f, _zoom)
                    };
                    ShowObjectGifFrames(window, zone, scaledArea.X, scaledArea.Y, _currentFrame);
                }
                if(AddingObject != null)
                {
                    ShowObjectGifFrames(window, AddingObject, SplashKit.MouseX(), SplashKit.MouseY(), 0);
                }
                DrawToolBox();
                Update();
                SplashKit.RefreshScreen(120);
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
            //detect choosing tool
            if(SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                if(AddingObject == null)
                    DragTool();
                else
                {
                    MapObject addedobj = new MapObject (AddingObject.Name, new Rectangle() { X = SplashKit.MouseX() / Math.Max(2.5f, _zoom) - (230 * (_zoom - 1.7)) + _offsetX, Y = SplashKit.MouseY() / Math.Max(2.5f, _zoom) - (230 * (_zoom - 1.7)) + _offsetY, Width = 200, Height = 200 }, 49, 0.04);
                    _zones.Add(addedobj);
                    AddingObject = null;
                }
            }

            // Handle input for panning
            Vector2D pos = SplashKit.MouseMovement();
            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                if(AddingObject == null)
                {
                    _offsetX += (float)pos.X;
                    _offsetY += (float)pos.Y;
                }
            }
            // Ensure the map stays within bounds
            LimitOffsets();

            // Check for mouse clicks
            if (SplashKit.MouseDown(MouseButton.RightButton))
            {
                float mouseX = SplashKit.MouseX();
                float mouseY = SplashKit.MouseY();

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


        public void ShowObjectGifFrames(Window window, MapObject obj, double x, double y, int _currentFrame)
        {
            _currentFrame = (_currentFrame % obj._frames.Length );
            // Get the dimensions of the box
            int BoxWidth = (int)obj.Area.Width / (int)Math.Max(2.5f, _zoom);
            int BoxHeight = (int)obj.Area.Height / (int)Math.Max(2.5f, _zoom);

            // Get the dimensions of the current frame
            Bitmap currentFrame = obj._frames[_currentFrame];
            int frameWidth = currentFrame.Width;
            int frameHeight = currentFrame.Height;

            // Calculate scaling factors
            double scaleX = (double)BoxWidth / frameWidth;
            double scaleY = (double)BoxHeight / frameHeight;

            //calculate picture position
            int posX = ((BoxWidth - frameWidth) / 2) + (int)x;
            int posY = ((BoxWidth - frameHeight) / 2) + (int)y;

            // Draw the current frame scaled to fit the box
            SplashKit.DrawBitmap(currentFrame, posX, posY, SplashKit.OptionScaleBmp(scaleX, scaleY));
        }
        public void DrawToolBox()
        {
            Bitmap[] tools =
            {
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"),
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"),
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"),
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png")
            };
            SplashKit.FillRectangle(Color.White, 0, 0, 100, 320);

            for (int i = 0; i < tools.Length; i++)
            {
                // Each tool box is 100x100 pixels
                double scaleX = (double)100 / tools[i].Width;
                double scaleY = (double)100 / tools[i].Height;

                //calculate picture position
                int posX = ((100 - tools[i].Width) / 2);
                int posY = ((100 - tools[i].Height) / 2) + i * 80;
                SplashKit.DrawBitmap(tools[i], posX, posY, SplashKit.OptionScaleBmp(scaleX, scaleY));
            }
        }
        public MapObject DragTool()
        {
            float mouseX = SplashKit.MouseX();
            float mouseY = SplashKit.MouseY();

            if (SplashKit.PointInRectangle(mouseX, mouseY, 0, 0, 100, 400))
            {
                AddingObject = new MapObject("D:\\OOP-custom-project\\Mining_removedbg2\\", new Rectangle() { X = mouseX, Y = mouseY, Width = 200, Height = 200 }, 49, 0.04);
            }
            else if(AddingObject != null)
            {
                AddingObject = null;
            }
            return AddingObject;
        }
    }
}
