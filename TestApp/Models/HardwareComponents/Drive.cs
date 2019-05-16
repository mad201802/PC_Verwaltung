using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.HardwareComponents
{
    class Drive:Speicher
    {
        public enum Interface { IDE, SATA_2, SATA_3, M_2, SAS }
        /// <summary>
        /// The diameter of the Disk, like 2,5 inches or 3,5 inches
        /// </summary>
        public double DiskDiameter { get; set; }

        public Drive():base(Volatile: false)
        {

        }
    }
}
