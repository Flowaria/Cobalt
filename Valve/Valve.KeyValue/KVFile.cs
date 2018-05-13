using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Valve.KeyValue
{
    public static class KVFile
    {
        private static Regex regRemark;
        private static Regex regKv;
        private static Regex regQuote;

        static KVFile()
        {
            regRemark = new Regex("((\\/\\/+).+)|((\\/\\*+)(.|\\n|\\r)*(\\*\\/))", RegexOptions.IgnoreCase);
            regKv = new Regex("(\\\"([^\\\"]*?)\\\")|(\\'[^\\']*?\\')|[{}]|([^\\s][^\\s]*)");
            regQuote = new Regex("(^\"+|\"+$)|(^'+|'+$)");
        }

        public static KeyValues ImportKeyValue(string content)
        {
            var rmvRemark = regKv.Matches(regRemark.Replace(content, ""));

            int depth = 0;
            string previous = null;
            KeyValues kv = new KeyValues("WaveSchedule");
            KVNode current = null;
            foreach (Match reach in rmvRemark)
            {
                string str = regQuote.Replace(reach.Value, "");
                switch (str)
                {
                    default:
                        if(previous == null) //홀수 노드
                        {
                            previous = str;
                            if (current == null) //root
                            {
                                kv.Root.KeyName = str;
                                current = kv.Root;
                            }
                            //keyvalue or parent
                        }
                        else //짝수 노드
                        {
                            if(previous == "#base") //베이스 파일 추가
                            {
                                kv.AddBaseFile(str);
                            }
                            else //전통적 키밸류
                            {
                                current.SetValue(str, previous);
                            }
                            previous = null;
                        }
                        break;

                    case "#base":
                        previous = "#base";
                        break;

                    //새 부모 노드
                    case "{":
                        if(previous != null) 
                        {
                            var node = new KVNode(previous);
                            current.AppendNode(node);
                            current = node;
                            previous = null;
                            depth++;
                        }
                        break;

                    //부모 노드 끝 -> 상위 노드로
                    case "}":
                        if (current != null && current.Type != KVNode.KVNodeType.Root)
                        {
                            current = current.Parent;
                        }
                        previous = null;
                        depth--;
                        break;
                }
            }
            if(depth == 0)
            {
                return kv;
            }
            return null;
        }

        private static void SkipEmptySpace(Stream stream)
        {

        }

        public static string ExportKeyValue(KeyValues kv)
        {
            StringBuilder sb = new StringBuilder();
            NodeToFullFormatString(kv.Root, sb);
            Console.Write(sb.ToString());
            return sb.ToString();
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
                sb.AppendFormat("\"{0}\" \"{1}\"", node.KeyName, node.GetValue().ToString());
            }
        }
    }
}
