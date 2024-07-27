using OfficeOpenXml;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class InteractiveMap : IAmAScreen
    {
        private float _zoom;
        private float _offsetX, _offsetY;
        private bool notification = false;
        private Bitmap _mapImage;
        private Bitmap? AddingObject;
        private Bitmap? examine;
        private Bitmap[] tools;
        private Game _game;
        private List<MapObject> _zones;
        private List<Mineral> obtainedmineral;
        private Define definezone = new Define();
        private string id;

        public InteractiveMap(Window window, Game game)
        {
            id = IDgenerator();
            obtainedmineral = new List<Mineral>();
            _game = game;
            _mapImage = new Bitmap("Map", @"D:\OOP-custom-project\Image\map.webp");
            _zoom = 1.7f;
            _offsetX = 0.0f;
            _offsetY = 0.0f;
            AddingObject = null;
            // Initialize zones
            _zones = new List<MapObject>
            {
                new MapObject("D:\\OOP-custom-project\\Mining_removedbg2\\", new Rectangle() { X = 100, Y = 100, Width = 200, Height = 200 }, 49, 0.04, definezone.AssignMineral(100, 100)),
                new MapObject("D:\\OOP-custom-project\\Mining_removedbg2\\", new Rectangle() { X = 300, Y = 300, Width = 200, Height = 200 }, 49, 0.04, definezone.AssignMineral(300, 300))
                // Add more zones as needed
            };

            tools = [
                new Bitmap("PikachuMiner", @"D:\OOP-custom-project\pikachu_mining\frame_0_delay-0.1s.png"),
                new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"),
                new Bitmap("BombMining", @"D:\OOP-custom-project\Bomb_miner_removeBG\frame_00_delay-0.04s.png"),
                new Bitmap("Examine", @"D:\OOP-custom-project\Image\Magnifier.png")
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

                Bitmap mapImage = new Bitmap("Background", @"D:\OOP-custom-project\Image\background.png");
                SplashKit.DrawBitmap(mapImage, _offsetX-120, _offsetY-120, SplashKit.OptionScaleBmp(2.2, 1.5));
                SplashKit.DrawBitmapOnBitmap(_mapImage, new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"), 2000, 800);
                SplashKit.DrawBitmap(_mapImage, _offsetX, _offsetY, SplashKit.OptionScaleBmp(_zoom, _zoom));

                if (_game.window.CloseRequested)
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
                    if (SplashKit.MouseClicked(MouseButton.RightButton) && obtainedmineral.Count == 0)
                    {
                        float mouseX = SplashKit.MouseX();
                        float mouseY = SplashKit.MouseY();

                        if (SplashKit.PointInRectangle(mouseX, mouseY, posX, posY, scaledArea.Width * _zoom, scaledArea.Height * _zoom))
                        {
                            int temp = int.Parse(id);
                            temp++;
                            id = temp.ToString();
                            Mineral mineral = new Mineral(new string[] { id }, "", "", CollectMaterial(zone), new List<Point2D>());
                            obtainedmineral.Add(mineral);
                            _game.bag.MineralBag.Inventory.Put(mineral);
                            notification = true;
                        }
                    }

                    //Run objects animation
                    ShowObjectGifFrames(_game.window, zone, _currentFrame, baseImage);
                }
                //Draw animation layer
                SplashKit.DrawBitmap(baseImage, _offsetX, _offsetY, SplashKit.OptionScaleBmp(_zoom, _zoom));

                if (SplashKit.KeyDown(KeyCode.EscapeKey))
                {
                    _game.ChangeScreen("starting");
                    return;
                }

                if (SplashKit.MouseDown(MouseButton.LeftButton))
                {
                    examine = null;
                    notification = false;
                    obtainedmineral.Clear();
                }
                if (notification)
                {
                    SplashKit.DrawBitmap(DrawObtained(), 400, 200, SplashKit.OptionScaleBmp(2,2));
                }
                baseImage.Free();

                if(examine !=null)
                {
                    SplashKit.DrawBitmap(examine, 400, 200, SplashKit.OptionScaleBmp(2, 2));
                }
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
                SplashKit.RefreshScreen(120);
            }
        }
        public void Update()
        {
            // Handle input for zooming
            Vector2D scroll = SplashKit.MouseWheelScroll();
            // Calculate the new zoom level
            if ((_zoom + (float)scroll.Y / 10.0f > 1.7f) && (_zoom + (float)scroll.Y / 10.0f < 3.1f))
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
                        case "PikachuMiner":
                            addedobj = new MapObject(@"D:\OOP-custom-project\pikachu_mining\", new Rectangle() { X = addX, Y = addY, Width = 150, Height = 150 }, 2, 0.1, definezone.AssignMineral(addX - 100, addY - 100));
                            _zones.Add(addedobj);
                            break;
                        case "ToolboxMining":
                            addedobj = new MapObject(@"D:\OOP-custom-project\Mining_removedbg2\", new Rectangle() { X = addX, Y = addY, Width = 200, Height = 200 }, 49, 0.04, definezone.AssignMineral(addX - 100, addY - 100));
                            _zones.Add(addedobj);
                            break;
                        case "BombMining":
                            DisplayBomb(addX - 100, addY - 100);
                            break;
                        case "Examine":
                            examine = Displayexamine(addX - 100, addY - 100);
                            break;
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
        private MineralType CollectMaterial(MapObject zone)
        {
            // Collect material from the zone
            return definezone.AssignMineral(zone.Area.X, zone.Area.Y);
        }
        private void ShowObjectGifFrames(Window window, MapObject obj, int _currentFrame, Bitmap baseImage)
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
        private void DrawToolBox()
        {
            SplashKit.FillRectangle(Color.White, 0, 0, 100, 400);

            for (int i = 0; i < tools.Length; i++)
            {
                // Each tool box is 100x100 pixels
                double scaleX = (double)100 / tools[i].Width;
                double scaleY = (double)100 / tools[i].Height;

                //calculate picture position
                int posX = ((100 - tools[i].Width) / 2);
                int posY = ((100 - tools[i].Height) / 2) + i * 100;
                SplashKit.DrawBitmap(tools[i], posX, posY, SplashKit.OptionScaleBmp(scaleX, scaleY));
            }
        }
        private Bitmap DragTool()
        {
            float mouseX = SplashKit.MouseX();
            float mouseY = SplashKit.MouseY();

            if (SplashKit.PointInRectangle(mouseX, mouseY, 0, 0, 100, 400))
            {
                AddingObject = tools[(int)(mouseY) / 100];
            }
            else if(AddingObject != null)
            {
                AddingObject = null;
            }
            return AddingObject;
        }
        private Bitmap DrawObtained()
        {
            Bitmap obtained = new Bitmap("obtained", @"D:\OOP-custom-project\Image\ObtainFrame.png");
            Bitmap baseimg = new Bitmap("bitmap", obtained.Width, obtained.Height+20);
            baseimg.Clear(Color.RGBAColor(255, 165, 0, 64));
            SplashKit.DrawBitmapOnBitmap(baseimg, obtained, 0, 0);
            SplashKit.DrawTextOnBitmap(baseimg, "Obtained", Color.WhiteSmoke, "Arial", 0, 110, 30);
            if(obtainedmineral.Count == 1)
            {
                obtainedmineral[0].Draw(50,-100,0.15);
                showdetails(obtainedmineral[0]);
            }
            else
            {
                for(int i = 0; i < obtainedmineral.Count; i++)
                {
                    obtainedmineral[i].Draw(-20 + i * 80, -100, 0.15);
                }
            }
            SplashKit.DrawTextOnBitmap(baseimg, "Click anywhere to continue", Color.WhiteSmoke, "Arial", 40, 40, baseimg.Height -  20);
            return baseimg;
        }
        private void showdetails(Mineral mineral)
        {
            Bitmap bitmap = new Bitmap("bitmap", 200, 200);
            bitmap.Clear(Color.RGBAColor(0, 0, 0, 0));
            //show details of the mineral
            SplashKit.DrawTextOnBitmap(bitmap,"Type: " + mineral.Type._name, Color.White, "Arial", 20, 30, 70);
            SplashKit.DrawTextOnBitmap(bitmap, "Stiffness: " + mineral.Type._stiffness, Color.White, "Arial", 20, 30, 90);
            SplashKit.DrawTextOnBitmap(bitmap, "Size: " + Math.Abs(mineral.Area), Color.White, "Arial", 20, 30, 110);
            SplashKit.DrawBitmap(bitmap, 600, 200, SplashKit.OptionScaleBmp(1.5,1.5));
        }
        private string IDgenerator()
        {
            FileInfo fileInfo = new FileInfo("D:\\OOP-custom-project\\Mineral.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Minerals"];

                if (worksheet == null)
                    throw new Exception("Worksheet 'Minerals' not found in the Excel file.");

                //use the newest id to add new mineral
                int rows = worksheet.Dimension.Rows;
                string? idCellValue = worksheet.Cells[rows, 1].Value?.ToString();
                if (rows > 1)
                    idCellValue = (int.Parse(idCellValue)).ToString();
                else
                    idCellValue = "1";
                return idCellValue;
            }
        }
        private void DisplayBomb(double x, double y)
        {
            notification = true;
            for (int i = 0; i< 5; i++)
            {
                Mineral mineral = new Mineral([id], "", "", CollectMaterial(new MapObject("D:\\OOP-custom-project\\Bomb_miner_removeBG\\", new Rectangle() { X = x, Y = y, Width = 200, Height = 200 }, 49, 0.04, definezone.AssignMineral(x - 100, y - 100))), new List<Point2D>());
                obtainedmineral.Add(mineral);
                _game.bag.MineralBag.Inventory.Put(mineral);
            }
            int _currentFrame = 0;
            DateTime _lastFrameTime = DateTime.Now;
            double _frameDuration = 0.04;
            while(_currentFrame < 49)
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();


                Bitmap mapImage = new Bitmap("Background", @"D:\OOP-custom-project\Image\background.png");
                SplashKit.DrawBitmap(mapImage, _offsetX - 120, _offsetY - 120, SplashKit.OptionScaleBmp(2.2, 1.5));
                SplashKit.DrawBitmapOnBitmap(_mapImage, new Bitmap("ToolboxMining", @"D:\OOP-custom-project\Mining_removedbg2\frame_00_delay-0.04s.png"), 2000, 800);
                SplashKit.DrawBitmap(_mapImage, _offsetX, _offsetY, SplashKit.OptionScaleBmp(_zoom, _zoom));

                if (_game.window.CloseRequested)
                    break;

                Bitmap bomb = new Bitmap("bomb", 500, 100);
                bomb.Clear(Color.Black);
                MapObject obj = new MapObject("D:\\OOP-custom-project\\Bomb_miner_removeBG\\", new Rectangle() { X = 250, Y = 50, Width = 200, Height = 200 }, 49, 0.04, definezone.AssignMineral(x - 100, y - 100));

                // Calculate time elapsed and update the frame
                DateTime now = DateTime.Now;
                double elapsedSeconds = (now - _lastFrameTime).TotalSeconds;

                if (elapsedSeconds >= _frameDuration)
                {
                    _currentFrame++;
                    _lastFrameTime = now;
                }
                ShowObjectGifFrames(_game.window, obj, _currentFrame, bomb);
                SplashKit.DrawBitmap(bomb, 250, 300, SplashKit.OptionScaleBmp(2, 2));
                bomb.Free();
                SplashKit.RefreshScreen(120);
            }
        }
        private Bitmap Displayexamine(double x, double y)
        {
            Bitmap obtained = new Bitmap("obtained", @"D:\OOP-custom-project\Image\ObtainFrame.png");
            Bitmap baseimg = new Bitmap("examine", obtained.Width, obtained.Height + 20);
            baseimg.Clear(Color.RGBAColor(255, 165, 0, 64));
            SplashKit.DrawBitmapOnBitmap(baseimg, obtained, 0, 0);
            SplashKit.DrawTextOnBitmap(baseimg, "This place you can mine", Color.WhiteSmoke, "Arial", 0, 50, 30);
            SplashKit.DrawTextOnBitmap(baseimg, definezone.AssignMineral(x, y)._name, Color.WhiteSmoke, "Arial", 0, 30, 60);
            string[] description = SplitParagraph(definezone.AssignMineral(x, y)._description, 22);
            for (int i = 0; i < description.Count(); i++)
                SplashKit.DrawTextOnBitmap(baseimg, description[i], Color.WhiteSmoke, "Arial", 0, 30, 80 + i * 10);
            SplashKit.DrawTextOnBitmap(baseimg, "Click anywhere to continue", Color.WhiteSmoke, "Arial", 40, 40, baseimg.Height - 20);
            return baseimg;
        }
        public string[] SplitParagraph(string paragraph, int maxLength)
        {
            string[] words = paragraph.Split(' ');
            int maxLines = (int)Math.Ceiling((double)paragraph.Length / maxLength);
            string[] lines = new string[maxLines];

            string currentLine = "";
            int lineIndex = 0;

            foreach (string word in words)
            {
                if (currentLine.Length + word.Length + 1 <= maxLength)
                {
                    currentLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                }
                else
                {
                    lines[lineIndex++] = currentLine;
                    currentLine = word;
                }
            }

            // Resize the array to remove unused elements
            Array.Resize(ref lines, lineIndex);

            return lines;
        }
    }
}
