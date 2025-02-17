using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IPLOG
{
    public class Device 
    {
        public string Id { get; set; }
        public string BusId { get; set; }
        public List<Input> Inputs { get; set; } = new List<Input>();
        public List<Output> Outputs { get; set; } = new List<Output>();
        public List<InterfaceModule> Interfaces { get; set; } = new List<InterfaceModule>();
    }
}


