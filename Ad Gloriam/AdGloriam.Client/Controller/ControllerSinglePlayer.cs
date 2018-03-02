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
    class ControllerSinglePlayer:IController
    {
        #region Attributes
        private IModel _model;
        private IView _view;
        private bool _selected = false;
        private bool _over = false;
        private const int DEPTH = 3;
        private Timer _t;
        #endregion

        #region Constructors
        public ControllerSinglePlayer(IModel model, IView view)
        {
            this._model = model;
            this._view = view;

            _t = new Timer()
            {
                Enabled = false
            };

            _t.Tick += _t_Tick;
            _t.Interval = 1000;
        }

        #endregion

        #region Members
        public void SendMessage(string message)
        {
            int player = _model.GetPlayer();
            if(player == 1) _view.DisplayMessage("Player: " + message);
            else _view.DisplayMessage("Computer: " + message);
        }

        public void CreateGame()
        {
            _model.ResetTable();
            _model.ResetPoints();
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
            if (_model.GetPlayer() == 2) return;

            if (_model.GetSelectedCoordinates().X != -1 && _model.GetSelectedCoordinates().Y != -1 && x == _model.GetSelectedCoordinates().X && y == _model.GetSelectedCoordinates().Y) {
                _model.SetSelected(-1, -1);
                _view.Redraw();
                _selected = false;
                return;
            } 

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
                _t.Enabled = true;
            }
            _view.Redraw();
            _view.UpdatePoints(_model.GetPointsPlayer1(), _model.GetPointsPlayer2());
            _view.UpdatePlayerTurn(_model.GetPlayer());
        }
        #endregion

        #region Methods

        private int Evaluation(int[,] table) //Todo: evaluate specific situations
        {
            int ev = 0;
            int p1 = 0;
            int p2 = 0;
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (table[i, j] == 2) p2++;
                    if (table[i, j] == 1) p1++;
                }
            }
            ev = (p2 - p1);
            if (p1 == 0) ev += 32000; //ai win
            if (p2 == 0) ev -= 32000; //ai loose

            return ev;
        }

        private void MoveAi()
        {
            if (_over) return;
            var table = new int[8, 8];
            var fields = _model.GetTable();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    table[i, j] = (int) fields[i, j];
                }
            }
            var move = AlphaBeta(table, DEPTH, int.MinValue, int.MaxValue, 2);

            _model.SetField(move.X, move.Y);
            if (Math.Abs(move.OldX - move.X) == 2 || Math.Abs(move.OldY - move.Y) == 2)
            {
                _model.ReleaseField(move.OldX, move.OldY);
            }

            Conversion(move.X, move.Y);
            _model.NextPlayer();

            int res = CheckGameOver();
            if(_over)
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
                    _model.SetField(point.X,point.Y);
                }
            }
        }

        private Move AlphaBeta(int[,] table, int depth, int alpha, int beta, int max)
        {
            var moves = MovesGenerator(table, max);
            if (depth == 0 || moves.Count == 0)
            {
                int evaluation = Evaluation(table);
                var move = new Move() { Value = evaluation };
                return move;
            }

            Move bestMove = new Move();
            if (max == 2)
            {
                bestMove.Value = -50000;
                foreach (var move in moves)
                {
                    //make a move
                    table[move.X, move.Y] = 2;
                    if (Math.Abs(move.OldX - move.X) == 2 || Math.Abs(move.OldY - move.Y) == 2)
                    {
                        table[move.OldX, move.OldY] = 0;
                    }
                    //convert
                    var surrounding = GetSurroundingFields(move.X, move.Y);
                    var converted = new List<Point>();
                    foreach (var point in surrounding)
                    {
                        var f = table[point.X, point.Y];
                        if (f == 1)
                        {
                            table[point.X, point.Y] = 2;
                            converted.Add(point);
                        }
                    }

                    var tmpMove = AlphaBeta(table, depth - 1, alpha, beta, 1);
                    move.Value = tmpMove.Value;

                    if (move.Value > bestMove.Value)
                    {
                        bestMove = move;
                    }

                    //deconvert
                    foreach (var point in converted)
                    {
                        table[point.X, point.Y] = 1;
                    }

                    //undo a move
                    table[move.X, move.Y] = 0;
                    if (Math.Abs(move.OldX - move.X) == 2 || Math.Abs(move.OldY - move.Y) == 2)
                    {
                        table[move.OldX, move.OldY] = 2;
                    }

                    alpha = Math.Max(alpha, bestMove.Value);
                    if (alpha >= beta) break;
                }
                return bestMove;
            }
            else
            {
                bestMove.Value = 50000;
                foreach (var move in moves)
                {
                    //make a move
                    table[move.X, move.Y] = 1;
                    if (Math.Abs(move.OldX - move.X) == 2 || Math.Abs(move.OldY - move.Y) == 2)
                    {
                        table[move.OldX, move.OldY] = 0;
                    }
                    //convert
                    var surrounding = GetSurroundingFields(move.X, move.Y);
                    var converted = new List<Point>();
                    foreach (var point in surrounding)
                    {
                        var f = table[point.X, point.Y];
                        if (f == 2)
                        {
                            table[point.X, point.Y] = 1;
                            converted.Add(point);
                        }
                    }

                    var tmpMove = AlphaBeta(table, depth - 1, alpha, beta, 2);
                    move.Value = tmpMove.Value;

                    if (move.Value < bestMove.Value)
                    {
                        bestMove = move;
                    }

                    //deconvert
                    foreach (var point in converted)
                    {
                        table[point.X, point.Y] = 2;
                    }
                    //undo a move
                    
                    table[move.X, move.Y] = 0;
                    if (Math.Abs(move.OldX - move.X) == 2 || Math.Abs(move.OldY - move.Y) == 2)
                    {
                        table[move.OldX, move.OldY] = 1;
                    }

                    beta = Math.Min(beta, bestMove.Value);
                    if (alpha >= beta) break;
                }
                return bestMove;
            }
            
        }

        private List<Move> MovesGenerator(int[,] table, int player)
        {
            List<Move> moves = new List<Move>();
           
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (table[i, j] == player)
                    {
                        int ox = i;
                        int oy = j;

                        var coords = GetAvailableFieldsCoordinates(ox, oy);

                        moves.AddRange(coords.Select(coord => new Move() {OldX = ox, OldY = oy, X = coord.X, Y = coord.Y}));
                    }
                }
            }
            return moves;
        }

        public List<GameItem> FindGames()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Handlers
        private void _t_Tick(object sender, EventArgs e)
        {
            MoveAi();

            _view.Redraw();
            _view.UpdatePoints(_model.GetPointsPlayer1(), _model.GetPointsPlayer2());
            _view.UpdatePlayerTurn(_model.GetPlayer());

            _t.Enabled = false;
        }
        #endregion

        public void EndGame()
        {

        }
    }
}
