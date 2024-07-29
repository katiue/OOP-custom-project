namespace OOP_custom_project
{
    public class MineralInventory
    {
        public MineralInventory()
        {
            Mineral = [];
        }
        public void Put(Mineral itm)
        {
            Mineral.Add(itm);
        }
        public void Put(List<Mineral> itm)
        {
            foreach (var i in itm)
            {
                Mineral.Add(i);
            }
        }
        public Mineral Take(string id)
        {
            Mineral takenItem = Fetch(id);
            Mineral.Remove(takenItem);
            return takenItem;
        }
        public Mineral Fetch(string id)
        {
            foreach (var i in Mineral)
            {
                if (i.AreYou(id))
                {
                    return i;
                }
            }
            return null;
        }
        public List<Mineral> Mineral { get; }
    }
}
