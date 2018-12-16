using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerInformation
{
    public class WindowsUser
    {
        public string Username { get; private set; }
        public bool IsActive { get; private set; }
        public bool PasswordRequired { get; private set; }
        public string Domain { get; private set; }
        public bool LocalProfile { get; private set; }

        public WindowsUser(string username, string domain, bool isActive, bool LocalProfile)
        {
            this.Username = username;
            this.Domain = domain;
            this.IsActive = isActive;
            this.LocalProfile = LocalProfile;
        }
    }
}
