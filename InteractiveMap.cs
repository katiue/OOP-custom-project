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
        private Bitmap? AddingObject;
        private Bitmap[] tools;
        public string ChangeScreen = "";

        public InteractiveMap(Window window, Game game)
        {
            _mapImage = new Bitmap("Map", @"D:\OOP-custom-project\map.webp");
            _zoom = 1.0f;
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

            tools = [
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"),
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"),
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"),
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png")
            ];
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
                SplashKit.DrawBitmapOnBitmap(_mapImage, new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"), 2000, 800);
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

                // Create a new bitmap with a transparent background
                Bitmap baseImage = new Bitmap("BaseImage", _mapImage.Width, _mapImage.Height);
                baseImage.Clear(Color.RGBAColor(0, 0, 0, 0));

                foreach (var zone in _zones)
                {
                    Rectangle scaledArea = zone.Area;
                    scaledArea.X = zone.Area.X + _offsetX;
                    scaledArea.Y = zone.Area.Y + _offsetY;
                    scaledArea.Width = zone.Area.Width / Math.Max(2.5f, _zoom);
                    scaledArea.Height = zone.Area.Height / Math.Max(2.5f, _zoom);

                    double addX = zone.Area.X - (scaledArea.Width / 2);
                    double addY = zone.Area.Y - (scaledArea.Height / 2);

                    //Find middle point of the map
                    double midX = _mapImage.Width / 2;
                    double midY = _mapImage.Height / 2;

                    //calculate object position
                    double posX = midX + _offsetX - ((midX - addX) * _zoom);
                    double posY = midY + _offsetY - ((midY - addY) * _zoom);

                    // Check for mouse clicks
                    if (SplashKit.MouseDown(MouseButton.RightButton))
                    {
                        float mouseX = SplashKit.MouseX();
                        float mouseY = SplashKit.MouseY();

                        if (SplashKit.PointInRectangle(mouseX, mouseY, posX, posY, scaledArea.Width * _zoom, scaledArea.Height * _zoom))
                        {
                            CollectMaterial(zone);
                            ChangeScreen = "molding";
                            Console.WriteLine(ChangeScreen);
                        }
                    }
                    ShowObjectGifFrames(window, zone, _currentFrame, baseImage);
                }
                SplashKit.DrawBitmap(baseImage, _offsetX, _offsetY, SplashKit.OptionScaleBmp(_zoom, _zoom));
                baseImage.Free();

                if(AddingObject != null)
                {
                    // Each tool box is 100x100 pixels
                    double scaleX = (double)100 / AddingObject.Width;
                    double scaleY = (double)100 / AddingObject.Height;

                    //calculate picture position
                    int posX = ((int)SplashKit.MouseX() - (AddingObject.Width / 2));
                    int posY = ((int)SplashKit.MouseY() - (AddingObject.Height / 2));

                    SplashKit.DrawBitmap(AddingObject, posX, posY, SplashKit.OptionScaleBmp(scaleX, scaleY));
                }
                DrawToolBox();
                Update();
                if(ChangeScreen != "")
                {                     
                    break;
                }
                SplashKit.RefreshScreen(120);
            }
        }

        public void Update()
        {
            // Handle input for zooming
            Vector2D scroll = SplashKit.MouseWheelScroll();
            // Calculate the new zoom level
            //if ((_zoom + (float)scroll.Y / 10.0f > 1.7f) && (_zoom + (float)scroll.Y / 10.0f < 3f))
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
                    MapObject addedobj;
                    double addX = (SplashKit.MouseX() - (_offsetX - (_mapImage.Width * _zoom - _mapImage.Width) / 2))/_zoom;
                    double addY = (SplashKit.MouseY() - (_offsetY - (_mapImage.Height * _zoom - _mapImage.Height) / 2))/_zoom;
                    switch (AddingObject.Name)
                    {
                        case "ToolboxMining":
                            addedobj = new MapObject("D:\\OOP-custom-project\\Mining_removedbg2\\", new Rectangle() { X = addX, Y = addY, Width = 200, Height = 200 }, 49, 0.04);
                            _zones.Add(addedobj);
                            break;
                        /*case "ToolboxSmashing":
                            addedobj = new Bitmap("Smashing_hammer", @"D:\OOP-custom-project\Smashing_hammer\frame_00_delay-0.03s.png");
                            break;
                        case "ToolboxPouring":
                            addedobj = new Bitmap("Pouring-molten-metal", @"D:\OOP-custom-project\Pouring-molten-metal\frame_00_delay-0.1s.png");
                            break;
                        case "ToolboxDrawing":
                            addedobj = new Bitmap("Drawing", @"D:\OOP-custom-project\Drawing\frame_00_delay-0.1s.png");
                            break;*/
                    }
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

        private int[,] CollectMaterial(MapObject zone)
        {
            // Collect material from the zone
            return zone.CollectMaterial();
        }


        public void ShowObjectGifFrames(Window window, MapObject obj, int _currentFrame, Bitmap baseImage)
        {
            _currentFrame = (_currentFrame % obj._frames.Length );
            // Get the dimensions of the box
            double BoxWidth = obj.Area.Width / Math.Max(2.5f, _zoom);
            double BoxHeight = obj.Area.Height / Math.Max(2.5f, _zoom);

            // Get the dimensions of the current frame
            Bitmap currentFrame = obj._frames[_currentFrame];
            int frameWidth = currentFrame.Width;
            int frameHeight = currentFrame.Height;

            // Calculate scaling factors
            double scaleX = (double)BoxWidth / frameWidth;
            double scaleY = (double)BoxHeight / frameHeight;

            // Draw the current frame scaled to fit the box
            SplashKit.DrawBitmapOnBitmap(baseImage, currentFrame, obj.Area.X - frameWidth / 2, obj.Area.Y - frameHeight / 2, SplashKit.OptionScaleBmp(scaleX, scaleY));
        }
        public void DrawToolBox()
        {
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
        public Bitmap DragTool()
        {
            float mouseX = SplashKit.MouseX();
            float mouseY = SplashKit.MouseY();

            if (SplashKit.PointInRectangle(mouseX, mouseY, 0, 0, 100, 400))
            {
                AddingObject = tools[(int)mouseY % (400/100)];
            }
            else if(AddingObject != null)
            {
                AddingObject = null;
            }
            return AddingObject;
        }
    }
}
