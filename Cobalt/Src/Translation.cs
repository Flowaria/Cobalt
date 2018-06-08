using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cobalt.Extension
{
    public static class Translation
    {
        private static XmlDocument doc = new XmlDocument();
        private static List<String> list; //string key list
        private static Dictionary<String, String> def; //default string
        private static Dictionary<String, String> dict; //translated string
        private static bool alternative_loaded;

        static Translation()
        {
            list = new List<String>();
            dict = new Dictionary<String, String>();
            def = new Dictionary<String, String>();
            alternative_loaded = false;
        }

        public static bool LoadDefaultFile(string filedir)
        {
            try
            {
                doc.Load(filedir);
                XmlElement root = doc.DocumentElement;
                foreach (XmlNode node in root)
                {
                    if (node.Attributes != null && node.Attributes["value"] != null)
                    {
                        def.Add(node.Name.ToLower(), node.Attributes["value"].Value);
                        list.Add(node.Name.ToLower());
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool LoadTranslationFile(string filedir)
        {
            try
            {
                dict.Clear();
                doc.Load(filedir);
                XmlElement root = doc.DocumentElement;
                foreach (XmlNode node in root)
                {
                    if (node.Attributes != null && node.Attributes["value"] != null && list.Exists(x=>x.ToLower() == node.Name.ToLower()))
                    {
                        dict.Add(node.Name.ToLower(), node.Attributes["value"].Value);
                    }
                }
                alternative_loaded = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static void InitKey(string key)
        {
            if(list.Exists(x=>x == key))
            {
                list.Add(key);
            }
        }
        public static string Get(string key)
        {
            try
            {
                if(list.Exists(x=>x == key.ToLower()))
                {
                    if(dict.ContainsKey(key.ToLower()))
                    {
                        return dict[key.ToLower()];
                    }
                    return def[key.ToLower()];
                }
                return null;
                
            }
            catch
            {
                return null;
            }
        }
    }
}
