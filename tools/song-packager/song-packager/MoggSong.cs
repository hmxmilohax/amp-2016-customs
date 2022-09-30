using System.Text.RegularExpressions;

namespace song_packager
{
    public class MoggSong
    {
        private static Regex cleanRegex = new Regex(@"\s+", RegexOptions.Compiled);
        private static Regex nameArtistCleanRegex = new Regex(@"[^a-zA-Z\d\s~`!@#$%^&*()_\-+=[{\]}\\|<,>\./?:]+", RegexOptions.Compiled);
        public string MoggPath { get; set; }
        public string MidiPath { get; set; }
        public string ArenaPath { get; set; }
        public decimal TunnelScale { get; set; }
        public string Title { get; set; }
        public string CleanTitle { get => nameArtistCleanRegex.Replace(this.Title, ""); }
        public string ShortTitle { get; set; }
        public string Artist { get; set; }
        public string CleanArtist { get => nameArtistCleanRegex.Replace(this.Artist, ""); }
        public string ShortArtist { get; set; }
        public string Description { get; set; }
        public string CleanDescription => this.Description == null ? null : cleanRegex.Replace(this.Description, " ");
        public string UnlockRequirement { get; set; }
        public int Bpm { get; set; }
        public int PreviewStartMs { get; set; }
        public int PreviewLengthMs { get; set; }
        public string DemoVideo { get; set; }
        public string Charter { get; set; }
    }
}
