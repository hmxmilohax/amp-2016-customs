using DtxCS;
using song_packager.Extensions;
using System.IO.Compression;
using System.Text.Json;

namespace song_packager
{
    internal class Program
    {
        static async Task ArchiveSong(string songSourcePath, string shortName, Stream output)
        {
            using var archive = new ZipArchive(output, ZipArchiveMode.Create, true);

            _ = archive.CreateEntry($"{shortName}/");

            foreach (var file in Directory.GetFiles(songSourcePath).Select(e => new FileInfo(e)))
            {
                _ = await Task.Run(() => archive.CreateEntryFromFile(file.FullName, $"{shortName}/{file.Name}"));
            }
        }
        static void Main(string[] args)
        {
            var songPath = args[0];
            var outputPath = args[1];
            var songs = new List<MoggSong>();

            Directory.CreateDirectory(outputPath);

            foreach (var info in Directory.GetDirectories(songPath).Select(e => new DirectoryInfo(e)))
            {
                var name = info.Name;

                using var dtxStream = File.OpenRead(Path.Combine(songPath, name, $"{name}.moggsong"));
                var root = DTX.FromDtaStream(dtxStream);
                var song = new MoggSong().LoadDta(root);

                var outputName = $"{name}.zip".ReplaceInvalidFilenameChars();

                songs.Add(new MoggSong().LoadDta(root));

                using (var zipFile = File.Create(Path.Combine(outputPath, $"{name}.zip")))
                {
                    Console.WriteLine($"Archiving: {name}.zip");
                    ArchiveSong(info.FullName, name, zipFile).Wait();
                }
            }

            var json = JsonSerializer.Serialize(songs, new JsonSerializerOptions() { WriteIndented = true });

            Console.WriteLine("Writing: info.json");
            File.WriteAllText(Path.Combine(outputPath, "info.json"), json);
        }
    }
}