using System;
using System.Collections.Generic;
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
            ComputerInformation CI = new ComputerInformation();
            CI.gatherInformation();

            Console.WriteLine("CPU Architektur: " + CI.CPUarchitecture);
            Console.WriteLine("CPU Base Clock: " + CI.CPUbaseClock);
            Console.WriteLine("CPU Bits: " + CI.CPUbits);
            Console.WriteLine("CPU Kerne: " + CI.CPUcores);
            Console.WriteLine("CPU Anzahl: " + CI.CPUcount);
            Console.WriteLine("CPU Level 3 Cache: " + CI.CPUl3cache);
            Console.WriteLine("CPU Hersteller: " + CI.CPUmanufacture);
            Console.WriteLine("CPU Max Clock: " + CI.CPUmaxSyncClock);
            Console.WriteLine("CPU Name: " + CI.CPUname);
            Console.WriteLine("CPU Threats: " + CI.CPUthreats);
            Console.WriteLine("GPU Hersteller: " + CI.GPUmanufacture);
            Console.WriteLine("GPU Name: " + CI.GPUname);
            Console.WriteLine("GPU RAM: " + CI.GPUram);
            Console.WriteLine("Mobo Hersteller: " + CI.motherboardManufacture);
            Console.WriteLine("Mobo Modell: " + CI.motherboardModel);
            Console.WriteLine("RAM Größe in GB: " + CI.ramSizeInGB);
            Console.WriteLine("RAM Takt: " + CI.ramDimSpeeds[0]);
            


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
    }
}

