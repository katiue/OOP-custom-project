namespace OOP_custom_project
{
    public class MineralInventory
    {
        private List<Mineral> mineral;
        public MineralInventory()
        {
            mineral = new List<Mineral>();
        }
        public bool HasItem(string id)
        {
            foreach (var i in mineral)
            {
                if (i.AreYou(id))
                {
                    return true;
                }
            }
            return false;
        }
        public void Put(Mineral itm)
        {
            mineral.Add(itm);
        }
        public void Put(List<Mineral> itm)
        {
            foreach (var i in itm)
            {
                mineral.Add(i);
            }
        }
        public Mineral Take(string id)
        {
            Mineral takenItem = Fetch(id);
            mineral.Remove(takenItem);
            return takenItem;
        }
        public Mineral Fetch(string id)
        {
            foreach (var i in mineral)
            {
                if (i.AreYou(id))
                {
                    return i;
                }
            }
            return null;
        }
        public List<Mineral> Mineral
        {
            get
            {
                return mineral;
            }
        }
    }
}
