using Cobalt.Enums;

namespace Cobalt.TFItems
{
    public class TFItem : TFItemInstance
    {
        private string m_DefName, m_Classname, m_ImageURL;
        private ItemQuality m_Quality;
        private ItemSlot m_Itemslot;
        public string DefName
        {
            get{return m_DefName;}
            set{m_DefName = value;}
        }
        public string Classname
        {
            get { return m_Classname; }
            set { m_Classname = value; }
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
        public float[] AttributeValue;

        public TFItem()
        {
            Name = "Bat";
            DefId = 0;
            Quality = 0;
            ItemSlot = ItemSlot.Melee;
            DefName = "TF_WEAPON_BAT";
            Classname = "tf_weapon_bat";
            ImageURL = "http://media.steampowered.com/apps/440/icons/c_bat.png";
            Attribute = new TFAttribute[15];
            AttributeValue = new float[15];
        }

        private short AttributeIndex;
        public void addAttribute(TFAttribute attribute, float value)
        {
            Attribute[AttributeIndex] = attribute;
            AttributeValue[AttributeIndex] = value;
            AttributeIndex++;
        }

        public void setAttribute(int index, TFAttribute attribute, float value)
        {
            Attribute[index] = attribute;
            AttributeValue[index] = value;
        }
    }
}
