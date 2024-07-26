namespace OOP_custom_project
{
    public class MineralZone : GameObject
    {
        public MineralType _mineral;
        public int Max_quantity;
        public double startX;
        public double startY;
        public double endX;
        public double endY;

        public MineralZone(string[] ids, string name, string desc, MineralType mineral, int quantity, double startX, double startY, double endX, double endY) : base(ids, name, desc)
        {
            _mineral = mineral;
            Max_quantity = quantity;
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
        }

        public void ReFill()
        {
            Max_quantity = _mineral._maxquantity;
        }
    }
}
