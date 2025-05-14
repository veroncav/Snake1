using System.Media;

namespace Snakegame
{
    // ===== Класс: Sounds =====
    // Управляет воспроизведением фоновой музыки и звуков игры
    public static class Sounds
    {
        private static SoundPlayer musicPlayer = new SoundPlayer("Sounds/music.wav");
        private static SoundPlayer eatPlayer = new SoundPlayer("Sounds/eat.wav");
        private static SoundPlayer gameOverPlayer = new SoundPlayer("Sounds/gameover.wav");
        private static SoundPlayer clickPlayer = new SoundPlayer("Sounds/click.wav");

        public static void PlayMusic() => musicPlayer.PlayLooping();
        public static void StopMusic() => musicPlayer.Stop();
        public static void PlayEat() => eatPlayer.Play();
        public static void PlayGameOver() => gameOverPlayer.Play();
        public static void PlayClick() => clickPlayer.Play();
    }
}

