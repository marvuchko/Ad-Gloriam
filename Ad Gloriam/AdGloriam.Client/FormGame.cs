using System;
using System.Drawing;
using System.Windows.Forms;
using Ad_Gloriam.Controller;
using Ad_Gloriam.Model;
using Ad_Gloriam.View;

namespace Ad_Gloriam
{
    public partial class FormGame : Form, IView
    {
        #region Attributes
        private Button[,] _buttons;
        private IController _controller;
        private bool _movable;
        private int _xOffset, _yOffset;
        private Form _parent;
        private Timer _t;
        #endregion

        #region Properties
        public IController GameController
        {
            set { _controller = value; }
        }
        #endregion

        #region Constructors
        public FormGame(Form _parent)
        {
            Ad_Gloriam.Manager.AudioManager.PlayLoop(Audio.GameLoop);
            InitializeComponent();

            BackgroundImage = ResourceCollection.GetResourceByName("ad_gloriam_game_background.png");

            _buttons = new Button[8, 8];
            pnlGame.BackColor = Color.Transparent;
            Draw();

            TransparencyKey = Color.FromArgb(31, 32, 33);
            BackColor = Color.FromArgb(31, 32, 33);
            Icon = Properties.Resources.logo1;

            _movable = false;

            this._parent = _parent;

            _t = new Timer()
            {
                Enabled = true,
                Interval = 10
            };

            _t.Tick += _t_Tick;
        }

        private void _t_Tick(object sender, EventArgs e)
        {
            if(Opacity == 1)
            {
                _t.Enabled = false;
                return;
            }

            Opacity = 1;
            _parent.Hide();
        }
        #endregion

        #region Event Handlers
        private void btnSendMessage_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).Height -= 5;
            ((Button)sender).Width -= 5;
            ((Button)sender).Top += 5;
        }

        private void btnSendMessage_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).Height += 5;
            ((Button)sender).Width += 5;
            ((Button)sender).Top -= 5;
        }

        private void tbxChatbox_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)sender).Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void FormGame_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }

        private void FormGame_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string msg = tbxMessage.Text;
            if (String.IsNullOrEmpty(msg)) return;

            _controller.SendMessage(msg);
            tbxMessage.Clear();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void tbxMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendMessage.PerformClick();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            FormDialog fd = new FormDialog("Do you want to leave the game?");
            fd.Opacity = 0;
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _controller.EndGame();
                _parent.Show();
                this.Close();
            }
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
        
        }



        private void Button_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            int x = int.Parse(btn.Name[0].ToString());
            int y = int.Parse(btn.Name[1].ToString());
            _controller.FieldClicked(x, y);
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
        #endregion

        #region Methods

        public void EnableTimer(bool b)
        {
            _t.Enabled = true;
        }

        public void ShowAvailableFields(int x, int y)
        {
            var coordinates = _controller.GetAvailableFieldsCoordinates(x, y);
            Point selected = _controller.GetSelectedCoordinates();
            foreach (var point in coordinates)
            {
                if (point.X < 0 || point.X > 7 || point.Y < 0 || point.Y > 7) return;
                _buttons[point.X, point.Y].BackgroundImage = ResourceCollection.GetResourceByName("token_neutral_jump.png");

                if (point.X == selected.X + 2 || point.X == selected.X - 2 || point.Y == selected.Y + 2 || point.Y == selected.Y - 2)
                _buttons[point.X, point.Y].BackgroundImage = ResourceCollection.GetResourceByName("token_neutral_avaiable.png");
            }
        }

        private void Draw()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _buttons[i, j] = new Button();
                    _buttons[i, j].Size = new Size(65, 65);
                    _buttons[i, j].Location = new Point(i * 80, j * 80);
                    _buttons[i, j].Name = i + "" + j;
                    _buttons[i, j].KeyDown += Button_KeyDown;
                    _buttons[i, j].TabStop = false;
                    _buttons[i, j].Click += Button_Click;
                    _buttons[i, j].MouseEnter += Button_MouseEnter;
                    _buttons[i, j].MouseLeave += Button_MouseLeave;
                    _buttons[i, j].MouseMove += Button_MouseMove;
                    _buttons[i, j].FlatStyle = FlatStyle.Flat;
                    _buttons[i, j].FlatAppearance.CheckedBackColor = Color.Transparent;
                    _buttons[i, j].FlatAppearance.BorderSize = 0;
                    _buttons[i, j].FlatAppearance.MouseDownBackColor = Color.Transparent;
                    _buttons[i, j].FlatAppearance.MouseOverBackColor = Color.Transparent;
                    _buttons[i, j].BackgroundImage = ResourceCollection.GetResourceByName("token_neutral.png");
                    _buttons[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    pnlGame.Controls.Add(_buttons[i, j]);
                }
            }

            _buttons[0, 0].BackgroundImage = ResourceCollection.GetResourceByName("token_player1.png");
            _buttons[7, 0].BackgroundImage = ResourceCollection.GetResourceByName("token_player1.png");
            _buttons[0, 7].BackgroundImage = ResourceCollection.GetResourceByName("token_player2.png");
            _buttons[7, 7].BackgroundImage = ResourceCollection.GetResourceByName("token_player2.png");
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            ((Button) sender).Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }
        #endregion

        #region Members
        public void GameOver(int winner)
        {
            throw new NotImplementedException();
        }

        public void UpdatePoints(int points1, int points2)
        {
            lblScore.Text = "Score  " + points1.ToString() + " : " + points2.ToString();
        }

        public void DisplayMessage(string message)
        {
            if (String.IsNullOrEmpty(message)) return;

            tbxChatbox.AppendText(message + Environment.NewLine);
        }

        public void Redraw()
        {
            Board.Field[,] table = _controller.GetTableFields();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (table[i, j] == Board.Field.Player1)
                    {
                        _buttons[i, j].BackgroundImage = ResourceCollection.GetResourceByName("token_player1.png");
                    }
                    if (table[i, j] == Board.Field.Player2)
                    {
                        _buttons[i, j].BackgroundImage = ResourceCollection.GetResourceByName("token_player2.png");
                    }
                    if (table[i, j] == Board.Field.Free)
                    {
                        _buttons[i, j].BackgroundImage = ResourceCollection.GetResourceByName("token_neutral.png");
                    }
                }
            }
            var coords = _controller.GetSelectedCoordinates();
            int x = coords.X;
            int y = coords.Y;
            if (x == -1 || y == -1) return;

            ShowAvailableFields(x, y);
            if(table[x, y] == Board.Field.Player1)
                _buttons[x, y].BackgroundImage = _buttons[x, y].BackgroundImage = ResourceCollection.GetResourceByName("token_player1_selected.png");
            else if (table[x, y] == Board.Field.Player2)
                _buttons[x, y].BackgroundImage = _buttons[x, y].BackgroundImage = ResourceCollection.GetResourceByName("token_player2_selected.png");
        }

        public void UpdatePlayerTurn(int player)
        {
            lblOnTheMove.Text = "Playing: player " + player;
        }
        #endregion

 
    }
}
