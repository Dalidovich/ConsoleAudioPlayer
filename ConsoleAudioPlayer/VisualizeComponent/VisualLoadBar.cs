using ConsoleAudioPlayer.Buffers;

namespace ConsoleAudioPlayer.VisualizeComponent
{
    public class VisualLoadBar
    {
        public char Filler { get; set; }
        public char EmptyFiller { get; set; }
        public int Capaciti { get; set; }
        public dynamic MaxValue;

        public bool ProcentFlag { get; set; }

        public VisualLoadBar(int capaciti, dynamic maxValue, bool procentFlag = true)
        {
            Capaciti = capaciti;
            MaxValue = maxValue;

            ProcentFlag = procentFlag;

            Filler = VisualBufferTemplate.StandartBarFiller;
            EmptyFiller = VisualBufferTemplate.StandartBarEmptyFiller;
        }

        public string ToString(dynamic value)
        {
            var ProcentValueOfMaxValue = value * 100 / MaxValue;
            var ProcentValueOfCapacity = Capaciti * ProcentValueOfMaxValue / 100;
            int fillerCount = (int)Math.Round(ProcentValueOfCapacity);
            return $"[{new string(Filler, fillerCount)}{new string(EmptyFiller, Capaciti - fillerCount)}]" +
                (ProcentFlag ? $"{(int)ProcentValueOfMaxValue}%" : "");
        }
    }
}
