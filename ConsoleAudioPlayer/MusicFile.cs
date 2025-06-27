using ConsoleAudioPlayer.VisualizeComponent;

namespace ConsoleAudioPlayer
{
    public class MusicFile
    {
        public string Title { get; set; }
        public string PathFile { get; set; }
        public TimeSpan? Duration { get; set; }

        public MusicFile(string path, TimeSpan duration)
        {
            Title = Path.GetFileName(path);
            PathFile = path;
            Duration = duration;
        }

        public MusicFile(string path)
        {
            Title = Path.GetFileName(path);
            PathFile = path;
        }

        public override string ToString()
        {
            return $"{Title}";
        }

        public string ToStringIcon()
        {
            var timeFormatting = Helper.GetFormatter(Duration);
            return $"{Title}     {Duration?.ToString(timeFormatting) ?? ""}";
        }
    }
}
