using ConsoleAudioPlayer.Buffers;
using ConsoleAudioPlayer.VisualizeComponent;
using System.Text.Json;

namespace ConsoleAudioPlayer.PlayerSettings
{
    public class PlayerSettigsController
    {
        private static string _settingsFileName = "Settings.json";

        public static string[] allowedExtensions = [".mp3", ".wav"];
        public AudioPlayer Player { get; set; }

        public async Task CreateOutSettings()
        {
            var settings = new Settings();
            settings.PutDataStandartData();

            var finalPath = Path.Combine(AppContext.BaseDirectory, _settingsFileName);
            if (!File.Exists(finalPath))
            {
                using (StreamWriter writer = new StreamWriter(finalPath, false))
                {
                    await writer.WriteAsync(JsonSerializer.Serialize(settings));
                }
            }
        }

        public async Task Init()
        {
            try
            {
                var finalPath = Path.Combine(AppContext.BaseDirectory, _settingsFileName);
                var settings = new Settings();
                using (StreamReader reader = new StreamReader(finalPath))
                {
                    string text = await reader.ReadToEndAsync();
                    settings = JsonSerializer.Deserialize<Settings>(text);
                }
                if (settings != null)
                {
                    ValueBufferTemplate.StandartVolumeValue = settings.VolumeValue ?? ValueBufferTemplate.StandartVolumeValue;
                    ValueBufferTemplate.StandartSkipValue = settings.SkipValue ?? ValueBufferTemplate.StandartSkipValue;

                    ValueBufferTemplate.BoundOfSelectedFile = settings.BoundOfSelectedFile ?? ValueBufferTemplate.BoundOfSelectedFile;
                    ValueBufferTemplate.ConsoleRefreshRate = settings.ConsoleRefreshRate ?? ValueBufferTemplate.ConsoleRefreshRate;
                    ValueBufferTemplate.ApplyContextMenu = settings.ApplyContextMenu ?? ValueBufferTemplate.ApplyContextMenu;

                    ValueBufferTemplate.SkipForward = settings.SkipForwardKey ?? ValueBufferTemplate.SkipForward;
                    ValueBufferTemplate.SkipBack = settings.SkipBackKey ?? ValueBufferTemplate.SkipBack;
                    ValueBufferTemplate.VolumeUp = settings.VolumeUpKey ?? ValueBufferTemplate.VolumeUp;
                    ValueBufferTemplate.VolumeDown = settings.VolumeDownKey ?? ValueBufferTemplate.VolumeDown;
                    ValueBufferTemplate.PlayState = settings.PlayStateKey ?? ValueBufferTemplate.PlayState;
                    ValueBufferTemplate.Stop = settings.StopKey ?? ValueBufferTemplate.Stop;
                    ValueBufferTemplate.SettingsOptions = settings.SettingsOptionsKey ?? ValueBufferTemplate.SettingsOptions;
                    ValueBufferTemplate.Next = settings.NextKey ?? ValueBufferTemplate.Next;
                    ValueBufferTemplate.Previous = settings.PreviousKey ?? ValueBufferTemplate.Previous;
                    ValueBufferTemplate.PlaylistState = settings.PlaylistStateKey ?? ValueBufferTemplate.PlaylistState;

                    ContextMenuRegisrated.Init();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
