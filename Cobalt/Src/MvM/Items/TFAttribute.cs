namespace Cobalt.MvM.Items
{
    public class TFAttribute : TFItemInstance
    {
        public enum EffectSide
        {
            Positive, Negative, Neutral
        }
        public enum DescriptionFormat
        {
            Percentage, Inverted_Percentage,
            Additive, Additive_Percentage,
            Date, Or, From_Lookup_Table, Particle_Index,
            KillstreakEffect_Index, Killstreak_IdleEffect_Index
        }
        public string Description;
        public bool Hidden;
    }
}
