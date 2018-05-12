using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Info
{
    public class TFClassFunction
    {
        public static TFClass StringToClass(string str)
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
                    return TFClass.Null;
            }
        }
    }
    public enum TFClass
    {
        Null = 0,
        Scout = 1,
        Soldier = 2,
        Pyro = 3,
        Heavy = 4,
        Engineer = 5,
        Demoman = 6,
        Sniper = 7,
        Spy = 8,
        Medic = 9
    }
}
