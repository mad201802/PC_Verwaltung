using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PC_Verwaltung
{
    class Database
    {

        private string ConnectionString;

        public Database(string server, string database, string uid, string password)
        {
            ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        private bool connect()
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

    }
}
