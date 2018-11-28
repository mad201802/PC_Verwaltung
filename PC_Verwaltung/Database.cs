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
        private MySqlConnection connection;
        private MySqlCommand command;

        public Database(string server, string database, string uid, string password)
        {
            ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        public bool connect()
        {
            /*
            try
            {
                if (connection.State != System.Data.ConnectionState.Open) {
                    connection = new MySqlConnection(ConnectionString);
                    command = connection.CreateCommand();
                    connection.Open();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            */

            return true;
        }

        public User GetUser(string username)
        {
            /*
            command.CommandText = "SELECT * FROM user WHERE username = '" + username + "' LIMIT 1;";
            MySqlDataReader Reader;
            command.Prepare();
            Reader = command.ExecuteReader();
            if (Reader.HasRows)
            {
                Reader.Read();
                User u = new User(Reader.GetString(1), "Default");
                u.setHashPassword(Reader.GetString(2));
                Console.WriteLine(u);
                connection.Close();
                return u;
            }
            else
            {
                connection.Close();
                return null;
            }
            */

            return new User("admin", "admin");

        }

    }
}
