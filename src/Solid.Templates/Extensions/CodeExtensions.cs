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
        public Code Join<T, M>(List<T> items, Func<T, M> select, string separator = ", ") =>
            code.Join(items, where: static _ => true, select: select, separator: separator);

        /// <summary>
        ///     Joins items in the provided filtered list using the specified separator.
        /// </summary>
        public Code Join<T, M>(List<T> items, Func<T, bool> where, Func<T, M> select, string separator = ", ")
        {
            var first = true;

            foreach (var item in items)
            {
                if (!where(item))
                    continue;

                if (first)
                    first = false;
                else
                    code.Append(separator);

                code.Append(select(item));
            }

            return code;
        }

        /// <summary>
        ///     Joins items in the provided list using the specified separator.
        /// </summary>
        public Code Join<T>(List<T> items, Action<Code, T> append, string separator = ", ") =>
            code.Join(items, where: static _ => true, append, separator);

        /// <summary>
        ///     Joins items in the provided filtered list using the specified separator.
        /// </summary>
        public Code Join<T>(List<T> items, Func<T, bool> where, Action<Code, T> append, string separator = ", ")
        {
            var first = true;

            foreach (var item in items)
            {
                if (!where(item))
                    continue;

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