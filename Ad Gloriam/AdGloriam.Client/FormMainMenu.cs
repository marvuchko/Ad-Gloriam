using System;
using System.Drawing;
using System.Windows.Forms;
using Ad_Gloriam.Controller;
using Ad_Gloriam.Model;
using Ad_Gloriam.View;
using System.IO;
using System.Configuration;

namespace Ad_Gloriam
{
    public partial class FormMainMenu : Form
    {
        private int _xOffset, _yOffset;
        private bool _movable;
        private Timer _tSinglePlayer, _tMultiplayer, _tOptions;
        private FormGame fg;
        private FormMultiPlayerMenu formMultiPlayerMenu;
        private Timer _t;

        public FormMainMenu()
        {
            Ad_Gloriam.Manager.AudioManager.PlayLoop(Audio.MainTheme);
            LoadServerIP();
            Opacity = 0;

            if(!File.Exists("./config.txt"))
            {
                ResourceCollection.SetDefaultCollection();
                StreamWriter sw = new StreamWriter("./config.txt");
                sw.WriteLine("Default");
                sw.Close();
            }
            else
            {
                StreamReader sr = new StreamReader("./config.txt");
                string config = sr.ReadLine();
                sr.Close();
                if(config.Equals("Default"))
                {
                    ResourceCollection.SetDefaultCollection();
                    StreamWriter sw = new StreamWriter("./config.txt");
                    sw.WriteLine("Default");
                    sw.Close();
                }
                else
                {
                    ResourceManagerImpl.LoadImages("./Themes/" + config);
                }
            }

            InitializeComponent();
            this.TransparencyKey = Color.FromArgb(31, 32, 33);
            this.BackColor = Color.FromArgb(31, 32, 33);
            this.Icon = Properties.Resources.logo1;

            BackgroundImage = ResourceCollection.GetResourceByName("main_menu.png");
            btnExit.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_exit.png");
            btnMultiPlayer.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_multyplayer.png");
            btnSinglePlayer.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_singleplayer.png");
            btnOptions.BackgroundImage = ResourceCollection.GetResourceByName("menu_button_options.png");

            _movable = false;

            _t = new Timer()
            {
                Enabled = true,
                Interval = 10
            };

            _tSinglePlayer = new Timer()
            {
                Enabled = false,
                Interval = 5
            };

            _tMultiplayer = new Timer()
            {
                Enabled = false,
                Interval = 5
            };

            _tOptions = new Timer()
            {
                Enabled = false,
                Interval = 5
            };

            _tSinglePlayer.Tick += _tSinglePlayer_Tick;
            _tMultiplayer.Tick += _tMultiplayer_Tick;
            _tOptions.Tick += _tOptions_Tick;
            _t.Tick += _t_Tick;
        }

        private void LoadServerIP()
        {
            if(!File.Exists("./server.txt"))
            {
                FormMessage fm = new FormMessage("Missing file: server.txt");
                fm.ShowDialog();
                return;
            }

            StreamReader sr = new StreamReader("./server.txt");
            string ip = sr.ReadLine();
            ConfigurationManager.AppSettings["ServerIPAddress"] = ip;
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

        private void _tOptions_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _tMultiplayer_Tick(object sender, EventArgs e)
        {
            if (formMultiPlayerMenu.Opacity == 1)
            {
                _tMultiplayer.Enabled = false;
                return;
            }

            formMultiPlayerMenu.Opacity = 1;
            this.Hide();
        }

        private void _tSinglePlayer_Tick(object sender, EventArgs e)
        {
            if(fg.Opacity == 1)
            {
                _tSinglePlayer.Enabled = false;
                return;
            }

            fg.Opacity = 1;
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FormDialog fd = new FormDialog("Do you want to exit?");
            fd.Opacity = 0;
            DialogResult dr = fd.ShowDialog();
            if(dr == DialogResult.OK)
                Environment.Exit(0);
        }

        private void btnSinglePlayer_Click(object sender, EventArgs e)
        {
            IModel model = new ModelGame();
            fg = new FormGame(this);
            IController ctrl = new ControllerSinglePlayer(model, fg);
            fg.GameController = ctrl;
            fg.Opacity = 0;
            fg.Show();

            _tSinglePlayer.Enabled = true;
        }

        private void btnMultiPlayer_Click(object sender, EventArgs e)
        {
            formMultiPlayerMenu = new FormMultiPlayerMenu(this);
            formMultiPlayerMenu.Opacity = 0;
            formMultiPlayerMenu.Show();

            _tMultiplayer.Enabled = true;
        }

        private void btnSinglePlayer_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width -= 5;
            ((Button)sender).Height -= 5;
            ((Button)sender).Top += 5;
        }

        private void btnSinglePlayer_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).Width += 5;
            ((Button)sender).Height += 5;
            ((Button)sender).Top -= 5;
        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void FormMainMenu_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            FormOptionsMenu fo = new FormOptionsMenu(this);
            fo.Opacity = 0;
            fo.ShowDialog();
        }

        private void FormMainMenu_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormMainMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }
        }

        private void btnSinglePlayer_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }
    }
}
