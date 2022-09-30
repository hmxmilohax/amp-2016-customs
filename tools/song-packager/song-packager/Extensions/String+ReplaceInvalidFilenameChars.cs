namespace song_packager.Extensions
{
    internal static class String_ReplaceInvalidFilenameCharsExtension
    {
        internal static string ReplaceInvalidFilenameChars(this string input)
        {
            return string.Join("_", input.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
