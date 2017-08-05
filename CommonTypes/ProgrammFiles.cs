using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class ProgrammFiles
    {
        public List<MyFileInfo> Programm { get; set; }
        public List<MyFileInfo> Plugins { get; set; }

        public ProgrammFiles()
        {
            Programm = new List<MyFileInfo>();
            Plugins = new List<MyFileInfo>();
        }
    }
}
