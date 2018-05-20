using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Valve.KeyValue;

namespace Cobalt.IconConverter
{
    class Program
    {
        public const string args_format = "-file \"{0}\" -output \"output\" -exportformat \"png\"";
        public const string OUTPUT_FILE = "output/definition.list";
        
        static void Main(string[] args)
        {
            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(Path.GetTempPath(),"VTFCmd.exe");
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            Regex regNameonly = new Regex("(.*leaderboard_class_)|(.vtf)|(.vmt)", RegexOptions.IgnoreCase);
            Directory.CreateDirectory("input");
            Directory.CreateDirectory("output");
            var vmtTarget = new Dictionary<string, List<string>>(); //sunstick_scout_giant -> sunstick_scout

            //Export Included Resource
            foreach (var file in System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                string[] splited = file.Split('.');
                string dir = Path.Combine(Path.GetTempPath(), String.Join(".", splited[splited.Length - 2], splited[splited.Length - 1]));
                if (!File.Exists(dir))
                {
                    using (var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(file))
                    {
                        using (var fileStream = new FileStream(dir, FileMode.Create))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
            }

            foreach (var file in Directory.GetFiles("input"))
            {
                if(file.EndsWith(".vtf"))
                {
                    process.StartInfo.Arguments = String.Format(args_format, file);
                    process.Start();
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
                        Console.WriteLine(String.Format("{0} :: {1}", basetexture, vmtfilename));
                    }
                }
            }

            DirectoryInfo d = new DirectoryInfo("output");
            FileInfo[] infos = d.GetFiles();
            foreach (FileInfo f in infos)
            {
                if(regNameonly.IsMatch(f.FullName))
                {
                    var newfile = "output/" + regNameonly.Replace(f.FullName, "");
                    if (File.Exists(newfile)) File.Delete(newfile);
                    File.Move(f.FullName, newfile);
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
                            
                            sw.Write("|");
                            sw.Write(String.Join("|", vmtTarget[key].ToArray()));
                        }
                        sw.WriteLine();
                    }
                }
            }

            File.Delete(Path.Combine(Path.GetTempPath(), "VTFCmd.exe"));
            File.Delete(Path.Combine(Path.GetTempPath(), "VTFLib.dll"));
            File.Delete(Path.Combine(Path.GetTempPath(), "DevIL.dll"));
        }
    }
}
