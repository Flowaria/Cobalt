using Cobalt.TFItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Data
{
    //Static HELL
    public static class ItemsData
    {
        public static List<TFItem> Items = new List<TFItem>();
        public static List<TFAttribute> Attributes = new List<TFAttribute>();

        public static List<TFAttribute> FindAttributesByName(string name)
        {
            return Attributes.FindAll(x => x.Name.Contains(name));
        }

        public static TFAttribute FindAttributeById(int id)
        {
            return Attributes.Find(x => x.DefId == id);
        }

        public static List<TFItem> FindItemsByName(string name)
        {
            return Items.FindAll(x => x.DefName.Contains(name) || x.Name.Contains(name));
        }

        public static TFItem FindItemById(int id)
        {
            return Items.Find(x => x.DefId == id);
        }
    }
}
