using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ad_Gloriam
{
    public partial class FormMessage : Form
    {
        private int _xOffset, _yOffset;
        private bool _movable;
        private Timer _t;

        public FormMessage(string text)
        {
            InitializeComponent();
            Icon = Properties.Resources.logo1;
            Cursor = new Cursor(Properties.Resources.cursor.Handle);

            lblMessage.Text = text;

            TransparencyKey = Color.FromArgb(31, 32, 33);
            BackColor = Color.FromArgb(31, 32, 33);

            _movable = false;

            _t = new Timer()
            {
                Interval = 5,
                Enabled = true
            };

            _t.Tick += _t_Tick;

            lblMessage.Location = new Point(Location.X + (Width - lblMessage.Width) / 2, lblMessage.Location.Y);

        }

        private void _t_Tick(object sender, EventArgs e)
        {
            if(Opacity == 1)
            {
                _t.Enabled = false;
                return;
            }

            Opacity = 1;
        }

        private void FormMessage_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }

        private void FormMessage_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormMessage_MouseMove(object sender, MouseEventArgs e)
        {
            if(_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }

            Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void btnOk_MouseEnter(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
