using System.Text.RegularExpressions;

namespace song_packager
{
    public class MoggSong
    {
        private static Regex cleanRegex = new(@"\s+", RegexOptions.Compiled);
        private static Regex nameArtistCleanRegex = new(@"[^a-zA-Z\d\s~`!@#$%^&*()_\-+=[{\]}\\|<,>\./?:]+", RegexOptions.Compiled);
        public string Id { get; set; }
        public string MoggPath { get; set; }
        public string MidiPath { get; set; }
        public string ArenaPath { get; set; }
        public decimal TunnelScale { get; set; }
        public string Title { get; set; }
        public string CleanTitle => nameArtistCleanRegex.Replace(Title, "");
        public string ShortTitle { get; set; }
        public string Artist { get; set; }
        public string CleanArtist => nameArtistCleanRegex.Replace(Artist, "");
        public string ShortArtist { get; set; }
        public string Description { get; set; }
        public string CleanDescription => Description == null ? null : cleanRegex.Replace(Description, " ");
        public string UnlockRequirement { get; set; }
        public decimal Bpm { get; set; }
        public int PreviewStartMs { get; set; }
        public int PreviewLengthMs { get; set; }
        public string DemoVideo { get; set; }
        public string Charter { get; set; }
    }
}
