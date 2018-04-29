using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TF2.Info;

namespace Flowaria.ItemSchema
{
    public static class ItemsInfo
    {
        private static string Key;
        private static List<TFItem> items;
        private static List<TFAttribute> attributes;
        public static async Task<bool> InitSchema(string saveurl)
        {
            items = new List<TFItem>();
            if(File.Exists(saveurl))
            {
                try
                {
                    string tempfile = String.Format("{0}.temp", saveurl);
                    await DownloadNew(tempfile);
                    await Task.Factory.StartNew(async delegate
                    {
                        XmlDocument odoc = new XmlDocument();
                        XmlDocument doc = new XmlDocument();
                        odoc.Load(saveurl);
                        doc.Load(tempfile);
                        if (odoc.DocumentElement["items_game_url"].Value != doc.DocumentElement["items_game_url"].Value)
                        {
                            await DownloadNew(saveurl);
                        }
                    });
                    File.Delete(tempfile);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    await DownloadNew(saveurl);
                }
                catch
                {
                    return false;
                }
            }

            try
            {
                await Task.Factory.StartNew(delegate
                {
                    Console.Out.WriteLine(saveurl);
                    XmlDocument schema = new XmlDocument();
                    schema.Load(saveurl);
                    foreach (XmlNode node in schema.DocumentElement)
                    {
                        if (node.Name.ToLower().Equals("items"))
                        {
                            if(node["item_class"] != null && node["defindex"] != null &&
                                node["name"] != null && node["item_slot"] != null && node["image_url"] != null && node["item_name"] != null)
                            {
                                TFItem item = null;
                                if(node["used_by_classes"] != null)
                                {
                                    item = new TFItem(node["item_class"].InnerText, int.Parse(node["defindex"].InnerText), node["name"].InnerText, TFSlotFunction.StringToSlot(node["item_slot"].InnerText), FormatImageURL(node["image_url"].InnerText), node["item_name"].InnerText, false);
                                    foreach (XmlNode node2 in node.SelectNodes("used_by_classes"))
                                    {
                                        TFClass cls = TFClassFunction.StringToClass(node2.InnerText);
                                        if(cls != TFClass.Null)
                                        {
                                            Console.Out.WriteLine(cls);
                                            item.UsedByClassToggle(cls);
                                        }
                                    }
                                }
                                else
                                {
                                    item = new TFItem(node["item_class"].InnerText, int.Parse(node["defindex"].InnerText), node["name"].InnerText, TFSlotFunction.StringToSlot(node["item_slot"].InnerText), FormatImageURL(node["image_url"].InnerText), node["item_name"].InnerText, true);
                                }
                                
                                //item.Debug();
                                items.Add(item);
                            }
                        }
                        else if (node.Name.ToLower().Equals("attributes"))
                        {

                        }
                    }
                });
            }
            catch
            {
                return false;
            }
            return false;
        }
        public static async Task InitItemImage(string saveurl)
        {

        }
        public static async Task RefreshTranslate(string lang)
        {

        }
        private static async Task DownloadNew(string saveurl)
        {
            await Task.Factory.StartNew(delegate
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    wc.Encoding = System.Text.Encoding.UTF8;
                    
                    string content = wc.DownloadString("http://git.optf2.com/schema-tracking/plain/Team%20Fortress%202%20Schema?h=teamfortress2");
                    XmlDocument doc = JsonConvert.DeserializeXmlNode(content);
                    doc.Save(saveurl);
                }
            });
        }
        private static string FormatImageURL(string fullurl)
        {
            string[] splited = fullurl.Split('/');
            return splited[splited.Length - 1];
        }
    }
}