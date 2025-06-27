using ConsoleAudioPlayer.PlayerSettings;

namespace ConsoleAudioPlayer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 0)
            {
                var player = new AudioPlayer(args[0]);
                await player.Init();
            }
            else
            {
                var settingsController = new PlayerSettigsController();
                await settingsController.CreateOutSettings();
                await settingsController.Init();
            }
        }
    }
}
