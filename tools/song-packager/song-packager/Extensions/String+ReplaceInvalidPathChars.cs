namespace song_packager.Extensions
{
    internal static class String_ReplaceInvalidPathChars
    {
        internal static string ReplaceInvalidPathChars(this string input)
        {
            return string.Join("_", input.Split(Path.GetInvalidPathChars()));
        }
    }
}
