using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ad_Gloriam.Controller;
using Ad_Gloriam.Model;
using Ad_Gloriam.View;

namespace Ad_Gloriam
{
    public partial class FormJoinGameInternet : Form
    {
        private Thread searchT;
        private FormGame fg;
        private IModel model;
        private IController ctrl;
        private bool _movable;
        private int _xOffset, _yOffset;
        private Form _parent;

        public FormJoinGameInternet(Form _parent)
        {
            InitializeComponent();

            BackgroundImage = ResourceCollection.GetResourceByName("menu_game_lobby.png");
            btnCancel.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_back.png");
            btnJoinGame.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_join_game.png");

            SetColumnsToDgv();

            this.TransparencyKey = Color.FromArgb(31, 32, 33);
            this.BackColor = Color.FromArgb(31, 32, 33);

            Control.CheckForIllegalCrossThreadCalls = false;

            Cursor = new Cursor(Properties.Resources.cursor.Handle);

            fg = new FormGame(this);
            model = new ModelGame();
            ctrl = new ControllerInternet(model, fg);
            fg.GameController = ctrl;

            _movable = false;
            this._parent = _parent;

            FindGames();
        }

        void SetColumnsToDgv()
        {
            dgvGames.ColumnCount = 4;
            dgvGames.Columns[0].Name = "Game name";
            dgvGames.Columns[1].Name = "IP address";
            dgvGames.Columns[2].Name = "Password protected";
        }

        void FindGames()
        {
            searchT = new Thread(new ThreadStart(Search));
            searchT.IsBackground = true;
            searchT.Start();
        }

        void Search()
        {
            this.UseWaitCursor = true;
            var list = ctrl.FindGames();
            PopulateTable(list);
            this.UseWaitCursor = false;
        }

        private void PopulateTable(List<GameItem> list)
        {
            dgvGames.Rows.Clear();

            for(int i=0; i< list.Count; i++)
            {
                dgvGames.Rows.Add();
                dgvGames.Rows[i].Cells[0].Value = list[i].GameName;
                dgvGames.Rows[i].Cells[1].Value = list[i].IpAddress;
                dgvGames.Rows[i].Cells[2].Value = list[i].PasswordProtected;                
            }
        }

        private void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _parent.Show();
            this.Close();
        }

        private void FormJoinGameInternet_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }

        private void FormJoinGameInternet_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormJoinGameInternet_MouseMove(object sender, MouseEventArgs e)
        {
            if (_movable) {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }
        }

        private void btnJoinGame_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).Height -= 5;
            ((Button)sender).Width -= 5;
            ((Button)sender).Top += 5;
        }

        private void btnJoinGame_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).Height += 5;
            ((Button)sender).Width += 5;
            ((Button)sender).Top -= 5;
        }

        private void dgvGames_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvGames.CurrentCell.ReadOnly = true;
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            if (dgvGames.RowCount == 1) return;

            string gameName = dgvGames.SelectedRows[0].Cells[0].Value as string;
            string ip = dgvGames.SelectedRows[0].Cells[1].Value as string;
            string pass = dgvGames.SelectedRows[0].Cells[2].Value as string;

            var item = new GameItem(gameName, ip, pass);
            string pwd = tbxPasswordGame.Text;

            if (item.PasswordProtected == "False") pwd = "";

            bool joined = ctrl.JoinGame(item, pwd);

            if (joined)
            {
                fg.Opacity = 0;
                fg.EnableTimer(true);
                fg.Show();
                fg.DisplayMessage("You connected!");
            }
            else
            {
                FormMessage fm = new FormMessage("Can't connect!");
                fm.Opacity = 0;
                fm.ShowDialog();
                btnRefresh.PerformClick();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            searchT.Abort();
            FindGames();
        }

        private void FormJoinGameInternet_Load(object sender, EventArgs e)
        {
            tbxPasswordGame.BackColor = Color.FromArgb(179, 179, 179);
            Icon = Properties.Resources.logo1;
            Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }
    }
}
