using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AdGloriam.Server
{
    public class Game
    {
        #region Attributes
        private string _gameName;
        private string _password;
        private IPAddress _player1;
        private IPAddress _player2;
        private bool _joined;
        #endregion

        #region Properties
        public string GameName { get => _gameName; set => _gameName = value; }
        public string Password { get => _password; set => _password = value; }
        public IPAddress Player1 { get => _player1; set => _player1 = value; }
        public IPAddress Player2 { get => _player2; set => _player2 = value; }
        public bool Joined { get => _joined; set => _joined = value; }
        #endregion

        #region Constructors
        public Game(string gameName, string password, IPAddress player1, IPAddress player2 = null)
        {
            _gameName = gameName;
            _password = password;
            _player1 = player1;
            _player2 = player2;
            _joined = false;
        }
        #endregion        

        #region Methods
        public override string ToString()
        {
            return GameName + "-" + (Password != "");
        }
        #endregion
    }
}
