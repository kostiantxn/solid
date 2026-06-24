using System.Text;

namespace Solid.Templates.Extensions;

/// <summary>
///     Extensions for <see cref="StringBuilder"/>.
/// </summary>
internal static class StringBuilderExtensions
{
    extension(StringBuilder self)
    {
        /// <summary>
        ///     Appends the provided span to the builder.
        /// </summary>
        public unsafe void Append(ReadOnlySpan<char> span)
        {
            if (span.IsEmpty)
                return;

            fixed (char* pointer = span)
                self.Append(pointer, span.Length);
        }

        /// <summary>
        ///     Checks whether the builder ends with the provided suffix.
        /// </summary>
        public bool EndsWith(string suffix)
        {
            if (self.Length < suffix.Length)
                return false;

            var i = 0;
            var j = self.Length - suffix.Length;

            while (i < suffix.Length)
                if (suffix[i++] != self[j++])
                    return false;

            return true;
        }

        /// <summary>
        ///     Removes trailing occurrences of the specified character from the builder
        ///     until the provided index.
        /// </summary>
        public StringBuilder TrimEnd(char character, int until = 0)
        {
            while (self.Length > until && self[self.Length - 1] == character)
                self.Length--;

            return self;
        }

        /// <summary>
        ///     Removes trailing occurrences of the specified suffixes from the builder
        ///     the provided number of times.
        /// </summary>
        public StringBuilder TrimEnd(string[] suffixes, int count)
        {
            while (--count >= 0)
            {
                foreach (var suffix in suffixes)
                {
                    if (self.EndsWith(suffix))
                    {
                        self.Length -= suffix.Length;
                        break;
                    }
                }
            }

            return self;
        }
    }
}
