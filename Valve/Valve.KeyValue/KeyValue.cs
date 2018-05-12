using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valve.KeyValue
{
    public class KeyValue
    {
        private List<string> basefile;
        public KVNode Root { get; }
        public KeyValue(string root)
        {
            Root = new KVNode(root);
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
