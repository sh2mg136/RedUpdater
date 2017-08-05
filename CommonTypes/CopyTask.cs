using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{

    public class CopyTask
    {
        public List<CommonTypes.MyFileInfo> Files { get; set; }
        public string DestinationPath { get; set; }
        public string Description { get; set; }
    }

}
