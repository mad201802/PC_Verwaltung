using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareComponents
{
    class GraphicsCard:HWComponent
    {
        public int VRAM { get; set; }
        public string Chip { get; set; }
        /// <summary>
        /// PCIe_x16 Gen3, PCIe_x8 Gen3
        /// </summary>
        public string Interface { get; set; }
    }
}
