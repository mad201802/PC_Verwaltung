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
        static void Main(string[] args)
        {
            ComputerInformation CI = new ComputerInformation();
            


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

