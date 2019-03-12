using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static Database db;
        #region README
        /* 
        * Diese Solution ist nur zum testen von (zukünftigen) Funktionen und ist bei der Bewertung weitgehend zu ignorieren!
        */
        #endregion

        static void Main(string[] args)
        {
            //testPCDataInsert();
            //testHardware();
            //testSoftware();


            testDatabaseConnection();

            /*
            string dirName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PC_verwaltung");
            string fileName = Path.Combine(dirName, "config.txt");
            
            if (! Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
                
            }



            string[] lines = { "db_user=root", "db_pass=" };
            

            File.WriteAllLines(fileName, lines);

            

        Console.WriteLine(fileName);
            Process.Start(dirName);

            
            #region database testing
            
            //config
            string server = "127.0.0.1";
            string database = "pc_verwaltung";
            string uid = "root";
            string password = "";

            Database db = new Database(server, database, uid, password);
            db.connect();

            //db.GetUser("Test");
            User a = new User("Herbert","phi", "aaaaa", "b@a.c", "123456", true);

            if (!db.UserExist(a))
            {
                Console.WriteLine("User does not exist");
                Console.ReadKey();
                
                db.createNewUser(a);
                Console.WriteLine("User created!");
            }
            else
            {
                Console.WriteLine("User exist!");
                Console.WriteLine(  db.GetUser("aaaaa").name);

            }
            
            #endregion
            */
    
            //Console.ReadKey();
            
            Console.ReadKey();

        }


        static void testSoftware()
        {
            SoftwareInformation SI = new SoftwareInformation();
            SI.GetUserDetails();
        }

        static void testHardware()
        {
            ComputerInformation CI = new ComputerInformation();
            CI.gatherInformation();
            Console.WriteLine("CPU:");
            Console.WriteLine("CPU Name: " + CI.CPUname);
            Console.WriteLine("CPU Kerne: " + CI.CPUcores);
            Console.WriteLine("CPU Threads: " + CI.CPUthreads);
            Console.WriteLine("CPU Hyperthrading: " + (CI.CPUcores*2 == CI.CPUthreads ? "Yes":"No"));
            Console.WriteLine("CPU Architektur: " + CI.CPUarchitecture);
            Console.WriteLine("CPU Base Clock: " + CI.CPUbaseClock + " MHz");
            Console.WriteLine("CPU Max Clock: " + CI.CPUmaxSyncClock + " MHz");
            Console.WriteLine("CPU Bits: " + CI.CPUbits);
            Console.WriteLine("CPU Level 3 Cache: " + CI.CPUl3cache);
            Console.WriteLine("CPU Serial Number: " + CI.CPUSerialNumber);

            Console.WriteLine("CPU Hersteller: " + CI.CPUmanufacture);
            Console.WriteLine("----\nGPU:");
            Console.WriteLine("GPU Hersteller: " + CI.GPUmanufacture);
            Console.WriteLine("GPU Name: " + CI.GPUname);
            Console.WriteLine("GPU RAM: " + CI.GPUram + " GB");

            Console.WriteLine("----\nMobo:");
            Console.WriteLine("Mobo Hersteller: " + CI.motherboardManufacture);
            Console.WriteLine("Mobo Modell: " + CI.motherboardModel);
            Console.WriteLine("Mobo Serial Number: " + CI.motherboardSerialNumber);

            Console.WriteLine("----\nRAM:");
            Console.WriteLine("RAM Größe in GB: " + CI.ramSizeInGB);
            Console.WriteLine("RAM Takt: " + CI.ramDimSpeeds[0] + " MHz");
            Console.WriteLine("----\nNetzwerkadapter:");
            foreach(var a in CI.networkInterfaces)
            {
                if (a.Name.Equals("WiFi") || a.Name.Equals("Ethernet"))
                {
                    Console.WriteLine(a.Name + " - " + CI.FormatMACAddress(a.GetPhysicalAddress().ToString()));
                }
            }
        }

        static void testDatabaseConnection()
        {
            //config
            string server = ConfigurationManager.AppSettings.Get("DatabaseServer");
            string database = ConfigurationManager.AppSettings.Get("DBname");
            string uid = ConfigurationManager.AppSettings.Get("DBUser");
            string password = ConfigurationManager.AppSettings.Get("DBPassword");

            db = new Database(server, database, uid, password);
            switch(db.connect())
            {
                case -1:
                    Console.WriteLine("Server antwortet nicht");
                    break;
                case 0:
                    Console.WriteLine("DB not found");
                    break;
                case 1:
                    Console.WriteLine("Connected");
                    break;
            }
        }

        static void testPCDataInsert()
        {
            testDatabaseConnection();
            ComputerInformation ci = new ComputerInformation();
            ci.gatherInformation();

            if(db.doesCPUexist(ci.CPUname))
            {
                Console.WriteLine("cpu existiert");
            }
            else
            {
                Console.WriteLine("cpu existiert nicht");
                Console.WriteLine("speichern?");
                if(Console.ReadKey().Key ==  ConsoleKey.Y)
                {
                    db.storeCPU(ci);
                    Console.WriteLine("done");
                }
            }
        }











    }
}

