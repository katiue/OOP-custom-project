using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace OOP_custom_project
{
    public class MapObject
    {
        public readonly Bitmap[] _frames;
        public string Name { get; set; }
        public Rectangle Area { get; set; }

        public MapObject(string name, Rectangle area, int frameCount, double frameDuration, MineralType mineral)
        {
            Name = name;
            Area = area;
            _frames = LoadGifFrames(name, frameCount, frameDuration);
        }
        private Bitmap[] LoadGifFrames(string baseName, int frameCount, double frameDuration)
        {
            Bitmap[] frames = new Bitmap[frameCount];
            if(frameCount < 10 )
            {
                for (int i = 0; i < frameCount; i++)
                {
                    string frameName;
                    frameName = $"{baseName}\\frame_{i}_delay-{frameDuration}s.png"; // Assuming frames are saved as PNG files
                    frames[i] = new Bitmap(frameName, frameName);

                    if (frames[i] == null)
                    {
                        Console.WriteLine($"Failed to load frame: {frameName}");
                        throw new Exception($"Failed to load frame: {frameName}");
                    }
                }
            }
            else if (frameCount < 100)
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
    }
}
