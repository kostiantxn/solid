namespace Solid.Templates.Extensions;

/// <summary>
///     Extensions for <see cref="string"/>.
/// </summary>
internal static class StringExtensions
{
    extension(string self)
    {
        /// <summary>
        ///     Checks whether the string starts with any of the provided prefixes.
        /// </summary>
        public bool StartsWith(string[] prefixes, out string? start)
        {
            start = null;

            foreach (var prefix in prefixes)
            {
                if (self.StartsWith(prefix))
                {
                    start = prefix;
                    return true;
                }
            }

            return false;
        }
    }
}