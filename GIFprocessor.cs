using SplashKitSDK;
using System;
using System.Diagnostics.Metrics;

namespace OOP_custom_project
{
    public class GIFprocessor
    {
        private DateTime _lastFrameTime;
        private int _currentFrame;
        private readonly double _frameDuration; // Duration for each frame in seconds
        private readonly Bitmap[] _frames;

        public GIFprocessor(string baseName, int frameCount, double frameDuration)
        {
            _frameDuration = frameDuration;
            _currentFrame = 0;
            _lastFrameTime = DateTime.Now;
            _frames = LoadGifFrames(baseName, frameCount, frameDuration);
        }

        private static Bitmap[] LoadGifFrames(string baseName, int frameCount, double frameDuration)
        {
            Bitmap[] frames = new Bitmap[frameCount];
            if(frameCount<100)
            {
                for (int i = 0; i < frameCount; i++)
                {
                    string frameName;
                    if (i < 10)
                        frameName = $"{baseName}\\frame_0{i}_delay-{frameDuration}s.png";
                    else
                        frameName = $"{baseName}\\frame_{i}_delay-{frameDuration}s.png"; // Assuming frames are saved as PNG files
                    frames[i] = new Bitmap(frameName, frameName);
                    if (frames[i] == null)
                    {
                        Console.WriteLine($"Failed to load frame: {frameName}");
                        throw new Exception($"Failed to load frame: {frameName}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < frameCount; i++)
                {
                    string frameName;
                    if (i < 10)
                        frameName = $"{baseName}\\frame_00{i}_delay-{frameDuration}s.png";
                    else if (i < 100)
                        frameName = $"{baseName}\\frame_0{i}_delay-{frameDuration}s.png";
                    else
                        frameName = $"{baseName}\\frame_{i}_delay-{frameDuration}s.png"; // Assuming frames are saved as PNG files
                    frames[i] = new Bitmap(frameName, frameName);
                    if (frames[i] == null)
                    {
                        Console.WriteLine($"Failed to load frame: {frameName}");
                        throw new Exception($"Failed to load frame: {frameName}");
                    }
                }
            }
            return frames;
        }

        public void ShowGifFrames(Window window)
        {
            while(_currentFrame < _frames.Length-1)
            {
                if (window.CloseRequested)
                    break;
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                // Calculate time elapsed and update the frame
                DateTime now = DateTime.Now;
                double elapsedSeconds = (now - _lastFrameTime).TotalSeconds;

                if (elapsedSeconds >= _frameDuration)
                {
                    _currentFrame++;
                    _lastFrameTime = now;
                }
                // Get the dimensions of the window
                int windowWidth = window.Width;
                int windowHeight = window.Height;

                // Get the dimensions of the current frame
                Bitmap currentFrame = _frames[_currentFrame];
                int frameWidth = currentFrame.Width;
                int frameHeight = currentFrame.Height;

                // Calculate scaling factors
                double scaleX = (double)windowWidth / frameWidth;
                double scaleY = (double)windowHeight / frameHeight;

                //calculate picture position
                int posX = (windowWidth - frameWidth) / 2;
                int posY = (windowHeight - frameHeight) / 2;

                // Draw the current frame scaled to fit the window
                SplashKit.DrawBitmap(currentFrame, posX, posY, SplashKit.OptionScaleBmp(scaleX,scaleY));
                SplashKit.RefreshScreen(120);
            }
        }
    }
}
