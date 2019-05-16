using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.HardwareComponents
{
    class RAM : Speicher
    {
        public string Type { get; set; }

        public RAM():base(Volatile: true)
        {
            
        }
    }
}
