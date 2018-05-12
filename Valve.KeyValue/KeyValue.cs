using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valve.Keyvalue
{
    public class KeyValue
    {
        private List<string> basefile;
        public KVNode Root { get; }
        public KeyValue(string root)
        {
            Root = new KVNode(root);
        }

        public void ExportToFile(string outputdir)
        {
            KVNode[] Waves = Root.FindNodes("Wave");
        }

        public string ExportToString(string outputdir)
        {

        }

        public static KeyValue LoadFromFile(string file)
        {
            if (File.Exists(file))
            {
                return Load(File.ReadAllText(file));
            }
            return null;
        }

        private static KeyValue Load(string content)
        {

        }

        public void AddBaseFile(string filename)
        {
            if(basefile.Exists(x=>x.Equals(filename)))
            {
                basefile.Add(filename);
            }
        }

        public void RemoveBaseFile(string filename)
        {
            if (basefile.Exists(x => x.Equals(filename)))
            {
                basefile.Remove(filename);
            }
        }
    }
}
