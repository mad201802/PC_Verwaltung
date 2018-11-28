using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PC_Verwaltung
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
                connection.Open();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User getUser(string username)
        {
            /*
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = connection.CreateCommand();


            command.CommandText = "SELECT * FROM user WHERE username = " + username + "LIMIT 1 ";
            MySqlDataReader Reader;
            connection.Open();
            command.Prepare();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    row += Reader.GetValue(i).ToString() + ", ";
                Console.WriteLine(row);
            }
            connection.Close();
            */
            return new User("admin", "admin");
        }

    }
}
