using ConsoleAudioPlayer.Buffers;
using ConsoleAudioPlayer.PlayerSettings;

namespace ConsoleAudioPlayer.VisualizeComponent
{
    public static class Helper
    {
        public static string GetFormatter(TimeSpan? timeSpan)
        {
            var timeFormatting = @"mm\:ss";
            if (timeSpan?.Hours != 0)
            {
                timeFormatting = @"hh\:mm\:ss";
            }
            return timeFormatting;
        }

        public static void PutDataStandartData(this Settings settings)
        {
            settings.VolumeValue = ValueBufferTemplate.StandartVolumeValue;
            settings.SkipValue = ValueBufferTemplate.StandartSkipValue;

            settings.BoundOfSelectedFile = ValueBufferTemplate.BoundOfSelectedFile;
            settings.ConsoleRefreshRate = ValueBufferTemplate.ConsoleRefreshRate;
            settings.ApplyContextMenu = ValueBufferTemplate.ApplyContextMenu;

            settings.SkipForwardKey = ValueBufferTemplate.SkipForward;
            settings.SkipBackKey = ValueBufferTemplate.SkipBack;
            settings.VolumeUpKey = ValueBufferTemplate.VolumeUp;
            settings.VolumeDownKey = ValueBufferTemplate.VolumeDown;
            settings.PlayStateKey = ValueBufferTemplate.PlayState;
            settings.StopKey = ValueBufferTemplate.Stop;
            settings.SettingsOptionsKey = ValueBufferTemplate.SettingsOptions;
            settings.NextKey = ValueBufferTemplate.Next;
            settings.PreviousKey = ValueBufferTemplate.Previous;
            settings.PlaylistStateKey = ValueBufferTemplate.PlaylistState;
        }
    }
}
