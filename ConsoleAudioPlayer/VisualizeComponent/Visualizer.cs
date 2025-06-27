using ConsoleAudioPlayer.Buffers;

namespace ConsoleAudioPlayer.VisualizeComponent
{
    public class Visualizer
    {
        public AudioPlayer Player { get; set; }
        public VisualLoadBar VolumeBar { get; set; }
        public VisualLoadBar DurationBar { get; set; }

        public Visualizer(AudioPlayer audioPlayer)
        {
            Player = audioPlayer;
            VolumeBar = new VisualLoadBar(10, 1);
            DurationBar = new VisualLoadBar(10, Player.Reader.TotalTime.TotalSeconds, false);
        }

        public void InitBars()
        {
            DurationBar = new VisualLoadBar(10, Player.Reader.TotalTime.TotalSeconds, false);
        }

        public async Task VisualizeTotal()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            Write(VisualizeUnselectedMusicFiles());
            Write(new string('_', Player.MusicFiles[Player.MusicFileSelector].Title.Length));
            Write(VisualizeName(Player.MusicFiles[Player.MusicFileSelector].ToString()));
            Write(VisualizeDuration());
            Write(VisualizeVolume());
            Write(VisualizePlayListState());
            Write(new string('_', Player.MusicFiles[Player.MusicFileSelector].Title.Length));
            Write(VisualizeUnselectedMusicFiles(true));
            if (Player.MusicFileSelector <= ValueBufferTemplate.BoundOfSelectedFile)
            {
                Console.SetCursorPosition(0, 0);
            }
            else
            {
                Console.SetCursorPosition(0, Player.MusicFileSelector - ValueBufferTemplate.BoundOfSelectedFile);
            }
            Console.Title = Player.MusicFiles[Player.MusicFileSelector].ToString();
            await Task.Delay(ValueBufferTemplate.ConsoleRefreshRate);
        }

        public string VisualizePlayListState()
        {
            return VisualBufferTemplate.StandartPlayListStates[Player.PlayListState];
        }

        public string VisualizeName(string name)
        {
            return name;
        }

        public string VisualizeDuration()
        {
            var timeFormatting = Helper.GetFormatter(Player.Reader.TotalTime);
            var value = $"{DurationBar.ToString(Player.Reader.CurrentTime.TotalSeconds)} " +
                $"{Player.Reader.CurrentTime.ToString(timeFormatting)}/{Player.Reader.TotalTime.ToString(timeFormatting)}";

            return value;
        }

        public string VisualizeVolume()
        {
            return VolumeBar.ToString(Player.WaveOut.Volume);
        }

        public string VisualizeUnselectedMusicFiles(bool afterSelectFile = false)
        {
            if (afterSelectFile)
            {
                return String.Join("\n", Player.MusicFiles.Skip(Player.MusicFileSelector + 1).Select(x => x.ToStringIcon()));
            }
            else
            {
                return String.Join("\n", Player.MusicFiles.Take(Player.MusicFileSelector).Select(x => x.ToStringIcon()));
            }
        }

        public void Write(string value)
        {
            Console.WriteLine($"{value}{new string(' ', 50)}");
        }
    }
}
