using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareComponents
{
    class CPU:HWComponent
    {
        public double StockClock { get; set; }
        public string Socket { get; set; }
        public int L3Cache { get; set; }
        public int CPUarchitecture { get; set; }
    }
}
