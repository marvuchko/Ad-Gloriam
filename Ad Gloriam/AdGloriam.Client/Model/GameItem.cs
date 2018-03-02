using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad_Gloriam.Model
{
    public class GameItem
    {
        private string _gameName;
        private string _ipAddress;
        private string _passwordProtected;

        public string GameName { get => _gameName; set => _gameName = value; }
        public string IpAddress { get => _ipAddress; set => _ipAddress = value; }
        public string PasswordProtected { get => _passwordProtected; set => _passwordProtected = value; }

        public GameItem(string gameName, string ipAddress, string passwordProtected)
        {
            GameName = gameName;
            IpAddress = ipAddress;
            PasswordProtected = passwordProtected;
        }


    }
}
