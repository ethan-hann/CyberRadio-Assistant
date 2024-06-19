using System.ComponentModel;
using Newtonsoft.Json;

namespace RadioExt_Helper.models
{
    public class MetaData : INotifyPropertyChanged
    {
        private string _displayName = "69.9 Your Station Name";

        [JsonProperty("displayName")]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        [JsonProperty("fm")]
        public float Fm { get; set; } = 69.9f;

        [JsonProperty("volume")]
        public float Volume { get; set; } = 1.0f;

        [JsonProperty("icon")]
        public string Icon { get; set; } = "UIIcon.alcohol_absynth";

        [JsonProperty("customIcon")]
        public CustomIcon CustomIcon { get; set; } = new();

        [JsonProperty("streamInfo")]
        public StreamInfo StreamInfo { get; set; } = new();

        [JsonProperty("order")]
        public List<string> SongOrder { get; set; } = [];

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString() => DisplayName;
    }
}