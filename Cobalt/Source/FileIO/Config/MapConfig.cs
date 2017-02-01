using Cobalt.Properties;
using System.Collections.Generic;
using System.Xml;

namespace Cobalt.FileIO.CFG
{
    public static class MapConfig
    {
        public static List<MapCFG> Maps;

        static MapConfig()
        {
            FileFunction.ExportString(Resources.mvm_decoy, Settings.Default.PATH_CFG_MAP, "mvm_decoy.xml");
            FileFunction.ExportString(Resources.mvm_coaltown, Settings.Default.PATH_CFG_MAP, "mvm_coaltown.xml");
            FileFunction.ExportString(Resources.mvm_mannworks, Settings.Default.PATH_CFG_MAP, "mvm_mannworks.xml");
            FileFunction.ExportString(Resources.mvm_ghost_town, Settings.Default.PATH_CFG_MAP, "mvm_ghost_town.xml");
            FileFunction.ExportString(Resources.mvm_bigrock, Settings.Default.PATH_CFG_MAP, "mvm_bigrock.xml");
            FileFunction.ExportString(Resources.mvm_rottenburg, Settings.Default.PATH_CFG_MAP, "mvm_rottenburg.xml");
            FileFunction.ExportString(Resources.mvm_mannhattan, Settings.Default.PATH_CFG_MAP, "mvm_mannhattan.xml");
        }

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
