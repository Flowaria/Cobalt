using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valve.Keyvalue
{
    public static class KVFile
    {
        public static KeyValue ImportKeyValue(string content)
        {

        }
        public static string ExportKeyValue(KeyValue kv)
        {
            StringBuilder sb = new StringBuilder();
            NodeToFullFormatString(kv.Root, sb);
            Console.Write(sb.ToString());
        }
        private static void NodeToFullFormatString(KVNode node, StringBuilder sb, int depth=0)
        {
            
            if(node.Type == KVNode.KVNodeType.Root || node.Type == KVNode.KVNodeType.Parent)
            {
                sb.Append('	', depth);
                sb.AppendFormat("{0}\n",node.KeyName);
                sb.Append(' ', depth);
                sb.Append("{\n");
                foreach(var n in node.FindNodes())
                {
                    NodeToFullFormatString(n, sb, depth + 1);
                }
                sb.Append('\n');
                sb.Append(' ', depth);
                sb.Append("}");
            }
            else
            {
                sb.Append(' ', depth);
                sb.AppendFormat("\"{0}\" \"{1}\"", node.KeyName, node.Get());
            }
        }
    }
}
