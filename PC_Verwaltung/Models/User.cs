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

        public bool canEditUser { get; private set; }

        public bool canEditRecords { get; private set; }

        public bool isAdmin { get; private set; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = sha256(password);
        }

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

        public void setHashPassword(string hash)
        {
            this.password = hash;
        }

    }
}
