namespace ConsoleAudioPlayer.Buffers
{
    public static class VisualBufferTemplate
    {
        public static char StandartBarFiller { get; set; } = '|';
        public static char StandartBarEmptyFiller { get; set; } = '~';
        public static string[] StandartPlayListStates { get; set; } = { "*series*", "*shuffle*", "*repit*" };
    }
}
