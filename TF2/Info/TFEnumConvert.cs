using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Info
{
    public class TFEnumConvert
    {
        public static TFClass StringToTFClass(string str)
        {
            switch (str.ToLower())
            {
                case "scout":
                    return TFClass.Scout;
                case "soldier":
                    return TFClass.Soldier;
                case "pyro":
                    return TFClass.Pyro;
                case "engineer":
                case "engi":
                    return TFClass.Engineer;
                case "heavyweapons":
                case "heavy":
                    return TFClass.Heavy;
                case "demoman":
                case "demo":
                    return TFClass.Demoman;
                case "medic":
                    return TFClass.Medic;
                case "sniper":
                    return TFClass.Sniper;
                case "spy":
                    return TFClass.Spy;
                default:
                    return TFClass.None;
            }
        }
        public static TFItemSlot StringToSlot(string str)
        {
            switch (str.ToLower())
            {
                case "primary":
                    return TFItemSlot.Primary;
                case "secondary":
                    return TFItemSlot.Secondary;
                case "melee":
                    return TFItemSlot.Melee;
                case "pda":
                    return TFItemSlot.PDA1;
                case "pda2":
                    return TFItemSlot.PDA2;
                case "building":
                    return TFItemSlot.Building;
                case "misc":
                    return TFItemSlot.Misc;
                default:
                    return TFItemSlot.None;
            }
        }
    }
    
}
