namespace Solid.Templates.Controls;

/// <summary>
///     A reusable control block which, when interpolated, can configure or append code.
/// </summary>
/// <example>
///     Several control blocks are supported out of the box, such as <see cref="For{S, I, A}"/>,
///     which loops over a collection of items to append each one.
/// </example>
public interface IControl
{
    /// <summary>
    ///     Appends the control block to the specified code.
    /// </summary>
    bool Append(Code code, int? alignment = null, string? format = null);
}
