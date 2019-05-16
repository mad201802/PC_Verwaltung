using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.HardwareComponents
{
    class SolidStateDrive:Drive
    {
        public enum Flash { SLC, MLC, TLC, QLC }

        public enum ControllerMode { IDE, AHCI, NVME }

    }
}
