namespace Cobalt.Enums
{
    public enum TFClass
    {
        None, Scout, Soldier, Pyro,
        Demoman, Engineer, HeavyWeapons,
        Medic, Spy, Sniper
    }

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
        VaccinatorBullets, VaccinatorBlast, VaccinatorFire, ProjectileShield
    }

    public enum TFBotSkill //BOT SKILL LEVEL
    {
        Easy, Normal, Hard, Expert
    }

    public enum TFBotWeaponRestrictions //BOT WEAPON RESTRICTION
    {
        None, PrimaryOnly, SecondaryOnly, MeleeOnly
    }
}
