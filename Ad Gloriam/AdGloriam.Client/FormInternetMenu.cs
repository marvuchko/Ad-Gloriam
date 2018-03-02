using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ad_Gloriam
{
    public partial class FormInternetMenu : Form
    {
        private Form _parent;
        private int _xOffset, _yOffset;
        private bool _movable;
        private Timer _tCreate, _tJoin;
        private FormCreateGameInternet fci;
        private FormJoinGameInternet fji;

        public FormInternetMenu(Form _parent)
        {
            InitializeComponent();
            this.TransparencyKey = Color.FromArgb(31, 32, 33);
            this.BackColor = Color.FromArgb(31, 32, 33);
            this.Icon = Properties.Resources.logo1;
            this._parent = _parent;
            Location = _parent.Location;

            _movable = false;

            _tCreate = new Timer()
            {
                Interval = 5,
                Enabled = false
            };

            _tJoin = new Timer()
            {
                Interval = 5,
                Enabled = false
            };

            _tCreate.Tick += _tCreate_Tick;
            _tJoin.Tick += _tJoin_Tick;
        }

        private void _tJoin_Tick(object sender, EventArgs e)
        {
            if (fji.Opacity == 1)
            {
                _tJoin.Enabled = false;
                return;
            }

            fji.Opacity = 1;
            Hide();
        }

        private void _tCreate_Tick(object sender, EventArgs e)
        {
            if (fci.Opacity == 1)
            {
                _tCreate.Enabled = false;
                return;
            }

            fci.Opacity = 1;
            Hide();
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            fji = new FormJoinGameInternet(this);
            fji.Opacity = 0;
            fji.Show();

            _tJoin.Enabled = true;
        }

        private void btnCreateGame_Click(object sender, EventArgs e)
        {
            fci = new FormCreateGameInternet(this);
            fci.Opacity = 0;
            fci.Show();

            _tCreate.Enabled = true;
        }

        private void FormLanMenu_Load(object sender, EventArgs e)
        {
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
            Location = _parent.Location;
        }

        private void btnCreateGame_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width -= 5;
            ((Button)sender).Height -= 5;
            ((Button)sender).Top += 5;
        }

        private void btnCreateGame_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width += 5;
            ((Button)sender).Height += 5;
            ((Button)sender).Top -= 5;
        }

        private void btnCreateGame_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _parent.Location = Location;
            _parent.Show();
            this.Close();
        }

        private void FormLanMenu_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormLanMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void FormLanMenu_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }
    }
}
