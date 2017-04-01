using Cobalt.Data;
using Cobalt.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Cobalt.FileIO.CFG
{
    public class MapConfig
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

        public async Task loadConfigAsync(string url)
        {
            await Task.Run(() => loadConfig(url));
        }

        public void loadConfig(string url)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(url);

                //처음 노드 가져오기
                XmlElement rNode = doc.DocumentElement;

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

                TFMap.AddMap(map);
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }

        private void getAllNodesData(XmlElement root, string path, List<string> handler)
        {
            foreach (XmlNode node in root.SelectNodes(path))
            {
                if (vAttribute(node, "name"))
                {
                    handler.Add(node.Attributes["name"].Value);
                }
            }
        }

        private bool vAttribute(XmlNode node, string attr)
        {
            return (node.Attributes != null && node.Attributes[attr] != null);
        }
    }
}
