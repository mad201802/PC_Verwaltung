using System;
using System.Collections.Generic;
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
        #region README
        /* 
        * Diese Solution ist nur zum testen von (zukünftigen) Funktionen und ist bei der Bewertung weitgehend zu ignorieren!
        */
        #endregion

        static void Main(string[] args)
        {

            //testDatabaseCreation();


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
            /*
            //config
            string server = "127.0.0.1";
            string database = "pc_verwaltung";
            string uid = "root";
            string password = "";

            Database db = new Database(server, database, uid, password);
            db.connect();

            //db.GetUser("Test");
            User a = new User("Herbert", "1234a");

            if (!db.DoesUserExist(a))
            {
                Console.WriteLine("User does not exist");
                Console.ReadKey();
                
                db.createNewUser(null, a);
                Console.WriteLine("User created!");
            }
            else
            {
                Console.WriteLine("User exist!");
                User cache = new User("cache", "New Password");
                db.changePassword(a, cache.password);
            }
            */
            #endregion

            Console.ReadKey();

        }




        static void testHardware()
        {
            ComputerInformation CI = new ComputerInformation();
            CI.gatherInformation();
            Console.WriteLine("CPU:");
            Console.WriteLine("CPU Anzahl: " + CI.CPUcount);
            Console.WriteLine("CPU Name: " + CI.CPUname);
            Console.WriteLine("CPU Kerne: " + CI.CPUcores);
            Console.WriteLine("CPU Threads: " + CI.CPUthreads);
            Console.WriteLine("CPU Architektur: " + CI.CPUarchitecture);
            Console.WriteLine("CPU Base Clock: " + CI.CPUbaseClock + " MHz");
            Console.WriteLine("CPU Max Clock: " + CI.CPUmaxSyncClock + " MHz");
            Console.WriteLine("CPU Bits: " + CI.CPUbits);
            Console.WriteLine("CPU Level 3 Cache: " + CI.CPUl3cache);

            Console.WriteLine("CPU Hersteller: " + CI.CPUmanufacture);
            Console.WriteLine("----\nGPU:");
            Console.WriteLine("GPU Hersteller: " + CI.GPUmanufacture);
            Console.WriteLine("GPU Name: " + CI.GPUname);
            Console.WriteLine("GPU RAM: " + CI.GPUram + " GB");

            Console.WriteLine("----\nMobo:");
            Console.WriteLine("Mobo Hersteller: " + CI.motherboardManufacture);
            Console.WriteLine("Mobo Modell: " + CI.motherboardModel);

            Console.WriteLine("----\nRAM:");
            Console.WriteLine("RAM Größe in GB: " + CI.ramSizeInGB);
            Console.WriteLine("RAM Takt: " + CI.ramDimSpeeds[0] + " MHz");
        }

        static void testDatabaseCreation()
        {
            //config
            string server = "127.0.0.1";
            string database = "pc_verwaltung";
            string uid = "root";
            string password = "";

            Database db = new Database(server, database, uid, password);
            switch(db.connect())
            {
                case -1:
                    Console.WriteLine("Server antwortet nicht");
                    break;
                case 0:
                    Console.WriteLine("Datenbank nicht gefunden. \nSoll die Datenbank erstellt werden? y/n");
                    if(Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        db.CreateDatabase();
                    }
                    break;
                case 1:
                    Console.WriteLine("Connected");
                    break;
            }
        }
    }
}

