using System;
using System.IO;
using System.Threading.Tasks;

namespace Tetris
{
    public static class SoundEffects
    {

        public static void PlaySound(System.IO.Stream str)
        {
            if (Properties.Settings.Default.PlaySounds)
            {
                Task.Run(() => {
                    new System.Media.SoundPlayer(str).Play();
                });
            }
        }

        private static WMPLib.WindowsMediaPlayer player;
        public static void PlayMusic(MusicState musicState = MusicState.Play)
        {
            if (Properties.Settings.Default.PlayMusic)
            {
                if(player == null)
                {
                    player = new WMPLib.WindowsMediaPlayer();

                    MemoryStream myMemStream = new MemoryStream();
                    Properties.Resources.korebeiniki.CopyTo(myMemStream);

                    byte[] b = myMemStream.ToArray();

                    var korebeinikiFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\korebeiniki.wav";
                    FileInfo fileInfo = new FileInfo(korebeinikiFilePath);

                    if (!File.Exists(korebeinikiFilePath))
                    {
                        FileStream fs = fileInfo.OpenWrite();
                        fs.Write(b, 0, b.Length);
                        fs.Close();

                        System.Threading.Thread.Sleep(100);
                    }
                    
                    player.URL = fileInfo.Name;
                }

                switch(musicState)
                {
                    case MusicState.Stop:
                        player.controls.stop();
                        break;
                    case MusicState.Play:
                        player.settings.setMode("Loop", true);
                        player.controls.play();
                        break;
                    case MusicState.Pause:
                        player.controls.pause();
                        break;
                }
            }
        }

        public enum MusicState
        {
            Stop,
            Play,
            Pause
        }
    }
}
