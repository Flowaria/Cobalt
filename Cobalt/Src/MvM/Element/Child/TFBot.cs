using Cobalt.MvM.Items;

namespace Cobalt.MvM.Element
{
    public class TFBot : Child
    {
        //ENUM
        public enum TFClass
        {
            None, Scout, Soldier, Pyro,
            Demoman, Engineer, HeavyWeapons,
            Medic, Spy, Sniper
        }
        public enum TFBotAttribute
        {
            None,
            SpawnWithFullCharge,
            AlwaysCrit,
            AlwaysFireWeapon,
            MiniBoss,
            UseBossHealthBar,
            HoldFireUntilFullReload,
            IgnoreFlag,
            TeleportToHint,
            AutoJump,
            AirChargeOnly,
            Parachute,
            VaccinatorBullets, VaccinatorBlast, VaccinatorFire, ProjectileShield
        }

        public string BaseTemplate = null;
        public string BaseTemplateFile = null;

        public enum TFSkill { Easy, Normal, Hard, Expert } //BOT SKILL LEVEL
        public enum TFWeaponRestrictions { None, PrimaryOnly, SecondaryOnly, MeleeOnly }

        public TFClass Class = TFClass.None;
        public TFBotAttribute[] Attributes;
        public TFSkill Skill = TFSkill.Normal;
        public TFWeaponRestrictions WeaponRestrict = TFWeaponRestrictions.None;

        public short AutoJumpMin = -1;
        public short AutoJumpMax = -1;

        public TFBot RevertGateBotsBehavior = null;

        public int MaxVisionRange = -1;
        public double Scale = 1.0;

        public TFItem[] Items;
        public TFAttribute[] BodyAttributes;

        public TFBot()
        {

        }
    }
}
