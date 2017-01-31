using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cobalt.Config
{
    public static class MapConfig
    {
        public static List<MapCFG> Maps;
        public static void loadConfig(string name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Properties.Settings.Default.PATH_CFG + "config.xml");

            XmlElement rNode = doc.DocumentElement;
            XmlNodeList NodeApi = rNode.GetElementsByTagName("API");
            XmlNodeList NodeMessage = rNode.GetElementsByTagName("MESSAGE");
        }

        public static MapCFG getMaps(string name)
        {
            MapCFG cfg = Maps.Find(x => x.MapName.Equals(name));
            if (cfg != null) return cfg;
            return null;
        }
    }

    public class MapCFG
    {
        public string MapName { get; set; }
        public string Name { get; set; }

        public List<string> Where { get; set; }

        public List<string> TankPath { get; set; }

        public List<NavigationTag> TagNav { get; set; }
        public List<string> TagBot { get; set; }
    }

    public enum NavType
    {
        Prefer, Avoid, Blocker
    }

    public class NavigationTag
    {

    }
}
