using SplashKitSDK;

namespace OOP_custom_project
{
    public class Game
    {

        private string current_screen_type = "starting";
        public Bag bag;
        public Window window = new Window("Game screen", 1000, 700);
        public WeaponForging forging;
        public MainScreen mainScreen;
        public Game() 
        {
            mainScreen = new MainScreen(this);
            bag = new Bag(this);
            forging = new WeaponForging(this);
        }
        public void Run()
        {
            InteractiveMap map = new InteractiveMap(window, this);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                switch (current_screen_type)
                {
                    case "starting":
                        mainScreen.Draw();
                        break;
                    case "map":
                        map.Draw();
                        break;
                    case "bag":
                        bag.Draw();
                        break;
                    case "forging":
                        forging.Draw();
                        break;
                }
                SplashKit.RefreshScreen(120);
            } while (!window.CloseRequested);
            bag.SaveFile();
        }
        public void ChangeScreen(string new_screen_type)
        {
            current_screen_type = new_screen_type;
        }
    }
}
