using System.Web;

namespace song_packager
{
    internal static class IndexGenerator
    {
        private static string Indent(int levels = 0, string indent = "    ", int baseLevels = 3)
        {
            return string.Join(indent, Enumerable.Repeat("", levels + baseLevels + 1).ToArray());
        }
        public static string GenerateIndex(params JsonSong[] songs)
        {
            var songListHtml = new List<string>();

            foreach (var song in songs.OrderBy(song => song.Name).OrderBy(song => song.Artist))
            {
                songListHtml.AddRange(new string[] {
                    $"\n{Indent(0)}<div class=\"song\">\n",
                    $"{Indent(1)}<div class=\"title\">\n{Indent(2)}<a class=\"download\" href=\"{HttpUtility.HtmlEncode(song.Download)}\">\n{Indent(3)}{HttpUtility.HtmlEncode($"{song.Artist} - {song.Name}")}\n{Indent(2)}</a>",
                    !string.IsNullOrWhiteSpace(song.Video) ? $"\n{Indent(2)}-\n{Indent(2)}<a class=\"video\" href=\"{HttpUtility.HtmlEncode(song.Video.Trim())}\" target=\"_blank\">\n{Indent(3)}Demo Video\n{Indent(2)}</a>\n" : "\n",
                    $"{Indent(1)}</div>\n",

                    $"{Indent(1)}<div class=\"id\">\n{Indent(2)}<strong>ID: </strong>\n{Indent(2)}{HttpUtility.HtmlEncode($"{song.Id}")}\n{Indent(1)}</div>\n",
                    song.Bpm.HasValue ? $"{Indent(1)}<div class=\"bpm\">\n{Indent(2)}<strong>BPM: </strong>\n{Indent(2)}{HttpUtility.HtmlEncode($"{song.Bpm.Value}")}\n{Indent(1)}</div>\n" : "",
                    !string.IsNullOrWhiteSpace(song.Charter) ? $"{Indent(1)}<div class=\"charter\">\n{Indent(2)}<strong>Charter: </strong>\n{Indent(2)}{HttpUtility.HtmlEncode(song.Charter.Trim())}\n{Indent(1)}</div>\n" : "",
                    $"{Indent(1)}<div class=\"content\">",
                    !string.IsNullOrWhiteSpace(song.Description) ? $"\n{Indent(2)}<p>{HttpUtility.HtmlEncode(song.Description.Trim())}</p>\n{Indent(1)}</div>\n" : "</div>\n",
                    $"{Indent(0)}</div>"
                });
            }

            return AppResources.template.Replace("<!--#SongList#-->", string.Join("", songListHtml));
        }
    }
}
