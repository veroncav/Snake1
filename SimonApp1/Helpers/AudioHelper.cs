using System.Threading.Tasks;
using Plugin.Maui.Audio;

namespace SimonApp1.Helpers
{
    public static class AudioHelper
    {
        private static IAudioManager audioManager = null!;

        // ������������� ����� (����������� ��� ������ ����������)
        public static void Init(IAudioManager manager)
        {
            audioManager = manager;
        }

        // ��������������� ����� �� ����� Resources/Sounds
        public static async Task PlaySoundAsync(string fileName)
        {
            if (audioManager == null)
                return;

            var stream = await FileSystem.OpenAppPackageFileAsync($"Sounds/{fileName}");
            var player = audioManager.CreatePlayer(stream);
            player.Play();
        }
    }
}
