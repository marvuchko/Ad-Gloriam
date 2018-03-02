using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ad_Gloriam.Model;
using Ad_Gloriam.View;

namespace Ad_Gloriam.Controller
{
    class ControllerHotseat:IController
    {
        #region Attributes
        private IModel _model;
        private IView _view;
        private bool _selected = false;
        private bool _over = false;
        #endregion

        #region Constructors
        public ControllerHotseat(IModel model, IView view)
        {
            this._model = model;
            this._view = view;
        }
        #endregion

        #region Members
        public void SendMessage(string message)
        {
            int player = _model.GetPlayer();
            _view.DisplayMessage("Player " + player + ": " + message);
        }

        public void CreateGame()
        {
            throw new NotImplementedException();
        }

        public bool JoinGame(GameItem gItem, string pwd)
        {
            throw new NotImplementedException();
        }

        public Point GetSelectedCoordinates()
        {
            return _model.GetSelectedCoordinates();
        }

        public Board.Field[,] GetTableFields()
        {
            return _model.GetTable();
        }

        public List<Point> GetAvailableFieldsCoordinates(int x, int y)
        {
            List<Point> fields = new List<Point>();
            for (int i = -2; i < 3; i++)
            {
                for (int j = -2; j < 3; j++)
                {
                    int newX = x + i;
                    int newY = y + j;
                    var f = _model.GetField(newX, newY);
                    if (f == Board.Field.Free)
                        fields.Add(new Point(newX, newY));
                }
            }
            return fields;
        }

        public void FieldClicked(int x, int y)
        {
            if (_over) return;
            Board.Field clickedField = _model.GetField(x, y);

            if (clickedField == (Board.Field)_model.GetPlayer())
            {
                _selected = true;
                _model.SetSelected(x, y);
            }
            else if (clickedField == Board.Field.Free)
            {
                if (!_selected) return;
                var coords = _model.GetSelectedCoordinates();
                int x2 = coords.X;
                int y2 = coords.Y;

                if (Math.Abs(x2 - x) > 2 || Math.Abs(y2 - y) > 2) return;

                _model.SetField(x, y);


                _model.SetSelected(-1, -1);
                _selected = false;

                if (Math.Abs(x2 - x) == 2 || Math.Abs(y2 - y) == 2)
                {
                    _model.ReleaseField(x2, y2);

                }

                Conversion(x, y);

                _model.NextPlayer();


                int res = CheckGameOver();
                if (_over)
                {
                    if (res == 1)
                    {
                        FormMessage fm = new FormMessage("Winner: Player 1!  Resault: " + _model.GetPointsPlayer1() + ":" + _model.GetPointsPlayer2());
                        fm.Opacity = 0;
                        fm.ShowDialog();
                    }
                    if (res == 2)
                    {
                        FormMessage fm = new FormMessage("Winner: Player 2!  Resault: " + _model.GetPointsPlayer1() + ":" + _model.GetPointsPlayer2());
                        fm.Opacity = 0;
                        fm.ShowDialog();
                    }
                    if (res == 0)
                    {
                        FormMessage fm = new FormMessage("Draw!");
                        fm.Opacity = 0;
                        fm.ShowDialog();
                    }
                }
            }

            _view.Redraw();
        }
        #endregion

        #region Methods

        private List<Point> GetSurroundingFields(int x, int y)
        {
            List<Point> fields = new List<Point>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int newX = x + i;
                    int newY = y + j;
                    if (newX > -1 && newX < 8 && newY > -1 && newY < 8)
                        fields.Add(new Point(newX, newY));
                }
            }
            return fields;
        }

        private int CheckGameOver()
        {
            int c1 = 0; //broj mogucih poteza za p1
            int c2 = 0; //broj mogucih poteza za p2

            int p1 = 0; //broj polja za p1
            int p2 = 0; //broj polja za p2

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var listMoves = GetAvailableFieldsCoordinates(i, j);
                    if (_model.GetField(i, j) == Board.Field.Player1)
                    {
                        c1 += listMoves.Count;
                        p1++;
                    }
                    if (_model.GetField(i, j) == Board.Field.Player2)
                    {
                        c2 += listMoves.Count;
                        p2++;
                    }
                }
            }

            if (c1 == 0)
            {
                p2 = 64 - p1;
                _over = true;
            }
            if (c2 == 0)
            {
                p1 = 64 - p2;
                _over = true;
            }

            _model.SetPointsPlayer1(p1);
            _model.SetPointsPlayer2(p2);

            _view.UpdatePoints(p1, p2);
            _view.UpdatePlayerTurn(_model.GetPlayer());

            if (p1 > p2) return 1;
            if (p1 < p2) return 2;

            return 0;
        }

        private void Conversion(int x, int y)
        {
            var surrounding = GetSurroundingFields(x, y);

            foreach (var point in surrounding)
            {
                var f = _model.GetField(point.X, point.Y);
                if (f != 0 && (int)f != _model.GetPlayer())
                {
                    _model.SetField(point.X, point.Y);
                }
            }
        }

        public List<GameItem> FindGames()
        {
            throw new NotImplementedException();
        }

        public void EndGame()
        {
            
        }

        #endregion

    }
}
