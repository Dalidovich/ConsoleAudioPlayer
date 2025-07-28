using ConsoleAudioPlayer.Buffers;
using ConsoleAudioPlayer.PlayerSettings;
using ConsoleAudioPlayer.VisualizeComponent;
using NAudio.Wave;
using ShellLink;
using File = System.IO.File;

namespace ConsoleAudioPlayer
{
    public class AudioPlayer
    {
        public string FilesPath { get; set; }
        public bool ChangeSelectMusicFileFlag { get; set; } = false;
        public int MusicFileSelector { get; set; }
        public int PlayListState { get; set; }

        public WaveStream Reader { get; set; }
        public WaveOutEvent WaveOut { get; set; }

        public PlayerSettigsController PlayerSettigsController { get; set; }
        public Visualizer Visualizer { get; set; }
        private CancellationTokenSource cancellationTokenSource { get; set; }
        public List<MusicFile> MusicFiles { get; set; } = new List<MusicFile>();

        public AudioPlayer(string filePath)
        {
            IOFileWork(filePath);

            Reader = SelectCorrectFileReader();
            WaveOut = new WaveOutEvent();
            cancellationTokenSource = new CancellationTokenSource();
            PlayerSettigsController = new PlayerSettigsController();
            Visualizer = new Visualizer(this);
        }

        public void IOFileWork(string path)
        {

            if (path.EndsWith(".lnk"))
            {
                path = Shortcut.ReadFromFile(path).LinkTargetIDList.Path;
            }

            FilesPath = path;


            if (!File.Exists(FilesPath))
            {
                var musicFiles = Directory.GetFiles(FilesPath, "**", SearchOption.TopDirectoryOnly)
                    .Where(file => PlayerSettigsController.allowedExtensions.Contains(Path.GetExtension(file).ToLower())).Select(x => new MusicFile(x));

                MusicFiles.AddRange(musicFiles);
            }
            else
            {
                MusicFiles.Add(new MusicFile(FilesPath));
            }
        }

        private async Task ControlSettingsLoop()
        {
            ConsoleKeyInfo keyinfo;
            while (true)
            {
                keyinfo = Console.ReadKey();
                switch (keyinfo.Key)
                {
                    case ConsoleKey key when key == ValueBufferTemplate.SkipForward:
                        SkipMusicTime();
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.SkipBack:
                        SkipMusicTime(true);
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.VolumeUp:
                        ChangeVolume(true);
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.VolumeDown:
                        ChangeVolume();
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.PlayState:
                        ChangePlayState();
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.Stop:
                        WaveOut.Stop();
                        Reader.Seek(0, SeekOrigin.Begin);
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.SettingsOptions:
                        await PlayerSettigsController.Init();
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.Next:
                        ChangeSelector(1);
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.Previous:
                        ChangeSelector(1, true);
                        break;
                    case ConsoleKey key when key == ValueBufferTemplate.PlaylistState:
                        ChangePlayListState();
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }
        }

        public void SkipMusicTime(bool leftDirection = false)
        {
            if (!leftDirection)
            {
                if (Reader.CurrentTime.TotalSeconds + ValueBufferTemplate.StandartSkipValue <= Reader.TotalTime.TotalSeconds)
                {
                    Reader.Skip(ValueBufferTemplate.StandartSkipValue);
                }
                return;
            }
            Reader.Skip(-ValueBufferTemplate.StandartSkipValue);
        }

        public void ChangeVolume(bool upDirection = false)
        {
            if (upDirection)
            {
                if (WaveOut.Volume + ValueBufferTemplate.StandartVolumeValue > 1)
                {
                    WaveOut.Volume = 1f;
                }
                else
                {
                    WaveOut.Volume += ValueBufferTemplate.StandartVolumeValue;
                }
                return;
            }
            if (WaveOut.Volume - ValueBufferTemplate.StandartVolumeValue < 0)
            {
                WaveOut.Volume = 0f;
            }
            else
            {
                WaveOut.Volume -= ValueBufferTemplate.StandartVolumeValue;
            }
        }

        public void ChangePlayState()
        {
            if (WaveOut.PlaybackState == PlaybackState.Playing)
            {
                WaveOut.Pause();
            }
            else if (WaveOut.PlaybackState == PlaybackState.Paused)
            {
                WaveOut.Play();
            }
            else
            {
                RepitMusicFile();
                WaveOut.Play();
            }
        }

        public void ChangeSelector(int value, bool upDirection = false)
        {
            switch (PlayListState)
            {
                case 0:
                    if (upDirection)
                    {
                        if (MusicFileSelector - value >= 0)
                        {
                            MusicFileSelector -= value;
                        }
                        else
                        {
                            MusicFileSelector = MusicFiles.Count - 1;
                        }

                        ChangeSelectMusicFile();
                        return;
                    }
                    if (MusicFileSelector + value < MusicFiles.Count)
                    {
                        MusicFileSelector += value;
                    }
                    else
                    {
                        MusicFileSelector = 0;
                    }
                    ChangeSelectMusicFile();
                    break;
                case 1:
                    var rnd = new Random();
                    var rndValue = rnd.Next(0, MusicFiles.Count);
                    while (MusicFileSelector == rndValue)
                    {
                        rndValue = rnd.Next(0, MusicFiles.Count);
                    }
                    MusicFileSelector = rndValue;
                    ChangeSelectMusicFile();
                    break;
                case 2:
                    ChangeSelectMusicFile();
                    break;
                default:
                    break;
            }

        }

        public void ChangePlayListState()
        {
            if (PlayListState + 1 == VisualBufferTemplate.StandartPlayListStates.Length)
            {
                PlayListState = 0;
            }
            else
            {
                PlayListState += 1;
            }
        }

        public void RepitMusicFile()
        {
            WaveOut.Stop();
            Reader.Seek(0, SeekOrigin.Begin);
        }

        public WaveStream SelectCorrectFileReader()
        {
            if (MusicFiles[MusicFileSelector].PathFile.EndsWith(".wav"))
            {
                return new WaveFileReader(MusicFiles[MusicFileSelector].PathFile);
            }
            return new Mp3FileReader(MusicFiles[MusicFileSelector].PathFile);
        }

        public void ChangeSelectMusicFile()
        {
            ChangeSelectMusicFileFlag = true;
            Reader = SelectCorrectFileReader();
            WaveOut.Stop();
            WaveOut.Init(Reader);

            MusicFiles[MusicFileSelector].Duration = Reader.TotalTime;
            Visualizer.InitBars();

            WaveOut.Play();
            ChangeSelectMusicFileFlag = false;
        }

        public async Task Init()
        {
            await PlayerSettigsController.CreateOutSettings();
            await PlayerSettigsController.Init();

            PlayListState = ValueBufferTemplate.StartPlayListStates;
            MusicFileSelector = 0;

            ChangeSelectMusicFile();
            var controlSettingsTask = Task.Factory.StartNew(ControlSettingsLoop, cancellationTokenSource.Token);

            while (true)
            {
                if (!ChangeSelectMusicFileFlag)
                {
                    await Visualizer.VisualizeTotal();
                }
                if (WaveOut.PlaybackState == PlaybackState.Stopped && Reader.CurrentTime.TotalSeconds == Reader.TotalTime.TotalSeconds)
                {
                    ChangeSelector(1);
                }
            }
        }
    }
}
