using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Ad_Gloriam.Model;

namespace Ad_Gloriam.Manager
{
    public static class AudioManager
    {
        private static SoundPlayer looper = new SoundPlayer();
        public static bool Enabled = true;

        public static void Play(Audio audio)
        {
            if (!System.IO.File.Exists(audio.Path) || !Enabled) return;
            SoundPlayer sp = new SoundPlayer(audio.Path);
            sp.Play();
        }

        public static void PlayLoop(Audio audio)
        {
            if (!System.IO.File.Exists(audio.Path) || !Enabled) return;
            looper.Stop();
            looper.SoundLocation = audio.Path;
            looper.PlayLooping();
        }

        public static void StopLoop()
        {
            looper.Stop();
        }

    }
}
