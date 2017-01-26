namespace Cobalt.MvM.Items
{
    public class TFItem : TFItemInstance
    {
        public enum ItemSlot
        {
            Primary, Secondary, Melee,
            Pda1, Pda2, Building, Misc, Action
        }

        public string DefName, Classname, ImageURL;
        public int Quality;
        public TFAttribute[] Attribute;
        public float[] AttributeValue;
        private short AttributeIndex;
        public ItemSlot Itemslot;

        public TFItem()
        {
            Name = "Bat";
            DefId = 0;
            Quality = 0;
            Itemslot = ItemSlot.Melee;
            DefName = "TF_WEAPON_BAT";
            Classname = "tf_weapon_bat";
            ImageURL = "http://media.steampowered.com/apps/440/icons/c_bat.png";
            Attribute = new TFAttribute[15];
            AttributeValue = new float[15];
        }

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
