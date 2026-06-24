namespace Solid.Templates.Controls;

/// <summary>
///     An <c>end</c> control statement that closes an <c>if</c> condition.
/// </summary>
/// <remarks>
///     For usage examples, see <see cref="Fluent"/> (e.g., <see cref="Fluent.End"/>).
/// </remarks>
public readonly record struct End;

public static partial class Fluent
{
    /// <summary>
    ///     <inheritdoc cref="Controls.End"/>
    /// </summary>
    /// <remarks>
    ///     While this method itself doesn't throw any exceptions, an incorrectly interpolated
    ///     <c>end</c> will cause <seealso cref="InvalidOperationException"/>.
    /// </remarks>
    /// <seealso cref="If(bool)"/>
    /// <seealso cref="Else(bool)"/>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              {If(true)}
    ///              Hello,
    ///              {Else}
    ///              Bye,
    ///              {End}
    ///              world!
    ///              """)
    ///     </code>
    ///     will append
    ///     <code>
    ///         Hello,
    ///         world!
    ///     </code>
    ///     to the code.
    ///     <para/>
    ///     A misplaced <c>end</c> will cause <seealso cref="InvalidOperationException"/>.
    ///     For example,
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              {If(false)}
    ///              Oops...
    ///              {End}
    ///              {End}
    ///              """)
    ///     </code>
    ///     throws an exception since the second <c>end</c> doesn't have an <c>if</c> to close.
    /// </example>
    public static End End() =>
        default;
}
