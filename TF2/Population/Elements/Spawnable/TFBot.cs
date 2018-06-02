using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Population.Element
{
    public class TFBot : Spawnable
    {
        public int BotAttribute;
        public Attributes BodyAttributes;

        public TFBot()
        {
            BodyAttributes.Set(32, 1.0f);
        }
    }
}
