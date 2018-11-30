using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //config
            string server = "127.0.0.1";
            string database = "pc_verwaltung";
            string uid = "root";
            string password = "";

            Database db = new Database(server, database, uid, password);
            db.connect();

            //db.GetUser("Test");
            
            if(!db.DoesUserExist("Herbert"))
            {
                Console.WriteLine("User does not exist");
                Console.ReadKey();
                User a = new User("Herbert", "1234a");

                db.createNewUser(null, a);
                Console.WriteLine("User created!");
            }

            Console.ReadKey();
        }
    }
}
