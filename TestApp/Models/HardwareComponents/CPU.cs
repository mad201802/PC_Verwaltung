using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.HardwareComponents
{
    class CPU
    {
        /// <summary>
        /// only for Database results
        /// </summary>
        public int ID { get; set; } = 0;
        public string Name { get; set; }
        public string Manufacture { get; set; }
        public double StockClock { get; set; }
        public string Socket { get; set; }
        public int L3Cache { get; set; }
        public int CPUarchitecture { get; set; }
    }
}
