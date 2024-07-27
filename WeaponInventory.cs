namespace OOP_custom_project
{
    public class WeaponInventory
    {
        private List<Weapon> component;
        public WeaponInventory()
        {
            component = new List<Weapon>();
        }
        public bool HasItem(string id)
        {
            foreach (Weapon i in component)
            {
                if (i.AreYou(id))
                {
                    return true;
                }
            }
            return false;
        }
        public void Put(Weapon itm)
        {
            component.Add(itm);
        }
        public void Put(List<Weapon> itm)
        {
            foreach (var i in itm)
            {
                component.Add(i);
            }
        }
        public Weapon Take(string id)
        {
            Weapon takenItem = Fetch(id);
            component.Remove(takenItem);
            return takenItem;
        }
        public Weapon Fetch(string id)
        {
            foreach (Weapon i in component)
            {
                if (i.AreYou(id))
                {
                    return i;
                }
            }
            return null;
        }
        public List<Weapon> WeaponList
        {
            get
            {
                return component;
            }
        }
    }
}
