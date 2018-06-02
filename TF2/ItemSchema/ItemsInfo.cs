using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using TF2.Info;

namespace TF2.Items
{
    public enum FetchSchemaResult
    {
        FILE_EXIST,
        FAIL_INVALID_APIKEY,
        FAIL_ACCESS,
        SUCCESS
    }

    public static class ItemsInfo
    {
        private const string VERIFY_APIKEY_URL = "https://api.steampowered.com/IEconItems_440/GetSchemaURL/v1/?key={0}&format=xml";
        private const string SCHEMA_ITEMS_URL = "https://api.steampowered.com/IEconItems_440/GetSchemaItems/v1/?key={0}&language={1}&format=xml";
        private const string SCHEMA_ATTRIBUTES_URL = "https://api.steampowered.com/IEconItems_440/GetSchemaOverview/v1/?key={0}&language={1}&format=xml";
        private static List<TFItem> items = new List<TFItem>();
        private static List<TFAttribute> attributes = new List<TFAttribute>();
        private static string schema_items_dir;
        private static string schema_attributes_dir;

        private static string apikey = null;
        private static string language = "en";
        private static string version;

        public static string Apikey { get { return apikey; } }
        public static string Language { get { return language; } }
        public static string Version { get { return version; } }

        public static Func<XmlNode, TFItem> ItemsHandler = NodeToTFItem;
        public static Func<XmlNode, TFAttribute> AttributesHandler = NodeToTFAttribute;

        public static Func<XmlNode, TFItem> DefaultItemsHandler
        {
            get
            {
                return NodeToTFItem;
            }
        }

        public static Func<XmlNode, TFAttribute> DefaultAttributesHandler
        {
            get
            {
                return NodeToTFAttribute;
            }
        }

        public static TFItem[] Items
        {
            get
            {
                return items.ToArray();
            }
        }

        public static TFAttribute[] Attributes
        {
            get
            {
                return attributes.ToArray();
            }
        }

        public static bool Register(string api_key, string lang = "en")
        {
            using (var wc = new WebClient())
            {
                try
                {
                    wc.Encoding = System.Text.Encoding.UTF8;
                    string content = wc.DownloadString(String.Format(VERIFY_APIKEY_URL, api_key));
                    if (!content.Contains("Forbidden"))
                    {
                        apikey = api_key;
                        language = lang;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (System.Net.WebException)
                {
                    return false;
                }
            }
        }

        //Fetch And Version Check
        public static async Task<FetchSchemaResult> FetchSchema(string directory)
        {
            if(apikey == null)
            {
                return FetchSchemaResult.FAIL_INVALID_APIKEY;
            }

            if(Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string schemaurl = String.Format(SCHEMA_ITEMS_URL, apikey, language);
            schema_items_dir = Path.Combine(directory, language + ".items.schama.xml");
            schema_attributes_dir = Path.Combine(directory, language + ".attributes.schama.xml");
                
            //File not Exist (full or one)
            if(!File.Exists(schema_items_dir) || !File.Exists(schema_attributes_dir))
            {
                File.Delete(schema_items_dir);
                File.Delete(schema_attributes_dir);
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    try
                    {
                        await wc.DownloadFileTaskAsync(String.Format(SCHEMA_ITEMS_URL, apikey, language), schema_items_dir);
                        await wc.DownloadFileTaskAsync(String.Format(SCHEMA_ATTRIBUTES_URL, apikey, language), schema_attributes_dir);
                        return FetchSchemaResult.SUCCESS;
                    }
                    catch(System.Net.WebException)
                    {
                        return FetchSchemaResult.FAIL_ACCESS;
                    }
                }
            }
            //All File Exist
            else
            {
                try
                {
                    //Get Current Version
                    XmlDocument doc = new XmlDocument();
                    doc.Load(schema_attributes_dir);
                    if(doc.DocumentElement["items_game_url"] != null)
                    {
                        version = doc.DocumentElement["items_game_url"].InnerText;
                    }

                    //Check Version
                    if(version != null)
                    {
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            wc.Encoding = System.Text.Encoding.UTF8;
                            string content = wc.DownloadString(String.Format(VERIFY_APIKEY_URL, apikey));
                            if (!content.Contains("Forbidden"))
                            {
                                doc.Load(content);
                                if (doc.DocumentElement["items_game_url"] != null)
                                {
                                    string webversion = doc.DocumentElement["items_game_url"].InnerText;
                                    if (!version.Equals(webversion))
                                    {
                                        version = webversion;
                                        File.Delete(schema_items_dir);
                                        File.Delete(schema_attributes_dir);
                                        await wc.DownloadFileTaskAsync(String.Format(SCHEMA_ITEMS_URL, apikey, language), schema_items_dir);
                                        await wc.DownloadFileTaskAsync(String.Format(SCHEMA_ATTRIBUTES_URL, apikey, language), schema_attributes_dir);
                                        return FetchSchemaResult.SUCCESS;
                                    }
                                    else
                                    {
                                        return FetchSchemaResult.FILE_EXIST;
                                    }
                                } 
                            }
                            else
                            {
                                return FetchSchemaResult.FAIL_INVALID_APIKEY;
                            }
                            
                        }
                    }
                }
                catch
                {
                    return FetchSchemaResult.FAIL_ACCESS;
                }
            }
            return FetchSchemaResult.FILE_EXIST;
        }

        public static async Task ReadSchema()
        {
            await Task.Factory.StartNew(delegate
            {
                XmlDocument schema = new XmlDocument();
                schema.Load(schema_items_dir);
                foreach (XmlNode node in schema.DocumentElement["items"])
                {
                    if (node.Name.ToLower().Equals("item"))
                    {
                        var item = ItemsHandler(node);
                        if(item != null)
                        {
                            items.Add(item);
                        }
                    }
                }
                schema.Load(schema_attributes_dir);
                foreach (XmlNode node in schema.DocumentElement["attributes"])
                {
                    if (node.Name.ToLower().Equals("attribute"))
                    {
                        var attribute = AttributesHandler(node);
                        if (attribute != null)
                        {
                            attributes.Add(attribute);
                        }
                    }
                }
            });
        }

        private static TFItem NodeToTFItem(XmlNode node)
        {
            if (node["item_class"] != null
                && node["defindex"] != null
                && node["name"] != null
                && node["item_slot"] != null
                && !node["item_slot"].InnerText.Equals("action")
                && node["image_url"] != null
                && node["image_url"].InnerText.StartsWith("http://media.steampowered.com")
                && node["item_name"] != null
                && node["item_class"] != null
                && !node["item_class"].InnerText.Equals("no_entity")
            )
            {
                var item = new TFItem()
                {
                    ClassName = node["item_class"].InnerText,
                    DefinitionID = int.Parse(node["defindex"].InnerText),
                    Name = node["name"].InnerText,
                    DisplayName = node["item_name"].InnerText,
                    ImageURL = node["image_url"].InnerText
                };

                var slot = TFEnumConvert.StringToSlot(node["item_slot"].InnerText);
                if (slot.HasValue) item.Slot = slot.Value;

                string[] splited = item.ImageURL.Split('/');
                var sp = splited[splited.Length - 1].Split('.');
                item.ImageName = String.Join(".", sp[0], sp[2]);

                if (node["used_by_classes"] != null)
                {
                    foreach (XmlNode node2 in node.SelectNodes("used_by_classes"))
                    {
                        var cls = TFEnumConvert.StringToTFClass(node2.InnerText);
                        if (cls.HasValue)
                        {
                            item.UsedByClass(cls.Value);
                        }
                    }
                }
                item.ReadOnly = true;
                return item;
            }
            return null;
        }

        private static TFAttribute NodeToTFAttribute(XmlNode node)
        {
            if (node["name"] != null
                && node["defindex"] != null
                && node["attribute_class"] != null
                && node["description_string"] != null
                && node["description_format"] != null
                && node["effect_type"] != null
            )
            {
                var attr = new TFAttribute()
                {
                    Name = node["name"].InnerText,
                    DefinitionID = int.Parse(node["defindex"].InnerText),
                    AttributeClass = node["attribute_class"].InnerText,
                    Description = node["description_string"].InnerText.Replace("%s1", "{0}")
                };
               

                var df = TFEnumConvert.StringToDescriptionFormat(node["description_format"].InnerText);
                var et = TFEnumConvert.StringToEffectType(node["effect_type"].InnerText);
                if (df.HasValue) attr.DescriptionFormat = df.Value;
                if (et.HasValue) attr.EffectType = et.Value;

                attr.ReadOnly = true;
                return attr;
            }
            return null;
        }
    }
}