﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class ComputerInformation
    {
        /// <summary> [!Zukünftige Funktion!]
        /// Sammelt:
        ///     Anzahl der CPUs (normale Desktops/Laptops = 1)
        ///     Anzahl der physichen Kerne (1-18)
        ///     Anzahl der logischen Kerne (1-36)
        ///     Hersteller (Intel, AMD, ARM)
        ///     Modelname (Bsp.: Intel Core i7-3770K @3,50GHz)
        ///     Größe des Level3 Caches (Bsp.: 8MB)
        ///     Basistakt (Bsp.: 100MHz)
        ///     Maximaler Synchroner Takt (Bsp.: 3501MHz)
        ///     Architektur (x86, x64)
        ///     Befehlslänge (32, 64 Bit)
        /// </summary>
        public void GetCPUDetails()
        {
            int CPUcount = 0;
            int CPUcores = 0;
            int CPUthreats = 0;

            int CPUbaseClock = 0;
            int CPUmaxSyncClock = 0;

            string CPUmanufacture = "";
            string CPUname = "";

            int L3cache = 0;

            string architecture = "";
            string bits = "";

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                CPUcount = Convert.ToInt32( item["NumberOfProcessors"]);
            }

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                bits = item["AddressWidth"].ToString();
                architecture = GetArchitectureDetail(int.Parse(item["Architecture"].ToString()));
                CPUmanufacture = item["Manufacturer"].ToString().Replace("Genuine","");
                CPUname = item["Name"].ToString();
                CPUcores = Convert.ToInt32(item["NumberOfCores"]);
                CPUthreats = Convert.ToInt32(item["NumberOfLogicalProcessors"]);
                L3cache = Convert.ToInt32(item["L3CacheSize"]);
                CPUbaseClock = Convert.ToInt32(item["ExtClock"]);
                CPUmaxSyncClock = Convert.ToInt32(item["MaxClockSpeed"]);            
            }
        }

        /// <summary> [!Zukünftige Funktion!]
        /// Sammelt:
        ///     Hersteller
        ///     Modelbezeichnung
        ///     Größe des Grafikspeichers
        /// </summary>
        public void GetGPUDetails()
        {
            string GPUname = "";
            string GPUmanufacture = "";
            int GPUram = 0; // in GB

            foreach(var item in new System.Management.ManagementObjectSearcher("Select * from Win32_VideoController ").Get())
            {
                
                GPUram = Convert.ToInt32(Convert.ToInt64(item["AdapterRAM"]) / 1_000_000_000L);

                GPUname = item["VideoProcessor"].ToString();
                GPUmanufacture = item["Name"].ToString().Replace(item["VideoProcessor"].ToString(), "").Trim();
            }
        }

        /// <summary> [!Zukünftige Funktion!]
        /// Sammelt: 
        ///     Gesammt Größe des RAMs in KB und GB
        ///     Größe und Taktraten der einzelnen DIMs
        /// </summary>
        public void GetMemoryDetails()
        {
            int ramSizeInGB = 0;
            double ramSizeInKB = 0;
            int[] ramDimSpeeds;
            long[] ramDimSizes;

            List <long> l_ramDimSizes = new List<long>();
            List <int> l_ramDimSpeeds = new List<int>();

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_OperatingSystem").Get())
            {
                ramSizeInKB = Convert.ToDouble(item["TotalVisibleMemorySize"]);
                ramSizeInGB = Convert.ToInt32(Math.Round( ramSizeInKB / (1024 * 1024), 2));
            }
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_PhysicalMemory").Get())
            {
                l_ramDimSizes.Add(Convert.ToInt64(item["Capacity"]));// Size in MB of each dim
                l_ramDimSpeeds.Add(Convert.ToInt32(item["Speed"]));// Clock speed in MHz of each dim
            }
            ramDimSpeeds = l_ramDimSpeeds.ToArray();
            ramDimSizes = l_ramDimSizes.ToArray();
        }

        /// <summary> [!Zukünftige Funktion!]
        /// Sammelt:
        ///     Hersteller
        ///     Modelbezeichnung
        /// </summary>
        public void GetMotherboardDetails()
        {
            string motherboardManufacture = "";
            string motherboardModel = "";
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_BaseBoard").Get())
            {
                motherboardManufacture = item["Manufacturer"].ToString();
                motherboardModel = item["Product"].ToString();
            }
        }


        /// <summary> [!Zukünftige Funktion!]
        /// Convertiert den Statuscode der architektur in ein Lesbaren string.
        /// </summary>
        /// <param name="architectureNumber"></param>
        /// <returns></returns>
        private string GetArchitectureDetail(int architectureNumber)
        {
            switch (architectureNumber)
            {
                case 0: return "x86";
                case 1: return "MIPS";
                case 2: return "Alpha";
                case 3: return "PowerPC";
                case 6: return "Itanium-based systems";
                case 9: return "x64";
                default:
                    return "Unkown";
            }
        }


    }
}