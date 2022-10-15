using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace song_packager
{
    public class JsonSong
    {
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Id { get; set; }

        [JsonPropertyName("artist")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Artist { get; set; }

        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Name { get; set; }

        [JsonPropertyName("bpm")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Bpm { get; set; }

        [JsonPropertyName("charter")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Charter { get; set; }

        [JsonPropertyName("download")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Download { get; set; }

        [JsonPropertyName("video")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Video { get; set; }

        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }
        public JsonSong(MoggSong song)
        {
            this.Id = song.Id;
            this.Artist = song.CleanArtist;
            this.Name = song.CleanTitle;
            this.Bpm = song.Bpm;
            this.Charter = song.Charter;
            this.Video = this.NormalizeVideoURL(song.DemoVideo);
            this.Description = song.CleanDescription;
            this.Download = $"{Program.DownloadBase}/{song.Id}.zip";
        }

        private string? NormalizeVideoURL(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            url = url.Trim();
            url = Regex.Replace(url, @"^https?://(?:www\.)?youtube\.com/watch?.*?v=([^&]+).*$", "https://youtu.be/$1");

            return url;
        }
    }
}
