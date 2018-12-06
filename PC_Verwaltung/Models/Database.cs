using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PC_Verwaltung
{
    public class Database
    {

        private string ConnectionString, ConnectionStringWithoutDatabase;
        private MySqlConnection connection;
        private MySqlCommand command;
        private string server;
        public Database(string server, string database, string uid, string password)
        {
            this.server = server;
            ConnectionStringWithoutDatabase = "SERVER=" + server + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        /// <summary>
        /// Stellt verbindung zur Datenbank her.
        /// </summary>
        /// <returns>1 wenn die datenbank verbunden wurde
        /// 0 wenn die datenbank nicht verbunden wurde aber der server antwortet
        /// -1 wenn der server nicht antwortet</returns>
        public int connect()
        {
            if (PingHost(server, 3306))
            {
                try
                {
                    createConnection(ConnectionString);
                    return 1;

                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }

        }
        /// <summary>
        /// Sucht nach einem User in der Datenbank mit dem angegeben Usernamen.
        /// </summary>
        /// <param name="username">der Username der gesucht wird</param>
        /// <returns>Den User, falls kein User gefunden Wurde null</returns>
        public User GetUser(string username)
        {
            try
            {
                //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                command.CommandText = "SELECT * FROM user WHERE username = '" + username + "' LIMIT 1;";
                MySqlDataReader Reader;
                command.Prepare(); // Prüft auf SQL-Syntaxfehler oder Injektions
                Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    Reader.Read();
                    User u = new User(Reader.GetString("name"), Reader.GetString("surname"), Reader.GetString("username"), Reader.GetString("email"), Reader.GetString("password"), false);
                    MessageBox.Show(u.password);
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

        /// <summary>
        /// Erstellt ein neuen User in der Datenbank
        /// </summary>
        /// <param name="currentUser">Der aktuell eingeloggte user - für admins um User hinzuzufügen --> Zukünfigte funktion</param>
        /// <param name="newUser">Der User der erstellt werden soll.</param>
        /// <returns></returns>
        public bool createNewUser(User currentUser, User newUser)
        {
            //TODO: Überprüfen ob user neue User hinzufügen darf.

            //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            command.CommandText = "INSERT INTO user(username, password)" +
            "VALUES('" + newUser.username + "', '" + newUser.password + "');";
            command.Prepare(); // Prüft auf SQL-Syntaxfehler oder Injektions
            command.ExecuteNonQuery(); // Führt die Abfrage an die Datenbank aus ohne das ein Result-Set zurück kommt.

            connection.Close();
            return true;
        }

        /// <summary>
        /// Ändert das passwort für den aktuellen user
        /// Überprüft nicht das alte passwort.
        /// </summary>
        /// <param name="currentUser">Der aktuell eingeloggte user - für admins um User passwörter zu ändern --> Zukünfigte funktion</param>
        /// <param name="newPassword">Das neue gehashte Passwort</param>
        /// <returns></returns>
        public bool changePassword(User currentUser, string newPassword)
        {
            //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            command.CommandText = "UPDATE user SET password = '" + newPassword + "' WHERE username = '" + currentUser.username + "';";
            command.Prepare(); // Prüft auf SQL-Syntaxfehler oder Injektions
            int statusCode = command.ExecuteNonQuery(); // Führt die Abfrage an die Datenbank aus ohne das ein Result-Set zurück kommt.

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

        /// <summary>
        /// Überprüft ob der angebene User bereits in der Datenbank enthalten ist.
        /// </summary>
        /// <param name="user">Der gesuchte User vom Typ User</param>
        /// <returns>true falls User exestiert, andernfalls false</returns>
        public bool UserExist(User user)
        {
            return GetUser(user.username) != null;
        }

        //Checkt ob der User schon in der Datenbank ist (Username param)
        /// <summary>
        ///  Überprüft ob der angebene User bereits in der Datenbank enthalten ist.
        /// </summary>
        /// <param name="username">Der gesuchte Username vom Typ String</param>
        /// <returns></returns>
        public bool UserExist(string username)
        {
            return GetUser(username) != null;
        }

        /// <summary>
        /// Überprüft mittels eines ICMP Pings ob der angegebene Host zur verfügung steht.
        /// </summary>
        /// <param name="hostUri">Adresse des hosts</param>
        /// <param name="portNumber">Zielport des Hosts</param>
        /// <returns>true wenn der sever antwortet, andernfalls false.</returns>
        public static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Führt mehere SQL Befehle aus die sowohl die Datenbank als auch die User Tabelle mit dem Admin erstellen.
        /// </summary>
        public void CreateDatabase()
        {
            string script = Properties.Resources.pcverwaltung;

            // split script on GO command
            IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$",
                                     RegexOptions.Multiline | RegexOptions.IgnoreCase);

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                createConnection(ConnectionStringWithoutDatabase);
            }
            foreach (string commandString in commandStrings)
            {
                if (commandString.Trim() != "")
                {
                    command.CommandText = commandString;
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine("\nDatabase created!");
            Console.WriteLine("Table User created!");
            Console.WriteLine("Admin account created!");
            connection.Close();
            Console.WriteLine("trying to connect ...");
            bool status = connect() == 1;
            if (status)
            {
                Console.WriteLine("success!");
            }
            else
            {
                Console.WriteLine("failed");
            }
        }
        private void createConnection(string ConnectionString)
        {
            try
            {
                connection = new MySqlConnection(ConnectionString);
                command = connection.CreateCommand();
                connection.Open();
            }
            catch (MySqlException msqlex)
            {
                throw;
            }
        }
    } // endclass
} //endnamespace