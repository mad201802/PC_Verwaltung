﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TestApp
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
        public Database()
        {
            string server = ConfigurationManager.AppSettings.Get("DatabaseServer");
            string database = ConfigurationManager.AppSettings.Get("DBname");
            string uid = ConfigurationManager.AppSettings.Get("DBUser");
            string password = ConfigurationManager.AppSettings.Get("DBPassword");

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
                catch (Exception)
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
                    try
                    {
                        Reader.Read();
                        string name = "", surname = "", password, email = "";

                        username = Reader.GetValue(Reader.GetOrdinal("username")) != DBNull.Value ? Reader.GetString(Reader.GetOrdinal("username")) : null;
                        password = Reader.GetValue(Reader.GetOrdinal("password")) != DBNull.Value ? Reader.GetString(Reader.GetOrdinal("password")) : null;

                        try { name = Reader.GetValue(Reader.GetOrdinal("name")) != DBNull.Value ? Reader.GetString(Reader.GetOrdinal("name")) : null; }
                        catch (IndexOutOfRangeException) {}
                        try { surname = Reader.GetValue(Reader.GetOrdinal("surname")) != DBNull.Value ? Reader.GetString(Reader.GetOrdinal("surname")) : null; }
                        catch (IndexOutOfRangeException) {}
                        try { email = Reader.GetValue(Reader.GetOrdinal("email")) != DBNull.Value ? Reader.GetString(Reader.GetOrdinal("email")) : null; }
                        catch (IndexOutOfRangeException) {}

                        User u = new User(name, surname, username, email, password, false);
                        connection.Close();
                        return u;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Erstellt ein neuen User in der Datenbank
        /// </summary>
        /// <param name="NewUser">Der User der erstellt werden soll.</param>
        /// <returns></returns>
        public bool createNewUser(User NewUser)
        {
            //validierung
            if(UserExist(NewUser.username))
            {
                throw new ArgumentException("User existiert bereits!");
            }
            if (String.IsNullOrEmpty(NewUser.username)||String.IsNullOrEmpty(NewUser.password))
            {
                throw new ArgumentException("Username or Password is null!");
            }
            if(!Regex.IsMatch(NewUser.password, "^[0-9a-fA-F]{64}$", RegexOptions.Compiled))
            {
                throw new ArgumentException("Passwort ungültig!");
            }

            // Parsing zu SQL
            string username, password, name, surname, email;
            username = NewUser.username;
            password = NewUser.password;
            name = ParseToSQLValues(NewUser.name);
            surname = ParseToSQLValues(NewUser.surname);
            email = ParseToSQLValues(NewUser.email);

            //erstellen des SQL statements
            command.CommandText = "INSERT INTO user(username, password, name, surname, email)" +
            "VALUES('" + username + "', '" + password + "', " + name + ", " + surname + ", " + email + ");";
            //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            command.Prepare(); // Prüft auf SQL-Syntaxfehler oder Injektions
            int result = command.ExecuteNonQuery(); // Führt die Abfrage an die Datenbank aus ohne das ein Result-Set zurück kommt.

            connection.Close();
            return result == 1;
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
            
            if(newPassword == null || newPassword.Equals(string.Empty))
            {
                throw new ArgumentException("Passwort ungültig!");
            }
            if(!UserExist(currentUser))
            {
                throw new ArgumentException("User nicht gefunden!");
            }

            command.CommandText = "UPDATE user SET password = '" + newPassword + "' WHERE username = '" + currentUser.username + "';";
            //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
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
        /// Ändert Stammdaten des Users in der Datenbank
        /// ändert NICHT Username und Password!
        /// </summary>
        /// <param name="user">der zu updatened User</param>
        /// <returns>true wenn erfolg</returns>
        public bool updateUser(User user)
        {
            
            //validierung
            if (user.username == null || user.password == null)
            {
                throw new ArgumentException("Username or Password is null!");
            }
            if(!UserExist(user))
            {
                throw new ArgumentException("User nicht gefunden!");
            }

            // Parsing zu SQL
            string name, surname, email;
            name = ParseToSQLValues(user.name);
            surname = ParseToSQLValues(user.surname);
            email = ParseToSQLValues(user.email);

            //erstellen des SQL statements
            command.CommandText = "UPDATE user"+
                "SET name = '" + name + "' , surname = '" + surname + "' , email = '" + email + 
                "' WHERE username = '" + user.username + 
                " AND password = '" + user.password + "';";

            //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            command.Prepare(); // Prüft auf SQL-Syntaxfehler oder Injektions
            int result = command.ExecuteNonQuery(); // Führt die Abfrage an die Datenbank aus ohne das ein Result-Set zurück kommt.

            connection.Close();
            return result == 1;
        }

        public bool deleteUser(User user)
        {
            //validierung
            if (user.username == null || user.password == null)
            {
                throw new ArgumentException("Username or Password is null!");
            }
            if (!UserExist(user))
            {
                throw new ArgumentException("User nicht gefunden!");
            }

            //erstellen des SQL statements
            command.CommandText = "DELETE FROM user WHERE username = " + ParseToSQLValues(user.username) + ";";

            //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            command.Prepare(); // Prüft auf SQL-Syntaxfehler oder Injektions
            int result = command.ExecuteNonQuery(); // Führt die Abfrage an die Datenbank aus ohne das ein Result-Set zurück kommt.

            connection.Close();
            return result == 1;
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
            catch (SocketException)
            {
                return false;
            }
        }

        public string[] getCPUs()
        {
            List<string> CPUs = new List<string>();
            try
            {
                //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.CommandText = "SELECT processor.ID, hersteller.NAME AS 'Hersteller', processor.NAME AS 'Name', ROUND(processor.StockClock / 1000, 1) AS 'Defaultclock in GHz', sockel.NAME AS 'Sockel' " +
                "FROM processor " + 
                "LEFT JOIN hersteller ON hersteller.ID = processor.Hersteller " +
                "LEFT JOIN sockel ON sockel.ID = processor.Sockel ";
                MySqlDataReader Reader;
                command.Prepare(); // Prüft auf SQL-Syntaxfehler oder Injektions
                Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    try
                    {
                        while (Reader.Read())
                        { 
                            CPUs.Add( Reader.GetValue(1).ToString() + " " + Reader.GetValue(2).ToString() );
                        }
                        connection.Close();
                        string[] result = CPUs.ToArray();
                        return result;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                {
                    connection.Close();
                    Console.WriteLine("Reader has no results");
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string[] getGPUs()
        {
            List<string> CPUs = new List<string>();
            try
            {
                //Überprüft ob die Verbindung zur DB offen ist, falls nein, öffnet diese.
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.CommandText = "SELECT processor.ID, hersteller.NAME AS 'Hersteller', processor.NAME AS 'Name', ROUND(processor.StockClock / 1000, 1) AS 'Defaultclock in GHz', sockel.NAME AS 'Sockel' " +
                "FROM processor " +
                "LEFT JOIN hersteller ON hersteller.ID = processor.Hersteller " +
                "LEFT JOIN sockel ON sockel.ID = processor.Sockel ";
                MySqlDataReader Reader;
                command.Prepare(); // Prüft auf SQL-Syntaxfehler oder Injektions
                Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    try
                    {
                        while (Reader.Read())
                        {
                            CPUs.Add(Reader.GetValue(1).ToString() + " " + Reader.GetValue(2).ToString() + " @" + Reader.GetValue(3).ToString() + "GHz");
                        }
                        connection.Close();
                        string[] result = CPUs.ToArray();
                        return result;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                {
                    connection.Close();
                    Console.WriteLine("Reader has no results");
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
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
            catch (MySqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Parst die Werte in ihre entspechende SQL werte. 
        /// Unterstüzt string, int, bool
        /// </summary>
        /// <param name="value">wert der convertiert werden soll als string, int oder bool</param>
        /// <returns>Das entsprechende SQL value als string für INSERT Statement</returns>
        private string ParseToSQLValues(object value)
        {
            if(value.GetType().Equals(typeof(string)))
            {
                string a = (string)value;
                string output = value == null ? "NULL" : "'" + a + "'";
                return output;
            }
            else if(value.GetType().Equals(typeof(int)))
            {
                int a = (int) value;
                string output = value == null ? "NULL" : a.ToString();
                return output;
            }
            else if(value.GetType().Equals(typeof(bool)))
            {
                int a = (bool)value ? 1 : 0;
                string output = value == null ? "NULL" : a.ToString();
                return output;
            }
            else
            {
                throw new ArgumentException("eins Enton ist verwirrt");
            }
        }
    } // endclass
} //endnamespace