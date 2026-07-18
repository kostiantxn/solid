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
        ///     Checks whether the builder ends with the provided suffix.
        /// </summary>
        public bool EndsWith(string suffix, int from)
        {
            if (from + 1 < suffix.Length)
                return false;

            var i = 0;
            var j = from + 1 - suffix.Length;

            while (i < suffix.Length)
                if (suffix[i++] != self[j++])
                    return false;

            return true;
        }

        /// <summary>
        ///     Returns the last index that indicates how far the character would have been trimmed.
        /// </summary>
        public int TrimEndIndex(char character, int until)
        {
            var index = self.Length - 1;

            while (index >= until && index >= 0 && self[index] == character)
                index--;

            return index;
        }

        /// <summary>
        ///     Removes trailing occurrences of the suffix from the builder
        ///     the provided number of times.
        /// </summary>
        public StringBuilder TrimEnd(string suffix, int count)
        {
            while (--count >= 0)
            {
                if (self.EndsWith(suffix))
                {
                    self.Length -= suffix.Length;
                    break;
                }
            }

            return self;
        }
    }
}