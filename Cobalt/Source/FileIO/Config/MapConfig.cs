using Cobalt.Data;
using Cobalt.Properties;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;

namespace Cobalt.FileIO.CFG
{
    public static class MapConfig
    {
        static MapConfig()
        {
            //기본맵 콘피그 추출
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
            try
            {
                doc.Load(name);

                //처음 노드 가져오기
                XmlElement rNode = doc.DocumentElement;
                Console.WriteLine(rNode.Name);

                var map = new TFMap();
                map.MapName = rNode.Name;

                //Where List
                getAllNodesData(rNode, "Where/Where", map.Where);

                //Relay Wave
                getAllNodesData(rNode, "Relay/Wave/WaveInit", map.RelayWaveInit);
                getAllNodesData(rNode, "Relay/Wave/WaveStarted", map.RelayWaveStarted);
                getAllNodesData(rNode, "Relay/Wave/WaveDone", map.RelayWaveDone);


                //Relay Spawn
                getAllNodesData(rNode, "Relay/Spawn/FirstSpawn", map.RelayFirstSpawn);
                getAllNodesData(rNode, "Relay/Spawn/LastSpawn", map.RelayLastSpawn);

                //Tank
                getAllNodesData(rNode, "Relay/Tank/Killed", map.RelayTankKilled);
                getAllNodesData(rNode, "Relay/Tank/BombDropped", map.RelayBombDropped);
                getAllNodesData(rNode, "TankPath/Path", map.PathTank);

                getAllNodesData(rNode, "Tag/Bot", map.TagBot);
                getAllNodesData(rNode, "Tag/Prefer", map.TagPrefer);

                foreach (string str in map.Where)
                {
                    Console.WriteLine(str);
                }
            }
            catch
            {
                Console.WriteLine("Error");
            }
           
        }

        private static void getAllNodesData(XmlElement root, string path, List<string> handler)
        {
            foreach (XmlNode node in root.SelectNodes(path))
            {
                if (vAttribute(node, "name"))
                {
                    handler.Add(node.Attributes["name"].Value);
                }
            }
        }

        private static bool vAttribute(XmlNode node, string attr)
        {
            return (node.Attributes != null && node.Attributes[attr] != null);
        }
    }
}
