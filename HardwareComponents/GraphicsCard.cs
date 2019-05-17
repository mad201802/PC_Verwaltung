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
    }
}
