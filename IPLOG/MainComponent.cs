using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLOG
{
    class MainComponent
    {
        public List<string> AssociatedComponents { get; set; } = new List<string>(); //seznam id komponent, které jsou s touto komponentou propojeny

        public string Name { get; set; } //název hlavní komponenty
    }
}
