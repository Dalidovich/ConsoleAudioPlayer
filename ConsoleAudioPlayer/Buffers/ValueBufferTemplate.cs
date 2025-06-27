namespace ConsoleAudioPlayer.Buffers
{
    public static class ValueBufferTemplate
    {
        public static int StandartSkipValue { get; set; } = 25;
        public static float StandartVolumeValue { get; set; } = 0.1f;

        public static int BoundOfSelectedFile { get; set; } = 15;
        public static int ConsoleRefreshRate { get; set; } = 250;
        public static bool ApplyContextMenu { get; set; } = false;

        public static ConsoleKey SkipForward { get; set; } = ConsoleKey.RightArrow;
        public static ConsoleKey SkipBack { get; set; } = ConsoleKey.LeftArrow;
        public static ConsoleKey VolumeUp { get; set; } = ConsoleKey.UpArrow;
        public static ConsoleKey VolumeDown { get; set; } = ConsoleKey.DownArrow;
        public static ConsoleKey PlayState { get; set; } = ConsoleKey.Spacebar;
        public static ConsoleKey Stop { get; set; } = ConsoleKey.X;
        public static ConsoleKey SettingsOptions { get; set; } = ConsoleKey.O;
        public static ConsoleKey Next { get; set; } = ConsoleKey.NumPad2;
        public static ConsoleKey Previous { get; set; } = ConsoleKey.NumPad8;
        public static ConsoleKey PlaylistState { get; set; } = ConsoleKey.S;
    }
}
