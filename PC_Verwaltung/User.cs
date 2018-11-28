using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Verwaltung
{
    class User
    {
        public string username { get; private set;}
        public string password { get; private set; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

    }
}
