using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Data
{
    public static class MapsData
    {
        private static List<TFMap> Maps;
        private static TFMap _Current;
        public static TFMap Current
        {
            get
            {
                return _Current;
            }
        }
        public static void AddMap(TFMap map)
        {
            Maps.Add(map);
        }
        public static void DeleteMap(string name)
        {
            Maps.RemoveAll(x => x.MapName.Equals(name) || x.CompileName.Equals(name));
        }
    }

    //맵 데이터
    public class TFMap
    {
        public string MapName;
        public string CompileName;
        public List<string> Where = new List<string>();
        public List<string> RelayWaveStarted = new List<string>();
        public List<string> RelayWaveDone = new List<string>();
        public List<string> RelayWaveInit = new List<string>();

        public List<string> RelayFirstSpawn = new List<string>();
        public List<string> RelayLastSpawn = new List<string>();

        public List<string> RelayTankKilled = new List<string>();
        public List<string> RelayBombDropped = new List<string>();

        public List<string> PathTank = new List<string>();
        public List<string> TagBot = new List<string>();
        public List<string> TagPrefer = new List<string>();
    }
}
