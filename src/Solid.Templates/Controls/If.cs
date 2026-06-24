using Unit = System.ValueTuple;

namespace Solid.Templates.Controls;

/// <summary>
///     An <c>if</c> control statement.
/// </summary>
/// <remarks>
///     For usage examples, see <see cref="Fluent"/> (e.g., <see cref="Fluent.If(bool)"/>).
/// </remarks>
public readonly record struct If<S>(S State, Func<S, bool> Condition);

/// <inheritdoc cref="If{S}"/>
public readonly record struct If(bool Condition)
{
    /// <summary>
    ///     Converts <see cref="If"/> to <see cref="If{Unit}"/>.
    /// </summary>
    public static implicit operator If<Unit>(If @if) =>
        new(default, @if.Condition ? static _ => true : static _ => false);
}

public static partial class Fluent
{
    /// <summary>
    ///     <inheritdoc cref="Controls.If"/>
    /// </summary>
    /// <remarks>
    ///     Note that the provided <c>condition</c> is <em>not</em> evaluated lazily. This is
    ///     because it needs to be evaluated before it can be passed to the method, which is then
    ///     used in the interpolated string.
    ///     <para/>
    ///     To enforce laziness, use <see cref="If(Func{bool})"/> or
    ///     <see cref="If{S}(S, Func{S, bool})"/>.
    /// </remarks>
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
    /// </example>
    /// <seealso cref="Else(bool)"/>
    public static If If(bool condition) =>
        new(condition);

    /// <summary>
    ///     <inheritdoc cref="Controls.If"/>
    /// </summary>
    /// <remarks>
    ///     The <c>condition</c> predicate is lazily invoked when the <c>if</c> branch needs to
    ///     be evaluated. If the <c>if</c> condition appears inside another <c>false</c> <c>if</c>
    ///     condition, the predicate will not be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              {If(() => true)}
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
    /// </example>
    /// <seealso cref="Else(Func{bool})"/>
    public static If<Func<bool>> If(Func<bool> condition) =>
        new(condition, static condition => condition());

    /// <summary>
    ///     <inheritdoc cref="Controls.If"/>
    /// </summary>
    /// <remarks>
    ///     The <c>condition</c> predicate is lazily invoked when the <c>if</c> branch needs to
    ///     be evaluated. If the <c>if</c> condition appears inside another <c>false</c> <c>if</c>
    ///     condition, the predicate will not be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              {If(request, r => r.Flag)}
    ///              Hello?
    ///              {Else(false)}
    ///              This will not be appended.
    ///              {End}
    ///              """)
    ///     </code>
    ///     will append
    ///     <code>
    ///         Hello?
    ///     </code>
    ///     to the code if <c>request.Flag</c> is <c>true</c>, and nothing otherwise.
    /// </example>
    /// <seealso cref="Else{S}(S, Func{S, bool})"/>
    public static If<S> If<S>(S state, Func<S, bool> condition) =>
        new(state, condition);
}
