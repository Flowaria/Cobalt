using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cobalt.Population
{
    public enum RelayType
    {

    }

    public class Relay
    {
        public Relay(RelayType type, string target, string action)
        {
            Type = type;
            Target = target;
            Action = action;
        }

        private RelayType m_type;
        private string m_target, m_action;

        public RelayType Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        public string Target
        {
            get { return m_target; }
            set { m_target = value; }
        }

        public string Action
        {
            get { return m_action; }
            set { m_action = value; }
        }

        public string toString()
        {
            try
            {
                return String.Format("{0}:{1}:{2}", Enum.GetName(typeof(RelayType), Type), Target, Action);
            }
            catch
            {
                return null;
            }
        }

        public static Relay parse(string input)
        {
            try
            {
                var sp = input.Split(':');
                if(sp.Count() < 3) //인자 3개 미만일시
                {
                    throw new FormatException();
                }

                RelayType rtype;
                if (Enum.TryParse(sp[0], true, out rtype))
                {
                    return new Relay(rtype, sp[1], sp[2]);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
