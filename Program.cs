namespace OOP_custom_project
{
    public class Program
    {
        public static void Main()
        {
            string purpose = "play";
            if (purpose == "play")
            {
                Game game = new();
                game.Run();
            }
            else if (purpose == "test")
            {
                Testing test = new();
                test.MakeMapObjectLayer();
            }
        }
    }
}
