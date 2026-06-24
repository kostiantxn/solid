namespace Solid.Templates.Controls;

/// <summary>
///     A block of indented code.
/// </summary>
/// <remarks>
///     For usage examples, see <see cref="Fluent"/> (e.g., <see cref="Fluent.Block"/>).
/// </remarks>
// TODO: Use `ref` fields if the target runtime supports it.
public readonly record struct Block<S>(S State, Action<Code, S>? Body) : IControl
{
    /// <inheritdoc/>
    public bool Append(Code code, int? alignment = null, string? format = null)
    {
        if (Body is null)
            return false;

        var length = code.Length;

        code.Indent.Keep(out var previous);

        Body(code, State);

        code.Indent.Level = previous;

        // If the length hasn't changed (i.e., the provided `append` action did not modify
        // the code), we would like to trim the previous newline character to avoid producing
        // an empty line.
        // 
        // This check is not ideal, as it assumes that text cannot be removed from the code
        // builder and that changes can only be additive.
        return code.Length != length;
    }
}

public static partial class Fluent
{
    /// <summary>
    ///     <inheritdoc cref="Controls.Block{S}"/>
    /// </summary>
    /// <remarks>
    ///     The <c>body</c> function is lazily invoked when the block needs to be appended.
    ///     If this block appears inside a <c>false</c> <c>if</c> branch, the function will never
    ///     be invoked.
    /// </remarks>
    /// <example>
    ///     In the following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              [
    ///                  {Block(state, Body)}
    ///              ]
    ///              """)
    ///     </code>
    ///     <see cref="Controls.Block{S}"/> will calculate the current indentation level of
    ///     the line where it is interpolated, and then use it to indent every line produced by
    ///     <c>Body(state)</c>. If, for example, <c>Body(state)</c> appends
    ///     <code>
    ///         // Hello,
    ///         // world!
    ///     </code>
    ///     then the interpolated string above will append
    ///     <code>
    ///         [
    ///             // Hello,
    ///             // world!
    ///         ]
    ///     </code>
    ///     to the code.
    /// </example>
    public static Block<S> Block<S>(S state, Action<Code, S> body) =>
        new(state, body);
}
