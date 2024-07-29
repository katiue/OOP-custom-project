namespace OOP_custom_project
{
    public class MineralZone(string[] ids, string name, string desc, MineralType mineral, int quantity, double startX, double startY, double endX, double endY) : GameObject(ids, name, desc)
    {
        public MineralType _mineral = mineral;
        public int Max_quantity = quantity;
        public double startX = startX;
        public double startY = startY;
        public double endX = endX;
        public double endY = endY;

        public void ReFill()
        {
            Max_quantity = _mineral.MaxQuantity;
        }

    }
}
