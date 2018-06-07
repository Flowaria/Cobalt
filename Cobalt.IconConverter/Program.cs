using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Valve.KeyValue;
using Valve.TextureFormat;

namespace Cobalt.IconConverter
{
    class Program
    {
        public const string OUTPUT_FILE = "output/definition.list";
        
        static void Main(string[] args)
        {
            Regex regNameonly = new Regex("(.*leaderboard_class_)|(.vtf)|(.vmt)");
            Directory.CreateDirectory("input");
            Directory.CreateDirectory("output");
            var vmtTarget = new Dictionary<string, List<string>>(); //sunstick_scout_giant -> sunstick_scout

            foreach (var file in Directory.GetFiles("input"))
            {
                if(file.EndsWith(".vtf"))
                {
                    //VTF vtf = new VTF();
                }
                else if(file.EndsWith(".vmt"))
                {
                    KeyValues kv = KVFile.ImportKeyValue(File.ReadAllText(file), false);
                    if(kv != null)
                    {
                        var vmtfilename = regNameonly.Replace(file, "");
                        var basetexture = regNameonly.Replace((string)kv.Root.GetValue("$baseTexture"), "");
                        if(vmtTarget.ContainsKey(basetexture))
                        {
                            vmtTarget[basetexture].Add(vmtfilename);
                        }
                        else
                        {
                            var list = new List<string>();
                            list.Add(vmtfilename);
                            vmtTarget.Add(basetexture, list);
                        }
                        Console.WriteLine(String.Format("{0} - {1}", vmtfilename, basetexture));
                    }
                }
            }
            
            if(File.Exists(OUTPUT_FILE))
            {
                string content = File.ReadAllText(OUTPUT_FILE);
                Regex regWhitespace = new Regex("\\s");
                content = regWhitespace.Replace(content, "");
                var images = content.Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0;i<images.Length;i++)
                {
                    var icons = images[i].Split('|');
                    bool noself = icons[0].StartsWith("!");
                    string icon = icons[0].Replace("!", "");
                    if()
                    if ()

                        images[i] = "|" + String.Join("|", icons);
                }
            }
            else
            {
                using (var fs = new FileStream(OUTPUT_FILE, FileMode.OpenOrCreate))
                {
                    fs.Seek(0, SeekOrigin.End);
                    using (var sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.AutoFlush = true;
                        foreach (var key in vmtTarget.Keys)
                        {
                            sw.Write("$");
                            if (!vmtTarget[key].Exists(x => x.Equals(key)))
                                sw.Write('!');
                            vmtTarget[key].Remove(key);
                            sw.Write(key);
                            if (vmtTarget[key].Count > 0)
                            {
                                sw.Write("|");
                                sw.Write(String.Join("|", vmtTarget[key].ToArray()));
                            }
                        }
                    }
                }
            }
            

            Console.ReadLine();
        }
    }
}
