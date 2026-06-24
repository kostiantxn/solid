namespace Solid.Templates.Extensions;

/// <summary>
///     Extensions for <see cref="ReadOnlySpan{T}"/>.
/// </summary>
internal static class ReadOnlyMemoryExtensions
{
    extension(ReadOnlyMemory<char> self)
    {
        /// <summary>
        ///     Splits the memory by the specified separators.
        /// </summary>
        public LinesEnumerator Lines() =>
            new(self);
    }

    /// <summary>
    ///     An enumerator over memory split by newline characters (CRLF, CR, and LF).
    /// </summary>
    public struct LinesEnumerator(ReadOnlyMemory<char> memory)
    {
        private int _start = -1;
        private int _end;
        private int _next = 0;

        public ReadOnlySpan<char> Current =>
            _start >= 0 ? memory.Span.Slice(_start, _end - _start) : throw new InvalidOperationException();

        public bool MoveNext()
        {
            _start = _next;

            if (_start > memory.Length)
                return false;

            var span = memory.Span.Slice(_start);
            var next = span.IndexOfAny('\r', '\n');
            if (next >= 0)
            {
                _end = _start + next;

                if (next < span.Length - 1 && span[next] is '\r' && span[next + 1] is '\n')
                    _next = _end + 2;
                else
                    _next = _end + 1;
            }
            else
            {
                _end = memory.Length;
                _next = _end + 1;
            }

            return true;
        }

        public LinesEnumerator GetEnumerator() =>
            this;
    }
}