using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareComponents
{
    class Memory:HWComponent
    {
        /// <summary>
        /// Returns the Capacity of the Memory in KB (KiB)
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// Returns the Capacity of the Memory in MB (MiB)
        /// </summary>
        public int CapacityMB {
            get { return Capacity / 1024; }
            set { Capacity = value * 1024; }
        }
        /// <summary>
        /// Returns the Capacity of the Memory in GB (GiB)
        /// </summary>
        public int CapacityGB
        {
            get { return Capacity / 1024 / 1024; }
            set { Capacity = value * 1024 * 1024; }
        }
        public readonly bool Volatile;

        public Memory(bool Volatile)
        {
            this.Volatile = Volatile;
        }
    }
}
