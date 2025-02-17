using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLOG
{
    public class InterfaceModule
    {
        public string Name { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

    }
}
