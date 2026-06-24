using Unit = System.ValueTuple;

namespace Solid.Templates.Controls;

/// <summary>
///     An <c>else</c> control statement that defines an alternative branch of an <c>if</c> condition.
/// </summary>
/// <remarks>
///     For usage examples, see <see cref="Fluent"/> (e.g., <see cref="Fluent.Else()"/>).
/// </remarks>
public readonly record struct Else<S>(S State, Func<S, bool> Condition);

/// <inheritdoc cref="Else{S}"/>
public readonly record struct Else(bool Condition)
{
    /// <summary>
    ///     Converts <see cref="Else"/> to <see cref="Else{Unit}"/>.
    /// </summary>
    public static implicit operator Else<Unit>(Else @else) =>
        new(default, @else.Condition ? static _ => true : static _ => false);
}

public static partial class Fluent
{
    /// <summary>
    ///     <inheritdoc cref="Controls.Else"/>
    /// </summary>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              {If(false)}
    ///              Bye,
    ///              {Else}
    ///              Hello,
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
    /// <seealso cref="If(bool)"/>
    public static Else Else() =>
        new(true);

    /// <summary>
    ///     <inheritdoc cref="Controls.Else"/>
    /// </summary>
    /// <remarks>
    ///     Note that the provided <c>condition</c> is <em>not</em> evaluated lazily. This is
    ///     because it needs to be evaluated before it can be passed to the method, which is then
    ///     used in the interpolated string.
    ///     <para/>
    ///     To enforce laziness, use <see cref="Else(Func{bool})"/> or
    ///     <see cref="Else{S}(S, Func{S, bool})"/>.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              {If(false)}
    ///              Bye,
    ///              {Else(true)}
    ///              Hello,
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
    /// <seealso cref="If(bool)"/>
    public static Else Else(bool condition) =>
        new(condition);

    /// <summary>
    ///     <inheritdoc cref="Controls.Else"/>
    /// </summary>
    /// <remarks>
    ///     The <c>condition</c> predicate is lazily invoked when the <c>else</c> branch needs to
    ///     be evaluated. If a previous branch has already evaluated to <c>true</c>, the predicate
    ///     will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              {If(false)}
    ///              Bye,
    ///              {Else(() => true)}
    ///              Hello,
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
    /// <seealso cref="If(Func{bool})"/>
    public static Else<Func<bool>> Else(Func<bool> condition) =>
        new(condition, static condition => condition());

    /// <summary>
    ///     <inheritdoc cref="Controls.Else"/>
    /// </summary>
    /// <remarks>
    ///     <c>state</c> is lazily passed to the <c>condition</c> predicate when the <c>else</c>
    ///     branch needs to be evaluated. If a previous branch has already evaluated to <c>true</c>,
    ///     the predicate will never be invoked.
    /// </remarks>
    /// <example>
    ///     The following interpolated string:
    ///     <code>
    ///         code.Append(
    ///             $"""
    ///              {If(false)}
    ///              This will not be appended.
    ///              {Else(request, r => r.Flag)}
    ///              Hello?
    ///              {End}
    ///              """)
    ///     </code>
    ///     will append
    ///     <code>
    ///         Hello?
    ///     </code>
    ///     to the code if <c>request.Flag</c> is <c>true</c>, and nothing otherwise.
    /// </example>
    /// <seealso cref="If{S}(S, Func{S, bool})"/>
    public static Else<S> Else<S>(S state, Func<S, bool> condition) =>
        new(state, condition);
}
