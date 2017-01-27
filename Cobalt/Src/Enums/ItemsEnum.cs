namespace Cobalt.Enums
{
    public enum ItemSlot
    {
        Primary, Secondary, Melee,
        Pda1, Pda2, Building, Misc, Action
    }
    public enum ItemQuality
    {
        Normal = 0,
        Genuine = 1,
        Rarity2 = 2,
        Vintage = 3,
        Rarity3 = 4,
        Unusual = 5,
        Unique = 6,
        Community = 7,
        Developer = 8,
        Selfmade = 9,
        Customized = 10,
        Strange = 11,
        Completed = 12,
        Haunted = 13,
        Collectors = 14,
        paintkitweapon = 15
    }
    public enum EffectSide
    {
        Positive,
        Negative,
        Neutral
    }

    public enum DescriptionFormat
    {
        Percentage,
        Inverted_Percentage,
        Additive,
        Additive_Percentage,
        
        From_Lookup_Table,
        Date,
        Or,

        Particle_Index,
        KillstreakEffect_Index,
        Killstreak_IdleEffect_Index
    }
}
