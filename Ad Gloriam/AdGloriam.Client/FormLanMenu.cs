using System;
using System.Drawing;
using System.Windows.Forms;
using Ad_Gloriam.View;

namespace Ad_Gloriam
{
    public partial class FormLanMenu : Form
    {
        private Form _parent;
        private int _xOffset, _yOffset;
        private bool _movable;
        private Timer _tCreate, _tJoin;
        private FormCreateGame fc;
        private FormJoinGame fj;

        public FormLanMenu(Form _parent)
        {
            InitializeComponent();

            BackgroundImage = ResourceCollection.GetResourceByName("main_menu.png");
            btnBack.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_back.png");
            btnJoinGame.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_join_game.png");
            btnCreateGame.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_create_game.png");

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
            if(fj.Opacity == 1)
            {
                _tJoin.Enabled = false;
                return;
            }

            fj.Opacity = 1;
            Hide();
        }

        private void _tCreate_Tick(object sender, EventArgs e)
        {
            if(fc.Opacity == 1)
            {
                _tCreate.Enabled = false;
                return;
            }

            fc.Opacity = 1;
            Hide();
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            fj = new FormJoinGame(this);
            fj.Opacity = 0;
            fj.Show();

            _tJoin.Enabled = true;
        }

        private void btnCreateGame_Click(object sender, EventArgs e)
        {
            fc = new FormCreateGame(this);
            fc.Opacity = 0;
            fc.Show();

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
