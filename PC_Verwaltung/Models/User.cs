using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Verwaltung
{
    public class User
    {
        public string username { get; private set;}

        public string password { get; private set; }

        /// <summary>
        /// [!Zukünftige Funktion!]
        /// </summary>
        public bool canEditUser { get; private set; }

        /// <summary>
        /// [!Zukünftige Funktion!]
        /// </summary>
        public bool canEditRecords { get; private set; }

        /// <summary>
        /// [!Zukünftige Funktion!]
        /// </summary>
        public bool isAdmin { get; private set; }

        /* Alter Constructor 
        public User(string username, string password)
        {
            this.username = username;
            this.password = sha256(password);
        }
        */
        /// <summary>
        /// Erstellt ein neues User Objekt.
        /// </summary>
        /// <param name="username">Der Username des Users</param>
        /// <param name="password">Das Passwort (gehasht von der DB oder im Klartext von der Nutzereingabe)</param>
        /// <param name="hashPassword">true falls das PW gehast werden muss, false falls das PW bereits gehasht ist.</param>
        public User(string username, string password, Boolean hashPassword)
        {
            if(hashPassword)
            {
                this.username = username;
                this.password = sha256(password);
            }
            else
            {
                this.username = username;
                this.password = password;
            }
        }
        /// <summary>
        /// Hasht das Passwort mit sha256.
        /// </summary>
        /// <param name="s">Das Passwort das Gehast werden soll im Klartext</param>
        /// <returns></returns>
        public static string sha256(string s)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(s));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        
    }
}
