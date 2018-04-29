using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TF2.Info;

namespace Flowaria.ItemSchema
{
    public class TFAttribute
    {
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
}
