using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ad_Gloriam.Model;
using Ad_Gloriam.Controller;
using Ad_Gloriam.View;

namespace Ad_Gloriam
{
    public partial class FormCreateGameInternet : Form
    {

        private Form _parent;
        private int _xOffset, _yOffset;
        private bool _movable;
        private String[] _gameNames = {"Alpha", "Beta", "Gama", "Delta", "Epsilon", "Zeta", "Eta", "Theta",
        "Mi", "Ni", "Ksi", "Omicron", "Pi", "Rho", "Sigma", "Thau", "Ipsilon", "Phi", "Hi", "Psi", "Omega"};
        private List<String> _names;
        private int _selectedIndex;

        public FormCreateGameInternet(Form _parent)
        {
            InitializeComponent();

            BackgroundImage = ResourceCollection.GetResourceByName("menu_create_game.png");
            btnCancel.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_back.png");
            btnCreateGame.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_create_game.png");

            this.TransparencyKey = Color.FromArgb(31, 32, 33);
            this.BackColor = Color.FromArgb(31, 32, 33);
            this._parent = _parent;
            this.Icon = Properties.Resources.logo1;

            tbxGameName.BackColor = Color.FromArgb(179, 179, 179);
            tbxGamePassword.BackColor = Color.FromArgb(179, 179, 179);

            _movable = false;

            _names = new List<string>();

            Random r = new Random();
            for (int i = 0; i < 10000; i++)
            {
                _names.Add(_gameNames[r.Next(0, _gameNames.Length - 1)] + i);
            }

        }

        private void btnCreateGame_Click(object sender, EventArgs e)
        {
            string gameName = tbxGameName.Text;
            string password = tbxGamePassword.Text;

            if(String.IsNullOrEmpty(gameName))
            {
                FormMessage fm = new FormMessage("Game name must be specified!");
                fm.Opacity = 0;
                fm.ShowDialog();
                tbxGameName.Focus();
                return;
            }

            if (_selectedIndex >= 0) _names.Remove(_names[_selectedIndex]);

            FormGame f = new FormGame(this);
            f.Opacity = 0;
            IModel m = new ModelGame(gameName, password);
            IController c = new ControllerInternet(m, (IView)f);
            f.GameController = c;
            c.CreateGame();
            f.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _parent.Show();
            this.Close();
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

        private void FormCreateGame_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }

        private void FormCreateGame_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormCreateGame_MouseMove(object sender, MouseEventArgs e)
        {
            if(_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void btnRandomName_Click(object sender, EventArgs e)
        {
            if (_names.Count == 0) _selectedIndex = -1;
            Random r = new Random();
            _selectedIndex = r.Next(0, _names.Count - 1);
            tbxGameName.Text = _names[_selectedIndex];
        }

        private void tbxGameName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FormCreateGame_Load(object sender, EventArgs e)
        {

        }
    }
}
