using SplashKitSDK;

namespace OOP_custom_project
{
    public class Game
    {
        private string current_screen_type = "starting";
        private readonly WeaponForging forging;
        private readonly MainScreen mainScreen;
        private readonly Quest quest;
        private readonly InteractiveMap map;
        public Window window = new("Game screen", 1000, 700);
        public Bag bag;

        public Game() 
        {
            mainScreen = new MainScreen(this);
            bag = new Bag(this);
            forging = new WeaponForging(this);
            quest = new Quest(this);
            map = new InteractiveMap(this);
        }
        public void Run()
        {
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
                    case "quest":
                        quest.Draw();
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
