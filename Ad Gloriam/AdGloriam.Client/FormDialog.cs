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
    public partial class FormDialog : Form
    {
        private int _xOffset, _yOffset;
        private bool _movable;
        private Timer _t;

        public FormDialog(string message)
        {
            InitializeComponent();
            Cursor = new Cursor(Properties.Resources.cursor.Handle);
            Icon = Properties.Resources.logo1;

            TransparencyKey = Color.FromArgb(31, 32, 33);
            BackColor = Color.FromArgb(31, 32, 33);

            _movable = false;

            _t = new Timer()
            {
                Enabled = true,
                Interval = 10
            };

            lblMessage.Text = message;

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
        }

        private void FormDialog_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void FormDialog_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormDialog_MouseMove(object sender, MouseEventArgs e)
        {
            if(_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }

            Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormDialog_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void FormDialog_MouseEnter(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void FormDialog_MouseDown_1(object sender, MouseEventArgs e)
        {

        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
