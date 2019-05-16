using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.HardwareComponents
{
    class Drive:Speicher
    {
        public enum Interface { IDE, SATA, M_2, SAS }

        public Drive():base(Volatile: false)
        {

        }
    }
}
