using Cobalt.Enums;
using System.Collections.Generic;

namespace Cobalt.TFItems
{
    public class TFItem
    {
        //Static
        private static List<TFItem> m_Items = new List<TFItem>();

        public static bool AddItem(TFItem item)
        {
            if(!m_Items.Exists(x => x.DefId == item.DefId))
            {
                m_Items.Add(item);
                return false;
            }
            return false;
        }

        public static void RemoveItembyId(int id)
        {
            m_Items.RemoveAll(x => x.DefId == id);
        }

        public static TFItem GetItembyId(int id)
        {
            return m_Items.Find(x => x.DefId == id);
        }

        public static TFItem[] ItemList()
        {
            TFItem[] items = new TFItem[m_Items.Count];
            m_Items.CopyTo(items);
            return items;
        }

        //Dynamic
        private int m_DefId;
        private string m_Classname, m_DefName, m_LocalName, m_ImageURL;
        private ItemQuality m_Quality;
        private ItemSlot m_Itemslot;

        public int DefId
        {
            get { return m_DefId; }
            set { m_DefId = value; }
        }

        public string Classname
        {
            get { return m_Classname; }
            set { m_Classname = value; }
        }

        public string DefName
        {
            get{return m_DefName;}
            set{m_DefName = value;}
        }

        public string LocalName
        {
            get { return m_LocalName; }
            set { m_LocalName = value; }
        }

        public string ImageURL
        {
            get { return m_ImageURL; }
            set { m_ImageURL = value; }
        }
        public ItemQuality Quality
        {
            get { return m_Quality; }
            set { m_Quality = value; }
        }
        public ItemSlot ItemSlot
        {
            get { return m_Itemslot; }
            set { m_Itemslot = value; }
        }

        public TFAttribute[] Attribute;
        public double[] AttributeValue;

        public TFItem(int defid = -1, string cname = null, string name = null, string local_name = null, string img_url = null, ItemSlot slot = ItemSlot.Misc, ItemQuality quality = ItemQuality.Normal)
        {
            DefId = defid;
            Classname = cname;
            DefName = name;
            LocalName = local_name;
            ImageURL = img_url;
            ItemSlot = slot;
            Quality = quality;
            Attribute = new TFAttribute[15];
            AttributeValue = new double[15];
        }

        private short AttributeIndex;
        public void addAttribute(TFAttribute attribute, double value)
        {
            Attribute[AttributeIndex] = attribute;
            AttributeValue[AttributeIndex] = value;
            AttributeIndex++;
        }

        public void setAttribute(int index, TFAttribute attribute, double value)
        {
            Attribute[index] = attribute;
            AttributeValue[index] = value;
        }

        public void overrideAttribute(params TFAttribute[] attr)
        {

        }
    }
}
