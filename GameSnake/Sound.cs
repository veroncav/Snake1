using NAudio.Wave;
using System;
using System.IO;

namespace GameSnake
{
    internal static class Sound
    {
        private static IWavePlayer backgroundPlayer;
        private static AudioFileReader backgroundReader;

        public static void PlayBackground(string path)
        {
            try
            {
                StopBackground();

                if (File.Exists(path))
                {
                    backgroundReader = new AudioFileReader(path) { Volume = 0.2f }; // тише
                    backgroundPlayer = new WaveOutEvent();
                    backgroundPlayer.Init(backgroundReader);
                    backgroundPlayer.Play();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка фоновой музыки: " + ex.Message);
            }
        }

        public static void StopBackground()
        {
            if (backgroundPlayer != null)
            {
                backgroundPlayer.Stop();
                backgroundPlayer.Dispose();
                backgroundPlayer = null;
            }

            if (backgroundReader != null)
            {
                backgroundReader.Dispose();
                backgroundReader = null;
            }
        }

        public static void PlaySound(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (var audioFile = new AudioFileReader(path))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка звука: " + ex.Message);
            }
        }
    }
}