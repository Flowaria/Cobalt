using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Info
{
    public class TFSlotFunction
    {
        public static TFSlot StringToSlot(string str)
        {
            switch (str.ToLower())
            {
                case "primary":
                    return TFSlot.Primary;
                case "secondary":
                    return TFSlot.Secondary;
                case "melee":
                    return TFSlot.Melee;
                case "pda":
                    return TFSlot.PDA1;
                case "pda2":
                    return TFSlot.PDA2;
                case "building":
                    return TFSlot.Building;
                case "misc":
                    return TFSlot.Misc;
                default:
                    return TFSlot.Null;
            }
        }
    }

    public enum TFSlot
    {
        Null,
        Primary,
        Secondary,
        Melee,
        PDA1,
        PDA2,
        Building,
        Misc
    }
}
