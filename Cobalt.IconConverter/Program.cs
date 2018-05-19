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

            using (var fileStream = new FileStream(OUTPUT_FILE, FileMode.OpenOrCreate))
            {
                fileStream.Seek(0, SeekOrigin.End);
                using (var sw = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    sw.AutoFlush = true;
                    foreach (var key in vmtTarget.Keys)
                    {
                        sw.Write("$");    
                        if (!vmtTarget[key].Exists(x => x.Equals(key)))
                            sw.Write('!');
                        vmtTarget[key].Remove(key);
                        sw.Write(key);
                        if(vmtTarget[key].Count > 0)
                        {
                            
                            sw.Write(" | ");
                            sw.WriteLine(String.Join(" | ", vmtTarget[key].ToArray()));
                        }
                    }
                }
                    
            }

            Console.ReadLine();
        }
    }
}
