using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowaria.Population.Element
{
    public class TFBot
    {
        public enum TFClass
        {
            None, Scout, Soldier, Pyro,
            Demoman, Engineer, HeavyWeapons,
            Medic, Spy, Sniper
        }

        [Flags]
        public enum TFBotAttribute //BOT ATTRIBUTE
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
            VaccinatorBullets,
            VaccinatorBlast,
            VaccinatorFire,
            ProjectileShield
        }

        public enum TFBotSkill //BOT SKILL LEVEL
        {
            Easy, Normal, Hard, Expert
        }

        public enum TFBotWeaponRestrictions //BOT WEAPON RESTRICTION
        {
            None, PrimaryOnly, SecondaryOnly, MeleeOnly
        }

        public int BotAttribute;
        public Attributes BodyAttributes;

        public TFBot()
        {
            BodyAttributes.Set(32, 1.0f);
        }
    }
}
