using SplashKitSDK;
using System.Linq.Expressions;

namespace OOP_custom_project
{
    public class MainScreen : IAmAScreen
    {
        private Game _game;
        private bool tutorial = true;
        private int scriptcount = 0;
        private string[] script = new string[]
        {
            "Hephaestus: Welcome to the world of forging, young adventurer!",
            "Hephaestus: I am Hephaestus, the god of forging. It's an honor to guide you on this journey.",
            "Hephaestus: In this world, we transform raw materials into magnificent weapons and tools.",
            "Hephaestus: This is the map icon. Use it to start exploring the world and discovering new resources.",
            "Hephaestus: This is the bag icon. Here you can view and manage your inventory of materials and items.",
            "Hephaestus: This is the forging icon. Click it to begin crafting weapons that will aid you in your adventures.",
            "Hephaestus: Remember, the quality of your creations depends on the materials you choose and your skill in forging.",
            "Hephaestus: Experiment with different combinations of minerals to discover powerful and unique weapons.",
            "Hephaestus: Here's where you see your quest about what you need to do.",
            "Hephaestus: Don't be afraid to make mistakes. Each failure is a step towards mastery.",
            "Hephaestus: Now, go forth and unleash your inner blacksmith! The world awaits your creations.",
            "Hephaestus: Good luck on your journey."
        };
        public MainScreen(Game game) 
        {
            _game = game;
        }
        public void Draw()
        {
            //draw main theme and icons
            SplashKit.DrawBitmap(new Bitmap("background", @"D:\OOP-custom-project\Image\Main-theme-background.png"), 0, 0);
            SplashKit.DrawBitmap(new Bitmap("map", @"D:\OOP-custom-project\Image\Game_map_icon.png"), 0, -45, SplashKit.OptionScaleBmp(0.5, 0.5));
            SplashKit.DrawBitmap(new Bitmap("bag", @"D:\OOP-custom-project\Image\bag_icon.png"), 100, -63, SplashKit.OptionScaleBmp(0.5, 0.5));
            SplashKit.DrawBitmap(new Bitmap("forging", @"D:\OOP-custom-project\Image\Forging_icon.webp"), 70, -200, SplashKit.OptionScaleBmp(0.2, 0.2));
            SplashKit.DrawBitmap(new Bitmap("quest", @"D:\OOP-custom-project\Image\Quest-icon.png"), 200, -195, SplashKit.OptionScaleBmp(0.22, 0.22));

            //draw Hephaestus and dialog
            if (tutorial)
            {
                Bitmap Hephaestus = new Bitmap("Hepatheus", @"D:\OOP-custom-project\Image\Hephaestus.png");
                SplashKit.DrawBitmap(Hephaestus, -250, -250, SplashKit.OptionScaleBmp(0.4, 0.4));
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    scriptcount++;
                    if (scriptcount == script.Length)
                    {
                        tutorial = false;
                    }
                }
                if (scriptcount != script.Length)
                    DrawDialog(scriptcount);
                Bitmap focus = new Bitmap("focus", @"D:\OOP-custom-project\Image\focus.png");
                switch (scriptcount)
                {
                    case 3:
                        SplashKit.DrawBitmap(focus, -140, -195, SplashKit.OptionScaleBmp(0.25,0.25));
                        break;
                    case 4:
                        SplashKit.DrawBitmap(focus, -30, -195, SplashKit.OptionScaleBmp(0.25, 0.25));
                        break;
                    case 5:
                        SplashKit.DrawBitmap(focus, 90, -195, SplashKit.OptionScaleBmp(0.25, 0.25));
                        break;
                    case 8:
                        SplashKit.DrawBitmap(focus, 210, -195, SplashKit.OptionScaleBmp(0.25, 0.25));
                        break;
                }
            }
            else
            {
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 50, 10, 100, 100))
                    {
                        _game.ChangeScreen("map");
                    }
                    if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 160, 10, 100, 100))
                    {
                        _game.ChangeScreen("bag");
                    }
                    if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 280, 10, 100, 100))
                    {
                        _game.ChangeScreen("forging");
                    }
                    if (SplashKit.PointInRectangle(SplashKit.MouseX(), SplashKit.MouseY(), 400, 10, 100, 100))
                    {
                        _game.ChangeScreen("quest");
                    }
                }
            }
        }
        private void DrawDialog(int count)
        {
            Bitmap bitmap = new Bitmap("dialog", 700, 150);
            bitmap.Clear(Color.RGBAColor(128, 128, 128, 100));

            string[] eachtext = script[count].Split('.', '!');
            for (int i = 0; i < eachtext.Length; i++)
            {
                SplashKit.DrawTextOnBitmap(bitmap, eachtext[i], Color.Black, "Arial", 50, 50, 10 + 10*i);
            }
            SplashKit.DrawTextOnBitmap(bitmap, "Click anywhere to continue", Color.Black, "Arial", 50, 200, 120);

            SplashKit.DrawBitmap(bitmap, 150, 520, SplashKit.OptionScaleBmp(1.5,1.5));
            bitmap.Dispose();
        }
    }
}
