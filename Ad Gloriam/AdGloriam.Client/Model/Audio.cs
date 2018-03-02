using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad_Gloriam.Model
{
    public class Audio
    {
        private string _path;
        public string Path { get => _path; set => _path = value; }

        public static Audio MainTheme = new Audio() { Path = "./Audio/theme.wav" };
        public static Audio GameLoop = new Audio() { Path = "./Audio/game.wav" };
    }
}
