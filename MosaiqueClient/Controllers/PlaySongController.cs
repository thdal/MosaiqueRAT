using Client.Packets.ServerPackets;
using NAudio.Wave;
using System.IO;

namespace Client.Controllers
{
    static class PlaySongController
    {

        public static void playOnInt(PlaySong packet)
        {
            if (packet.song == 1)
                playFile(Properties.Resources.bikeHorn);
            else if (packet.song == 2)
                playFile(Properties.Resources.cash);
            else if (packet.song == 3)
                playFile(Properties.Resources.beep);
            else if (packet.song == 4)
                playFile(Properties.Resources.piano);
            else if (packet.song == 5)
                playFile(Properties.Resources.singes);
            else if (packet.song == 6)
                playFile(Properties.Resources.sifflet);
            else if (packet.song == 7)
                playFile(Properties.Resources.death);
            else if (packet.song == 8)
                playFile(Properties.Resources.deagle);
            else if (packet.song == 9)
                playFile(Properties.Resources.lasersword);
        }

        private static void playFile(byte[] resourceMp3)
        {
            MemoryStream mp3file = new MemoryStream(resourceMp3);
            Mp3FileReader mp3reader = new Mp3FileReader(mp3file);
            var waveOut = new WaveOut(); // or WaveOutEvent()
            waveOut.Init(mp3reader);
            waveOut.Play();
            while (waveOut.PlaybackState == PlaybackState.Playing)
            {
                System.Threading.Thread.Sleep(100);
            }
            waveOut.Stop();
            waveOut.Dispose();
            waveOut = null;
            mp3reader.Close();
        }
    }
}
