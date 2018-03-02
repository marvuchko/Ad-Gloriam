using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ad_Gloriam.Model
{

    public class ModelGame:IModel
    {
        #region Attributes

        private Game _game;
        private int _selectedX;
        private int _selectedY;

        #endregion

        #region Constructors
        public ModelGame(string gameName="", string password="")
        {
            _game = new Game(gameName, password);
            _selectedY = -1;
            _selectedX = -1;
        }
        #endregion

        #region Members
        public void SetSelected(int x, int y)
        {
            _selectedX = x;
            _selectedY = y;
        }

        public Board.Field[,] GetTable()
        {
            return _game.Board.Fields;
        }

        public void SetPointsPlayer1(int value)
        {
            _game.Points[0] = value;
        }

        public void SetPointsPlayer2(int value)
        {
            _game.Points[1] = value;
        }

        public int GetPointsPlayer1()
        {
            return _game.Points[0];
        }

        public int GetPointsPlayer2()
        {
            return _game.Points[1];
        }

        public void ResetTable()
        {
            _game.Board.ResetBoard();
        }

        public Board.Field GetField(int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7) return Board.Field.Invalid;
            return _game.Board.Fields[x, y];
        }

        public void NextPlayer()
        {
            _game.OnTheMove ^= 3;
        }

        public int GetPlayer()
        {
            return _game.OnTheMove;
        }

        public void SetField(int x, int y)
        {
            int player = _game.OnTheMove;
            _game.Board.SetOwner(x, y, (Board.Field)player);
        }

        public void ReleaseField(int x, int y)
        {
            _game.Board.SetOwner(x,y, Board.Field.Free);
        }

        public Point GetSelectedCoordinates()
        {
            return new Point(_selectedX, _selectedY);
        }
        #endregion

        #region Methods

        public void PlayMove(int x, int y, Board.Field owner)
        {
            if (_game.Board.Fields[x, y] != Board.Field.Free)
            {
                MessageBox.Show("Field is not free!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _game.Board.SetOwner(x, y, owner);
        }

        public void ResetPoints()
        {
            SetPointsPlayer1(0);
            SetPointsPlayer2(0);
        }

        public Game GetGame()
        {
            return _game;
        }





        #endregion
    }
}
