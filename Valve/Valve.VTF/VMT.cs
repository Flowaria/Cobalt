using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valve.KeyValue;

namespace Valve.TextureFormat
{
    public class VMT
    {
        public VMT()
        {
            KeyValues kv = KVFile.ImportKeyValue("filename");
            var basetexture = kv.Root.GetValue("$baseTexture");
        }
    }
}
