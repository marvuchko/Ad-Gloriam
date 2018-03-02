using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad_Gloriam.Model
{
    public class Game
    {
        private Player[] _players;
        private int[] _points;
        private int _winner;
        private int _onTheMove;
        private Board _board;
        private string _gameName;
        private string _password;

        public int[] Points { get => _points; set => _points = value; }
        public int Winner { get => _winner; set => _winner = value; }
        public int OnTheMove { get => _onTheMove; set => _onTheMove = value; }
        public Board Board { get => _board; set => _board = value; }
        public string GameName { get => _gameName; set => _gameName = value; }
        public string Password { get => _password; set => _password = value; }

        public Game(string gameName="", string password="")
        {
            ResetGame();
            GameName = gameName;
            Password = password;
        }       

        public void ResetGame()
        {
            Board = new Board();
            //players...
            Points = new int[2];
            Points[0] = 2;
            Points[1] = 2;
            Winner = 0;
            OnTheMove = 1;
        }
    }
}
