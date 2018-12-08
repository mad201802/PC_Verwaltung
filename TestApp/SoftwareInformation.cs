using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class SoftwareInformation
    {
        public string[] Users { get; private set; }
        public long PagefileSize { get; private set; }
        public string PagefileLocation { get; private set; }
        public string OSName { get; private set; }
        public string OSArchitecture { get; private set; }
        public string OSSerialNumber { get; private set; }
        public string BIOSVersion { get; private set; }
        public string BIOSCaption { get; private set; }
        public string BIOSBuildNumber { get; private set; }
        public string BIOSManufacturer { get; private set; }

        public void gatherInformation()
        {
            GetOSDetails();
            GetPagefileDetails();
            GetUserDetails();
            GetBIOSDetails();
        }
        public void GetUserDetails()
        {
            List<string> WindowsUserList = new List<string>();
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_UserAccount").Get())
            {
                WindowsUserList.Add(item["Name"].ToString());
            }
            Users = WindowsUserList.ToArray();
        }

        public void GetPagefileDetails()
        {
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_PageFile").Get())
            {
                PagefileSize = Convert.ToInt64(item["FileSize"]) / (1024 * 1024);
                PagefileLocation = item["Drive"].ToString();
            }
        }
        public void GetOSDetails()
        {
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_OperatingSystem").Get())
            {
                OSName = item["Name"].ToString().Substring(0, item["Name"].ToString().IndexOf("|"));
                OSArchitecture = item["OSArchitecture"].ToString();
                OSSerialNumber = item["SerialNumber"].ToString();
            }
        }

        public void GetBIOSDetails()
        {
            using (ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS"))

            using (ManagementObjectCollection moc = mos.Get())

            {

                StringBuilder sb = new StringBuilder();

                foreach (ManagementObject mo in moc)

                {

                    string[] BIOSVersions = (string[])mo["BIOSVersion"];
                    for(int i = 0; BIOSVersions[i] != null && BIOSVersions[i].Equals(string.Empty) ;i++)
                    {
                        BIOSVersion = BIOSVersions[i];
                    }
                    
                    BIOSCaption = (string)mo["Caption"];
                    BIOSManufacturer = (string)mo["Manufacturer"];
                    
                }

                Console.WriteLine(sb.ToString());

            }
        }
    
    }
}
