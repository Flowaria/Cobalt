using Cobalt.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cobalt.Population
{
    public partial class PopProjectFile
    {
        public PopProject Import(string filename)
        {
            var parser = new KeyValueParser();
            Console.WriteLine(filename);
            List<KeyValueNode> pop = parser.read(File.ReadAllText(filename));
            if (pop != null)
            {
                if(pop.Any(x => StringFunction.IEqual("WaveSpawn", x.Name)
                           ||   StringFunction.IEqual("Mission", x.Name))
                )
                {
                    Console.WriteLine("Reading");
                    var proj = new PopProject();

                    
                    var Templates = pop.Find(x => StringFunction.IEqual("Templates", x.Name) && !x.isValue);
                    var LBaseFile = pop.FindAll(x => StringFunction.IEqual("#base", x.Name) && x.isValue);
                    var LMission = pop.FindAll(x => StringFunction.IEqual("Mission", x.Name) && !x.isValue);
                    var LWave = pop.FindAll(x => StringFunction.IEqual("Wave", x.Name) && !x.isValue);

                    foreach (var key in LBaseFile)
                    {
                        Console.WriteLine("{0} {1}", key.Name, key.Value);
                        proj.BaseFiles.Add(key.Value);
                    }
                    foreach (var key in LMission)
                    {
                        var childs = parser.readChilds(pop, key);
                        if(childs != null)
                        {
                            var child = childs.Find(x => StringFunction.IEqual("TFBot", x.Name) && !x.isValue);
                            if(child != null)
                            {
                                childs = parser.readChilds(childs, child);
                            }
                        }   
                    }

                    //For Wave
                    foreach (var key in LWave)
                    {
                        var childs = parser.readChilds(pop, key);
                        if (childs != null)
                        {
                            var child = childs.Find(x => StringFunction.IEqual("WaveSpawn", x.Name) && !x.isValue);
                            if (child != null)
                            {
                                childs = parser.readChilds(childs, child);
                            }
                        }
                    }

                    return proj;
                }
            }
            return null;
        }

        //readPopulationFile 기능들
        private void inBlock_TFBot()
        {

        }

        public PopProject Open(string filename)
        {
            if (PopProject.ValidateProjFile(filename))
            {
                
            }
            return null;
        }

    }
}
