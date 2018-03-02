using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ad_Gloriam.View;

namespace Ad_Gloriam
{
    public partial class FormOptionsMenu : Form
    {
        private Form _parent;
        private int _xOffset, _yOffset;
        private bool _movable;
        private Timer _t;

        public FormOptionsMenu(Form _parent)
        {
            InitializeComponent();

            TransparencyKey = Color.FromArgb(31, 32, 33);
            BackColor = Color.FromArgb(31, 32, 33);

            string image = "";
            string label = "Audio: ";
            if(Ad_Gloriam.Manager.AudioManager.Enabled)
            {
                image = "soundEnabled.png";
                label += "ON";
            }
            else
            {
                image = "soundDisable.png";
                label += "OFF";
            }

            btnAudio.BackgroundImage = ResourceCollection.GetResourceByName(image);
            lblAudio.Text = label;

            this._parent = _parent;
            Location = new Point(_parent.Location.X, _parent.Location.Y);

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

        private void FormOptionsMenu_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }

        private void FormOptionsMenu_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormOptionsMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if(_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }

            Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void btnChangeTheme_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width -= 5;
            ((Button)sender).Height -= 5;
            ((Button)sender).Top += 5;
        }

        private void btnChangeTheme_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width += 5;
            ((Button)sender).Height += 5;
            ((Button)sender).Top -= 5;
        }

        private void btnChangeTheme_Click(object sender, EventArgs e)
        {
            FormChangeThemes fc = new FormChangeThemes(this);
            fc.Opacity = 0;
            fc.ShowDialog();
        }

        private void btnAudio_Click(object sender, EventArgs e)
        {
            bool oldStatus = Ad_Gloriam.Manager.AudioManager.Enabled;

            Ad_Gloriam.Manager.AudioManager.Enabled = !oldStatus;

            if (btnAudio.BackgroundImage.Equals(ResourceCollection.GetResourceByName("soundEnabled.png")))
            {
                btnAudio.BackgroundImage = ResourceCollection.GetResourceByName("soundDisable.png");
                lblAudio.Text = "Audio: OFF";
                Ad_Gloriam.Manager.AudioManager.StopLoop();
            }
            else
            {
                btnAudio.BackgroundImage = ResourceCollection.GetResourceByName("soundEnabled.png");
                Ad_Gloriam.Manager.AudioManager.PlayLoop(Ad_Gloriam.Model.Audio.MainTheme);
                lblAudio.Text = "Audio: ON";
            }

        }

        private void FormOptionsMenu_Load(object sender, EventArgs e)
        {
            Location = new Point(_parent.Location.X, _parent.Location.Y);
            Cursor = new Cursor(Properties.Resources.cursor.Handle);
            Icon = Properties.Resources.logo1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _parent.Location = new Point(Location.X, Location.Y);
            _parent.Show();
            Close();
        }
    }
}
