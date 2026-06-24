using Unit = System.ValueTuple;

namespace Solid.Templates.Controls;

/// <summary>
///     A <c>for</c> control statement that loops over a collection of items and invokes
///     the provided action on each one of them.
/// </summary>
/// <remarks>
///     For usage examples, see <see cref="Fluent"/> (e.g., <see cref="Fluent.For"/>).
/// </remarks>
public readonly record struct For<S, I, A>(S State, Func<S, IEnumerable<I>?> Collection, A Action, Action<Code, S, I, A> Body) : IControl
{
    /// <inheritdoc/>
    public bool Append(Code code, int? alignment = null, string? format = null)
    {
        Format.Validate(format);

        var collection = Collection(State);
        if (collection is null or ICollection<I> { Count: 0 })
            return false;

        code.Indent.Keep(out var previous);

        var first = true;

        if (collection is List<I> list)
        {
            foreach (var item in list)
                Body(this, code, item, format, ref first);
        }
        else if (collection is I[] array)
        {
            foreach (var item in array)
                Body(this, code, item, format, ref first);
        }
        else
        {
            foreach (var item in collection)
                Body(this, code, item, format, ref first);
        }

        code.Indent.Level = previous;

        return !first;

        static void Body(in For<S, I, A> @for, Code code, in I item, string? format, ref bool first)
        {
            if (first)
                first = false;
            else if (format is not null)
                Format.Append(code, format);

            @for.Body(code, @for.State, item, @for.Action);
        }
    }

    private static class Format
    {
        public static void Validate(string? format)
        {
            if (!string.IsNullOrEmpty(format) && format![format.Length - 1] != ':')
                throw new FormatException("Format must end with semicolon");
        }

        public static void Append(Code code, string format)
        {
            // -1 to skip the closing `:`.
            for (var i = 0; i < format.Length - 1; ++i)
            {
                if (format[i] is '\\' && format[i + 1] is 'n')
                {
                    code.Line();
                    ++i;
                }
                else
                    code.Append(format[i]);
            }
        }
    }
}

public static partial class Fluent
{
    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For(["Hello", "world"]):, :}")
    ///     </code>
    ///     will append
    ///     <code>
    ///         Hello, world!
    ///     </code>
    ///     to the code.
    ///     <para/>
    ///     The <c>:, :</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>, </c>) is inserted between the joined
    ///     items.
    /// </example>
    public static For<IEnumerable<object>, object, Unit> For(IEnumerable<object> list) =>
        new(list, x => x, default, static (code, _, item, _) => code.Append(item));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For([24, 8, 1991]):/:}")
    ///     </code>
    ///     will append
    ///     <code>
    ///         24/8/1991
    ///     </code>
    ///     to the code.
    ///     <para/>
    ///     The <c>:, :</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>, </c>) is inserted between the joined
    ///     items.
    /// </example>
    public static For<IEnumerable<I>, I, Unit> For<I>(IEnumerable<I> list) where I : IFormattable =>
        new(list, x => x, default, static (code, _, item, _) => code.Append(item));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>list</c> function is lazily invoked when the collection needs to be appended.
    ///     If the <c>for</c> loop appears inside a <c>false</c> <c>if</c> branch, the function
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For(song, s => s.Artists): and :}")
    ///     </code>
    ///     will append
    ///     <code>
    ///         Joey Valence and Brae
    ///     </code>
    ///     to the code if <c>song.Artists</c> contains values <c>Joey Valence</c> and <c>Brae</c>.
    ///     <para/>
    ///     The <c>: and :</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c> and </c>) is inserted between the
    ///     joined items.
    /// </example>
    public static For<S, object, Unit> For<S>(S state, Func<S, IEnumerable<object>?> list) =>
        new(state, list, default, static (code, _, item, _) => code.Append(item));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>list</c> function is lazily invoked when the collection needs to be appended.
    ///     If the <c>for</c> loop appears inside a <c>false</c> <c>if</c> branch, the function
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For(set, s => s.Members):, :}")
    ///     </code>
    ///     will append
    ///     <code>
    ///         70, 836, 4030
    ///     </code>
    ///     if <c>set.Members</c> contains values <c>70</c>, <c>836</c>, and <c>4030</c>.
    ///     <para/>
    ///     The <c>:, :</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>, </c>) is inserted between the
    ///     joined items.
    /// </example>
    public static For<S, I, Unit> For<S, I>(S state, Func<S, IEnumerable<I>?> list) where I : IFormattable =>
        new(state, list, default, static (code, _, item, _) => code.Append(item));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>append</c> function is lazily invoked when the collection needs to be appended.
    ///     If the <c>for</c> loop appears inside a <c>false</c> <c>if</c> branch, the function
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For(["banana", "coffee", "cardamom"], append):, :}")
    ///     </code>
    ///     will invoke the specified <c>append</c> action for each item in the provided list,
    ///     and given
    ///     <code>
    ///         (code, item) => code.Append(item.ToUpper())
    ///     </code>
    ///     this will append
    ///     <code>
    ///         BANANA, COFFEE, CARDAMOM
    ///     </code>
    ///     to the code.
    ///     <para/>
    ///     The <c>:, :</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>, </c>) is inserted between the
    ///     joined items.
    /// </example>
    public static For<IEnumerable<I>, I, Action<Code, I>> For<I>(IEnumerable<I> list, Action<Code, I> append) =>
        new(list, x => x, append, static (code, _, item, append) => append(code, item));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>append</c> function is lazily invoked when the collection needs to be appended.
    ///     If the <c>for</c> loop appears inside a <c>false</c> <c>if</c> branch, the function
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For(recipe, r => r.Ingredients, append):\n:}")
    ///     </code>
    ///     will invoke the specified <c>append</c> action for each item in the provided list.
    ///     <para/>
    ///     The <c>:\n:</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>\n</c>) is inserted between the
    ///     joined items.
    /// </example>
    public static For<S, I, Action<Code, I>> For<S, I>(S state, Func<S, IEnumerable<I>?> list, Action<Code, I> append) =>
        new(state, list, append, static (code, _, item, append) => append(code, item));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>append</c> function is lazily invoked when the collection needs to be appended.
    ///     If the <c>for</c> loop appears inside a <c>false</c> <c>if</c> branch, the function
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For(recipe, r => r.Ingredients, append):\n:}")
    ///     </code>
    ///     will invoke the specified <c>append</c> action for each item in the provided list.
    ///     <para/>
    ///     The <c>:\n:</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>\n</c>) is inserted between the
    ///     joined items.
    /// </example>
    public static For<S, I, Action<Code, S, I>> For<S, I>(S state, Func<S, IEnumerable<I>?> list, Action<Code, S, I> append) =>
        new(state, list, append, static (code, state, item, append) => append(code, state, item));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>append</c> function is lazily invoked when the collection needs to be appended.
    ///     If the <c>for</c> loop appears inside a <c>false</c> <c>if</c> branch, the function
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For([1, 2, 3], append):\n:}")
    ///     </code>
    ///     will invoke the specified <c>append</c> action for each item in the provided list.
    ///     <para/>
    ///     The <c>:\n:</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>\n</c>) is inserted between the
    ///     joined items.
    /// </example>
    public static For<IEnumerable<I>, I, Func<I, Code.Interpolation>> For<I>(IEnumerable<I> list, Func<I, Code.Interpolation> append) =>
        new(list, x => x, append, static (code, _, item, append) => code.Append(append(item)));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>append</c> function is lazily invoked when the collection needs to be appended.
    ///     If the <c>for</c> loop appears inside a <c>false</c> <c>if</c> branch, the function
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For(recipe, r => r.Ingredients, append):\n:}")
    ///     </code>
    ///     will invoke the specified <c>append</c> action for each item in the provided list.
    ///     <para/>
    ///     The <c>:\n:</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>\n</c>) is inserted between the
    ///     joined items.
    /// </example>
    public static For<S, I, Func<I, Code.Interpolation>> For<S, I>(S state, Func<S, IEnumerable<I>?> list, Func<I, Code.Interpolation> append) =>
        new(state, list, append, static (code, _, item, append) => code.Append(append(item)));

    /// <summary>
    ///     <inheritdoc cref="For{S, I, A}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>append</c> function is lazily invoked when the collection needs to be appended.
    ///     If the <c>for</c> loop appears inside a <c>false</c> <c>if</c> branch, the function
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"{For(recipe, r => r.Ingredients, append):\n:}")
    ///     </code>
    ///     will invoke the specified <c>append</c> action for each item in the provided list.
    ///     <para/>
    ///     The <c>:\n:</c> format parameter is used to indicate the join separator: everything
    ///     between <c>:</c> (in the example above, it's <c>\n</c>) is inserted between the
    ///     joined items.
    /// </example>
    public static For<S, I, Func<S, I, Code.Interpolation>> For<S, I>(S state, Func<S, IEnumerable<I>?> list, Func<S, I, Code.Interpolation> append) =>
        new(state, list, append, static (code, state, item, append) => code.Append(append(state, item)));
}
