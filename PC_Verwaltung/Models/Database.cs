using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        //Verbinden zur Datebank, gibt boolean zurück
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

        //Rückgabewert: User und Passwort aus der Datenbank 
        public User GetUser(string username)
        {
            try
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
            catch (Exception ex)
            {
                return null;
            }
        }

        //Erstellen eines neues Users in der Datenbank
        public bool createNewUser(User currentUser, User newUser)
        {
            //TODO: Überprüfen ob user neue User hinzufügen darf.
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            command.CommandText = "INSERT INTO user(username, password)" +
            "VALUES('" + newUser.username + "', '" + newUser.password + "');";
            command.Prepare();
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }

        /// <summary>
        /// Ändert das passwort für den aktuellen user
        /// Überprüft nicht das alte passwort.
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="newPassword">Das neue gehashte Passwort</param>
        /// <returns></returns>
        public bool changePassword(User currentUser, string newPassword)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            command.CommandText = "UPDATE user SET password = '" + newPassword + "' WHERE username = '" + currentUser.username + "';";
            command.Prepare();
            int statusCode = command.ExecuteNonQuery();

            connection.Close();

            if (statusCode == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Checkt ob der User schon in der Datenbank ist (User-Obj. param)
        public bool UserExist(User user)
        {
            return GetUser(user.username) != null;
        }

        //Checkt ob der User schon in der Datenbank ist (Username param)
        public bool UserExist(string username)
        {
            return GetUser(username) != null;
        }
    }
}