namespace Solid.Templates.Controls;

/// <summary>
///     A helper class for building fluent interpolation strings.
/// </summary>
/// <remarks>
///     This class defines methods for <c>if</c>/<c>else</c>/<c>end</c> conditions, <c>for</c> loops,
///     as well as indented code blocks.
///     <para/>
///     It is intended to be imported via
///     <code>
///         using static Solid.Templates.Controls.Fluent;
///     </code>
/// </remarks>
/// <example>
///     Given <c>number</c> = <c>2014</c>, the following interpolated string:
///     <code>
///         code.Append(
///             $"""
///              Number {number} is:
///              {If(number == 0)}
///                  - zero.
///              {Else}
///                  {If(number > 0)}
///                  - positive,
///                  {Else}
///                  - negative,
///                  {End}
///                  {If(number % 2 == 0)}
///                  - even.
///                  {Else}
///                  - odd.
///                  {End}
///              {End}
///              """)
///     </code>
///     will append
///     <code>
///         Number 2014 is:
///             - positive,
///             - even.
///     </code>
///     to the code.
///     <para/>
///     In the example above, <c>if</c>/<c>else</c>/<c>end</c> conditions control the flow of
///     interpolation: only literals under <c>true</c> <c>if</c> conditions are appended to the code.
/// </example>
public static partial class Fluent;
