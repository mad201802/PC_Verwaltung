using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Verwaltung
{
    public class User
    {
        public string name { get; private set; }

        public string surname { get; private set; }
        
        /// <summary>
        /// Nicht veränderbar!
        /// </summary>
        public string username { get; }

        public string email { get; private set; }

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
      
        /// <summary>
        /// Erstellt ein neues User Objekt.
        /// </summary>
        /// <param name="username">Der Username des Users</param>
        /// <param name="password">Das Passwort (gehasht von der DB oder im Klartext von der Nutzereingabe)</param>
        /// <param name="hashPassword">true falls das PW gehast werden muss, false falls das PW bereits gehasht ist.</param>
        /// <exception cref="ArgumentException">falls das passwort kein Hash ist aber als solches angegeben wurde.</exception>
        public User(string name, string surname, string username, string email, string password, Boolean hashPassword)
        {
            if (hashPassword)
            {
                this.name = name;
                this.surname = surname;
                this.username = username;
                this.email = email;
                this.password = sha256(password);
            }
            else
            {
                if (!Regex.IsMatch(password, "^[0-9a-fA-F]{64}$", RegexOptions.Compiled))
                {
                    throw new ArgumentException("Passwort ungültig, es wird ein gehastes Passwort erwartet.");
                }
                this.name = name;
                this.surname = surname;
                this.username = username;
                this.email = email;
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
