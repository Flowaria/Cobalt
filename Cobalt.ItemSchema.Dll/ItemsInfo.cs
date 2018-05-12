using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TF2.Info;

namespace TF2.Items
{
    public static class ItemsInfo
    {
        private const string SCHEMA_ITEMS_URL = "https://api.steampowered.com/IEconItems_440/GetSchemaItems/v1/?key={0}&language={1}&format=xml";
        private const string SCHEMA_ATTRIBUTES_URL = "https://api.steampowered.com/IEconItems_440/GetSchemaOverview/v1/?key={0}&language={1}&format=xml";
        private static List<TFItem> items;
        private static List<TFAttribute> attributes;
        private static string version_string;
        private static string schema_items_dir;
        private static string schema_attributes_dir;

        public static async Task FetchSchema(string directory, string key, string lang = "en", string current_version = "empty")
        {
            string schemaurl = String.Format(SCHEMA_ITEMS_URL, key, lang);
            string schamadir;
            if (directory.EndsWith("/"))
                schamadir = directory + lang + ".schama.xml";
            else
                schamadir = directory + "/" + lang + ".schama.xml";

            if(File.Exists(schamadir))
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    wc.Encoding = System.Text.Encoding.UTF8;

                    string content = await wc.DownloadStringTaskAsync(String.Format(SCHEMA_ITEMS_URL, key, lang));
                    XmlDocument schema = new XmlDocument();
                }
            }
        }

        public static async Task ReadSchema(string directory)
        {
            /*
            await Task.Factory.StartNew(delegate
            {
                Console.Out.WriteLine(saveurl);
                XmlDocument schema = new XmlDocument();
                schema.Load(saveurl);
                foreach (XmlNode node in schema.DocumentElement)
                {
                    if (node.Name.ToLower().Equals("items"))
                    {
                        if (node["item_class"] != null && node["defindex"] != null &&
                            node["name"] != null && node["item_slot"] != null && node["image_url"] != null && node["item_name"] != null)
                        {
                            TFItem item = null;
                            if (node["used_by_classes"] != null)
                            {
                                item = new TFItem(node["item_class"].InnerText, int.Parse(node["defindex"].InnerText), node["name"].InnerText, TFSlotFunction.StringToSlot(node["item_slot"].InnerText), FormatImageURL(node["image_url"].InnerText), node["item_name"].InnerText, false);
                                foreach (XmlNode node2 in node.SelectNodes("used_by_classes"))
                                {
                                    TFClass cls = TFClassFunction.StringToClass(node2.InnerText);
                                    if (cls != TFClass.Null)
                                    {
                                        item.UsedByClassToggle(cls);
                                    }
                                }
                            }
                            else
                            {
                                item = new TFItem(node["item_class"].InnerText, int.Parse(node["defindex"].InnerText), node["name"].InnerText, TFSlotFunction.StringToSlot(node["item_slot"].InnerText), FormatImageURL(node["image_url"].InnerText), node["item_name"].InnerText, true);
                            }
                            items.Add(item);
                        }
                    }
                    else if (node.Name.ToLower().Equals("attributes"))
                    {

                    }
                }
            });
            */
        }

        public static string UrlToVersionString(string url)
        {
            string[] splited = url.Split('/');
            return splited[splited.Length - 1].Split('.')[1];
            //http://media.steampowered.com/apps/440/scripts/items/items_game.9219e13469eb57d1a2513dc5124786b5df20cf6c.txt
        }

        private static string FormatImageURL(string fullurl)
        {
            string[] splited = fullurl.Split('/');
            return splited[splited.Length - 1];
        }
    }
}