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

        public MapObject(string name, Rectangle area, int frameCount, double frameDuration)
        {
            Name = name;
            Area = area;
            _frames = LoadGifFrames(name, frameCount, frameDuration);
        }
        private static Bitmap[] LoadGifFrames(string baseName, int frameCount, double frameDuration)
        {
            Bitmap[] frames = new Bitmap[frameCount];
            if (frameCount < 100)
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
        public int[,] CollectMaterial()
        {
            int[,] matrix = new int[1000, 700];
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 700; j++)
                {
                    matrix[i, j] = random.Next(2); // Generates random 0 or 1
                }
            }

            return matrix;
        }
    }
}
