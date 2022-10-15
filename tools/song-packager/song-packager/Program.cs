using DtxCS;
using song_packager.Extensions;
using System.IO.Compression;
using System.Text.Json;

namespace song_packager
{
    internal class Program
    {
        public static string DownloadBase = ".";
        private static async Task ArchiveSong(string songSourcePath, string shortName, Stream output)
        {
            using var archive = new ZipArchive(output, ZipArchiveMode.Create, true);

            _ = archive.CreateEntry($"{shortName}/");

            foreach (var file in Directory.GetFiles(songSourcePath).Select(e => new FileInfo(e)))
            {
                _ = await Task.Run(() => archive.CreateEntryFromFile(file.FullName, $"{shortName}/{file.Name}"));
            }
        }

        private static void Main(string[] args)
        {
            string? downloadBase = Environment.GetEnvironmentVariable("DOWNLOAD_BASE");

            if (!string.IsNullOrEmpty(downloadBase))
            {
                DownloadBase = downloadBase.Trim();
            }

            string songPath = args[0];
            string outputPath = args[1];
            var songs = new List<JsonSong>();

            _ = Directory.CreateDirectory(outputPath);

            foreach (var info in Directory.GetDirectories(songPath).Select(e => new DirectoryInfo(e)))
            {
                string name = info.Name;

                using var dtxStream = File.OpenRead(Path.Combine(songPath, name, $"{name}.moggsong"));
                var root = DTX.FromDtaStream(dtxStream);
                var song = new MoggSong().LoadDta(root);

                string outputName = $"{name}.zip".ReplaceInvalidFilenameChars();

                songs.Add(new JsonSong(new MoggSong() { Id = name }.LoadDta(root)));

                using var zipFile = File.Create(Path.Combine(outputPath, $"{name}.zip"));
                Console.WriteLine($"Archiving: {name}.zip");
                //ArchiveSong(info.FullName, name, zipFile).Wait();
            }

            string json = JsonSerializer.Serialize(songs.OrderBy(song => song.Name).OrderBy(song => song.Artist), new JsonSerializerOptions() { WriteIndented = true });

            Console.WriteLine("Writing: songs.json");
            File.WriteAllText(Path.Combine(outputPath, "songs.json"), json);

            Console.WriteLine("Writing: index.html");
            File.WriteAllText(Path.Combine(outputPath, "index.html"), IndexGenerator.GenerateIndex(songs.ToArray()));
        }
    }
}