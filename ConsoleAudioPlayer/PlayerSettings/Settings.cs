namespace ConsoleAudioPlayer.PlayerSettings
{
    public class Settings
    {
        public int? SkipValue { get; set; }
        public float? VolumeValue { get; set; }

        public int? BoundOfSelectedFile { get; set; }
        public int? ConsoleRefreshRate { get; set; }

        public bool? ApplyContextMenu { get; set; }

        public ConsoleKey? SkipForwardKey { get; set; }
        public ConsoleKey? SkipBackKey { get; set; }
        public ConsoleKey? VolumeUpKey { get; set; }
        public ConsoleKey? VolumeDownKey { get; set; }
        public ConsoleKey? PlayStateKey { get; set; }
        public ConsoleKey? StopKey { get; set; }
        public ConsoleKey? SettingsOptionsKey { get; set; }
        public ConsoleKey? NextKey { get; set; }
        public ConsoleKey? PreviousKey { get; set; }
        public ConsoleKey? PlaylistStateKey { get; set; }

    }
}
