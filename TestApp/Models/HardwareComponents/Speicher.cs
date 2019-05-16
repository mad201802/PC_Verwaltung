using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.HardwareComponents
{
    class Speicher:HWComponent
    {
        public int Capacity { get; set; }
        public readonly bool Volatile;

        public Speicher(bool Volatile)
        {
            this.Volatile = Volatile;
        }
    }
}
