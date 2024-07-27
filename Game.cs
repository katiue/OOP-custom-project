using SplashKitSDK;

namespace OOP_custom_project
{
    public class Game
    {

        private string current_screen_type = "starting";
        public Window window = new Window("Game screen", 1000, 700);
        public Bag bag;
        private WeaponForging forging;
        private MainScreen mainScreen;
        private Quest quest;
        
        public Game() 
        {
            mainScreen = new MainScreen(this);
            bag = new Bag(this);
            forging = new WeaponForging(this);
            quest = new Quest(this);
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
