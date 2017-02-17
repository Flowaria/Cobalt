using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Population
{
    public partial class PopProjectFile
    {
        public bool Save(PopProject proj, string filename)
        {
            return true;
        }
        public bool Export(PopProject proj, string filename)
        {
            return false;
        }
    }
}
