using System;
using Ad_Gloriam.View;
using System.Drawing;
using System.Windows.Forms;
using Ad_Gloriam.Controller;
using Ad_Gloriam.Model;

namespace Ad_Gloriam
{
    public partial class FormMultiPlayerMenu : Form
    {
        private FormMainMenu _parent;
        private int _xOffset, _yOffset;
        private bool _movable;
        private Timer _tHotseat, _tLan, _tInternet;
        private FormGame fg;
        private FormLanMenu f;
        private FormInternetMenu fi;

        public FormMultiPlayerMenu(FormMainMenu _parent)
        {
            InitializeComponent();

            BackgroundImage = ResourceCollection.GetResourceByName("main_menu.png");
            btnHotseat.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_hot_seat.png");
            btnLan.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_lan.png");
            btnInternet.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_internet.png");
            button1.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_back.png");

            this.TransparencyKey = Color.FromArgb(31, 32, 33);
            this.BackColor = Color.FromArgb(31, 32, 33);
            this.Icon = Properties.Resources.logo1;
            this._parent = _parent;

            _movable = false;

            _tHotseat = new Timer()
            {
                Interval = 5,
                Enabled = false
            };

            _tLan = new Timer()
            {
                Interval = 5,
                Enabled = false
            };

            _tInternet = new Timer()
            {
                Interval = 5,
                Enabled = false
            };

            _tHotseat.Tick += _tHotseat_Tick;
            _tLan.Tick += _tLan_Tick;
            _tInternet.Tick += _tInternet_Tick;
        }

        private void _tInternet_Tick(object sender, EventArgs e)
        {
            if(fi.Opacity == 1)
            {
                _tInternet.Enabled = false;
                return;
            }

            fi.Opacity = 1;
            this.Hide();
        }

        private void _tLan_Tick(object sender, EventArgs e)
        {
            if (f.Opacity == 1)
            {
                _tLan.Enabled = false;
                return;
            }

            f.Opacity = 1;
            this.Hide();
        }

        private void _tHotseat_Tick(object sender, EventArgs e)
        {
            if(fg.Opacity == 1)
            {
                _tHotseat.Enabled = false;
                return;
            }

            fg.Opacity = 1;
            this.Hide();
        }

        private void btnHotseat_Click(object sender, EventArgs e)
        {
            IModel model = new ModelGame();
            fg = new FormGame(this);
            IController ctrl = new ControllerHotseat(model, fg);
            fg.GameController = ctrl;
            fg.Opacity = 0;

            fg.Show();

            _tHotseat.Enabled = true;
        }

        private void btnLan_Click(object sender, EventArgs e)
        {
            f = new FormLanMenu(this);
            f.Opacity = 0;
            f.Show();

            _tLan.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _parent.Location = Location;
            _parent.Show();
            this.Close();
        }

        private void FormMultiPlayerMenu_Load(object sender, EventArgs e)
        {
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
            Location = _parent.Location;
        }

        private void btnHotseat_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width -= 5;
            ((Button)sender).Height -= 5;
            ((Button)sender).Top += 5;
        }

        private void btnHotseat_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width += 5;
            ((Button)sender).Height += 5;
            ((Button)sender).Top -= 5;
        }

        private void FormMultiPlayerMenu_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }

        private void FormMultiPlayerMenu_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormMultiPlayerMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }
        }

        private void btnInternet_Click(object sender, EventArgs e)
        {
            fi = new FormInternetMenu(this);
            fi.Opacity = 0;
            fi.Show();

            _tInternet.Enabled = true;
        }

        private void btnHotseat_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }
    }
}
