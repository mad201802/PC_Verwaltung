using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TestApp
{
    public class Database
    {

        private string ConnectionString;

        public Database(string server, string database, string uid, string password)
        {
            ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        public bool connect()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                MySqlCommand command = connection.CreateCommand();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User GetUser(string username)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = connection.CreateCommand();


            command.CommandText = "SELECT * FROM user WHERE username = '" + username + "' LIMIT 1;";
            MySqlDataReader Reader;
            connection.Open();
            command.Prepare();
            Reader = command.ExecuteReader();
            if(Reader.HasRows)
            {
                Reader.Read();
                User u = new User(Reader.GetValue(1).ToString(), "");
                
                Console.WriteLine(u);
                connection.Close();
                return u;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

    }
}
