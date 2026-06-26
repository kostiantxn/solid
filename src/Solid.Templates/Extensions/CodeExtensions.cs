namespace Solid.Templates.Extensions;

/// <summary>
///     Extensions for <see cref="Code"/>.
/// </summary>
public static class CodeExtensions
{
    extension(Code code)
    {
        /// <summary>
        ///     Joins items in the provided list using the specified separator.
        /// </summary>
        public Code Join<T>(IEnumerable<T> items, string separator = ", ") =>
            code.Join(items, static (code, item) => code.Append(item), separator);

        /// <inheritdoc cref="Join{T}(Code, IEnumerable{T}, string)"/>
        public Code Join<T>(IEnumerable<T> items, Action<Code, T> append, string separator = ", ")
        {
            var first = true;

            foreach (var item in items)
            {
                if (first)
                    first = false;
                else
                    code.Append(separator);

                append(code, item);
            }

            return code;
        }
    }
}