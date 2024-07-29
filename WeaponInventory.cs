namespace OOP_custom_project
{
    public class WeaponInventory
    {
        public WeaponInventory()
        {
            WeaponList = [];
        }
        public void Put(Weapon itm)
        {
            WeaponList.Add(itm);
        }
        public void Put(List<Weapon> itm)
        {
            foreach (var i in itm)
            {
                WeaponList.Add(i);
            }
        }
        public Weapon Take(string id)
        {
            Weapon takenItem = Fetch(id);
            WeaponList.Remove(takenItem);
            return takenItem;
        }
        public Weapon Fetch(string id)
        {
            foreach (Weapon i in WeaponList)
            {
                if (i.AreYou(id))
                {
                    return i;
                }
            }
            return null;
        }
        public List<Weapon> WeaponList { get; }
    }
}
