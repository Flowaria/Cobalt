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
        public List<string> BaseFiles = new List<string>();
        public WaveSchedule Population;

        public static bool ValidateProjFile(string filename)
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
