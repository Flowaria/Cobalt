using Cobalt.Data;
using System.Collections.Generic;

namespace Cobalt.TFItems
{
    public class TFAttribute
    {
        //Static
        private static List<TFAttribute> m_Attri = new List<TFAttribute>();

        public static bool AddAttribute(TFAttribute item)
        {
            if (!m_Attri.Exists(x => x.DefId == item.DefId))
            {
                m_Attri.Add(item);
                return false;
            }
            return false;
        }

        public static void RemoveItembyId(int id)
        {
            m_Attri.RemoveAll(x => x.DefId == id);
        }

        public static TFAttribute GetItembyId(int id)
        {
            return m_Attri.Find(x => x.DefId == id);
        }

        public static TFAttribute GetItembyName(string name)
        {
            return m_Attri.Find(x => x.DefName.Equals(name));
        }

        public static TFAttribute[] AttributesList()
        {
            TFAttribute[] att = new TFAttribute[m_Attri.Count];
            m_Attri.CopyTo(att);
            return att;
        }

        //Dynamic

        private int m_DefId;
        private string m_DefName;

        public int DefId
        {
            get { return m_DefId; }
            set { m_DefId = value; }
        }

        public string DefName
        {
            get { return m_DefName; }
            set { m_DefName = value; }
        }
    }
}
