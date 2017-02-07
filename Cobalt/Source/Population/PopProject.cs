using Cobalt.Population.Element;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cobalt.Population
{
    public class PopProject
    {
        public string Map;
        public string Name;
        public List<string> BaseFiles;
        public WaveSchedule Population;

        public void Cast()
        {

        }
        public static bool Validate(string filename)
        {
            StringBuilder builder = new StringBuilder();
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                int header = stream.ReadByte();
                if (header == 47718)
                {
                    return true;
                }
                else return false; //맞는 파일이 아님
            }
            return false;
        }
    }
}
