using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Data
{
    public class TFMap
    {
        public static event EventHandler<MapChangeEventArgs> MapChange = delegate { };

        private static List<TFMap> Maps = new List<TFMap>();
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

        public static bool SetCurrentMap(string name)
        {
            var i = Maps.Find(x => x.MapName.Equals(name));
            if (i != null && (_Current == null || !i.MapName.Equals(_Current.MapName)))
            {
                _Current = i;
                MapChange(null, new MapChangeEventArgs(i));
                return true;
            }
            return false;
        }

        public static TFMap[] GetMaps()
        {
            TFMap[] maps = new TFMap[Maps.Count];
            Maps.CopyTo(maps);
            return maps;
        }

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

    public class MapChangeEventArgs : EventArgs
    {
        public TFMap Current { get; set; }

        public MapChangeEventArgs(TFMap current)
        {
            Current = current;
        }
    }
}
