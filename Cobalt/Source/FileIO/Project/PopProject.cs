using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.FileIO.Project
{
    public class PopProject
    {
        public void Cast()
        {

        }
    }

    public class PopProjectFile
    {
        public static PopProject loadProjectFile(string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                int header = stream.ReadByte();
                if(header == 47718)
                {

                }else return null; //맞는 파일이 아님
            }
            return null;
        }
    }
}
