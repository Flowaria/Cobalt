using Cobalt.Enums;
using Cobalt.TFItems;

namespace Cobalt.Population.Element
{
    public class TFBot : Child
    {
        public string BaseTemplate = null;
        public string BaseTemplateFile = null;

        public TFClass Class = TFClass.None;
        public TFBotAttribute Attributes = TFBotAttribute.None;
        public TFBotSkill Skill = TFBotSkill.Normal;
        public TFBotWeaponRestrictions WeaponRestrict = TFBotWeaponRestrictions.None;

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
