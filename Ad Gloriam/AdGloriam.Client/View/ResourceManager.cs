using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;

namespace Ad_Gloriam.View
{
    public static class ResourceManagerImpl
    {
        private static ImageList _imageList;
        private static Image _tumbnail;
        private static string[] _imageNames;
        private static DirectoryInfo[] _directories;
        private static string[] _directoryNames;
        private static string[] _directoryNamesFull;

        private static ResourceManager _resouces;
        private static ResourceSet _resourceSet;

        public static ImageList ImageList { get => _imageList; set => _imageList = value; }
        public static string[] ImageNames { get => _imageNames; set => _imageNames = value; }
        public static string[] DirectoryNames { get => _directoryNames; set => _directoryNames = value; }
        public static DirectoryInfo[] Directories { get => _directories; set => _directories = value; }
        public static string[] DirectoryNamesFull { get => _directoryNamesFull; set => _directoryNamesFull = value; }
        public static Image Tumbnail { get => _tumbnail; set => _tumbnail = value; }

        public static ResourceManager Resouces { get => _resouces; set => _resouces = value; }
        public static ResourceSet ResourceSet { get => _resourceSet; set => _resourceSet = value; }

        public static void LoadImages(string folder)
        {
            _resouces = new ResourceManager("resources", typeof(global::Ad_Gloriam.Properties.Resources).Assembly);

            ResourceCollection.Clear();

            _imageList = new ImageList();
            DirectoryInfo di = new DirectoryInfo(folder);
            FileInfo[] files = di.GetFiles("*.png");

            _imageNames = new string[files.Length];

            for(int i = 0; i < files.Length; i++)
            {
                _imageList.Images.Add(Image.FromFile(files[i].FullName));
                _imageNames[i] = files[i].Name;
                ResourceCollection.addResources(files[i].Name, Image.FromFile(files[i].FullName), i);
            }

        }

        public static void LoadTumbnail(string folder)
        {
            _imageList = new ImageList();
            DirectoryInfo di = new DirectoryInfo(folder);
            FileInfo[] files = di.GetFiles("theme.png");

            if (files.Length == 0) return;

            Tumbnail = Image.FromFile(files[0].FullName);
        }

        public static void LoadDirectories(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            Directories = di.GetDirectories();

            _directoryNames = new string[Directories.Length];
            _directoryNamesFull = new string[Directories.Length];

            for (int i = 0; i < Directories.Length; i++)
            {
                DirectoryNames[i] = Directories[i].Name;
                DirectoryNamesFull[i] = Directories[i].FullName;
            }
        }
    }
}
