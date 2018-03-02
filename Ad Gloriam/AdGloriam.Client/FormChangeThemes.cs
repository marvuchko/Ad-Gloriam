using System;
using Ad_Gloriam.View;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Ad_Gloriam
{
    public partial class FormChangeThemes : Form
    {
        private int _xOffset, _yOffset;
        private bool _movable;
        private Timer _t;
        private Form _parent;

        public FormChangeThemes(Form _parent)
        {
            InitializeComponent();
            this._parent = _parent;
            Icon = Properties.Resources.logo1;

            TransparencyKey = Color.FromArgb(0, 1, 2);
            BackColor = Color.FromArgb(0, 1, 2);

            _movable = false;

            pnlHolder.BackColor = Color.FromArgb(145, 138, 111);
            Cursor = new Cursor(Properties.Resources.cursor.Handle);

            _t = new Timer()
            {
                Enabled = true,
                Interval = 5
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

        private void btnDefault_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).Height += 5;
            ((Button)sender).Width += 5;
            ((Button)sender).Top -= 5;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _parent.Show();
            this.Close();
        }

        private void FormChangeTokens_Load(object sender, EventArgs e)
        {
            ResourceManagerImpl.LoadDirectories("./Themes");

            twThemes.ImageList = new ImageList();

            for (int i = 0; i < ResourceManagerImpl.Directories.Length; i++)
            {
                ResourceManagerImpl.LoadTumbnail(ResourceManagerImpl.DirectoryNamesFull[i]);
                twThemes.Nodes.Add(ResourceManagerImpl.DirectoryNames[i]);
                if (ResourceManagerImpl.Tumbnail != null)
                {
                    twThemes.ImageList.Images.Add(ResourceManagerImpl.Tumbnail);
                    twThemes.Nodes[i].ImageIndex = i;
                    ResourceManagerImpl.Tumbnail = null;
                }
            }
        }

        private void FormChangeTokens_MouseDown(object sender, MouseEventArgs e)
        {
            _movable = true;
            _xOffset = Cursor.Position.X - Location.X;
            _yOffset = Cursor.Position.Y - Location.Y;
        }

        private void FormChangeTokens_MouseUp(object sender, MouseEventArgs e)
        {
            _movable = false;
        }

        private void FormChangeTokens_MouseMove(object sender, MouseEventArgs e)
        {
            if(_movable)
            {
                Location = new Point(Cursor.Position.X - _xOffset, Cursor.Position.Y - _yOffset);
                Update();
            }

            Cursor = new Cursor(Properties.Resources.cursor.Handle);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (twThemes.SelectedNode == null)
            {
                FormMessage fms = new FormMessage("Please select a theme!");
                fms.Opacity = 0;
                fms.ShowDialog();
                return;
            }

            ResourceManagerImpl.LoadDirectories("./Themes");
            ResourceManagerImpl.LoadImages(ResourceManagerImpl.DirectoryNamesFull[twThemes.SelectedNode.Index]);

            StreamWriter sw = new StreamWriter("./config.txt");
            sw.WriteLine(ResourceManagerImpl.DirectoryNames[twThemes.SelectedNode.Index]);
            sw.Close();

            FormMessage fm = new FormMessage("Successfully changed! Resterting game!");
            fm.Opacity = 0;
            if(DialogResult.OK == fm.ShowDialog())
            {
                Application.Restart();
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            ResourceCollection.SetDefaultCollection();
            StreamWriter sw = new StreamWriter("./config.txt");
            sw.WriteLine("Default");
            sw.Close();

            FormMessage fm = new FormMessage("Default theme loaded! Resterting game!");
            fm.Opacity = 0;
            if (DialogResult.OK == fm.ShowDialog())
            {
                Application.Restart();
            }
        }

        private void btnDefault_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).Height -= 5;
            ((Button)sender).Width -= 5;
            ((Button)sender).Top += 5;
        }

        
    }
}
