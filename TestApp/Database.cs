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
        private MySqlConnection connection;
        private MySqlCommand command;

        public Database(string server, string database, string uid, string password)
        {
            ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        public bool connect()
        {
            try
            {
                connection = new MySqlConnection(ConnectionString);
                command = connection.CreateCommand();
                connection.Open();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User GetUser(string username)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }


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
        }

        public Boolean createNewUser(User currentUser, string username, string hashPassword)
        {
            //TODO: Überprüfen ob user neue User hinzufügen darf.
            if (GetUser(username) == null)
            {

                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                command.CommandText = "INSERT INTO user(username, password)" +
                "VALUES('" + username + "', '" + hashPassword + "');";

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean changePassword(User currentUser, string oldPassword, string newPassword)
        {
            //TODO
            return false;
        }

    }
}