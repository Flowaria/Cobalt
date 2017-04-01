using Cobalt.Data;
using Cobalt.Enums;
using Cobalt.TFItems;
using System.Collections.Generic;

namespace Cobalt.Population.Element
{
    public class TFBot
    {
        public long Health;
        public string Name;
        public string ClassIcon;

        public string BaseTemplate = null;
        public string BaseTemplateFile = null;

        public TFClass Class = TFClass.Scout;
        public TFBotAttribute Attributes = TFBotAttribute.None;
        public TFBotSkill Skill = TFBotSkill.Normal;
        public TFBotWeaponRestrictions WeaponRestrict = TFBotWeaponRestrictions.None;

        public short AutoJumpMin = -1;
        public short AutoJumpMax = -1;

        public TFBot RevertGateBotsBehavior = null;

        public int MaxVisionRange = -1;
        public double Scale = 1.0;

        public List<TFItem> Items;
        public List<TFAttribute> BodyAttributes;

        public TFBot()
        {

        }

        public void AutoGenerateGatebot()
        {
            int i = AddGateBotHat();
            var Gatebot = this.MemberwiseClone() as TFBot;
            Gatebot.Items.Find(x => x.DefId == 1).addAttribute(TFAttribute.GetItembyId(542), 1.4);
            this.RevertGateBotsBehavior = Gatebot;
        }

        public int AddItem(int id)
        {
            TFItem item = TFItem.GetItembyId(id);
            if(item != null && !HasItem(id))
            {
                Items.Add(item);
                return id;
            }
            return -1;
        }

        public void RemoveItem(int id)
        {
            if (HasItem(id))
            {
                Items.RemoveAll(x => x.DefId == id);
            }
        }

        public bool HasItem(int id)
        {
            return Items.Exists(x => x.DefId == id);
        }

        public int AddGateBotHat()
        {
            int i = -1;
            switch (Class)
            {
                case TFClass.Scout:
                    i = 1057;
                    break;
                case TFClass.Pyro:
                    i = 1058;
                    break;
                case TFClass.Soldier:
                    i = 1063;
                    break;

                case TFClass.Demoman:
                    i = 1061;
                    break;
                case TFClass.HeavyWeapons:
                    i = 1060;
                    break;
                case TFClass.Engineer:
                    i = 1065;
                    break;

                case TFClass.Medic:
                    i = 1059;
                    break;
                case TFClass.Sniper:
                    i = 1062;
                    break;
                case TFClass.Spy:
                    i = 1064;
                    break;
            }
            if (i > 0)
                AddItem(i);

            return i;
        }
    }
}
