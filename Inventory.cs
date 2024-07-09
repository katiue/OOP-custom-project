using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OOP_custom_project
{
    public class Inventory
    {
        private List<Item> _items;
        public Inventory()
        {
            _items = new List<Item>();
        }
        public bool HasItem(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id))
                {
                    return true;
                }
            }
            return false;
        }
        public void Put(Item itm)
        {
            _items.Add(itm);
        }
        public Item Take(string id)
        {   
            Item takenItem = Fetch(id);
            _items.Remove(takenItem);
            return takenItem;
        }
        public Item Fetch(string id)
        {
            foreach (Item i in _items)
            {
                if (i.AreYou(id))
                {
                    return i;
                }
            }
            return null;
        }
        public string ItemList
        {
            get
            {
                string ItemList = "";
                foreach (Item i in _items)
                {
                    ItemList += i.ShortDescription + "\n";
                }
                return ItemList;
            }
        }
    }
}
