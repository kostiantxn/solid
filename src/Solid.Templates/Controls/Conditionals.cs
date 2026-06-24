using System.Buffers;
using Solid.Templates.Collections;
using Unit = System.ValueTuple;

namespace Solid.Templates.Controls;

/// <summary>
///     A conditional statement in the interpolated template.
/// </summary>
/// <param name="True">
///     Whether the current conditional branch is <c>true</c>.
/// </param>
/// <param name="Done">
///     Whether the conditional statement is done:
///     <list type="bullet">
///         <item>
///             <c>null</c> means that it is still being processed (i.e., a <c>true</c>
///             branch hasn't been processed yet);
///         </item>
///         <item>
///             <c>true</c> means that a <c>true</c> branch has been processed, and thus
///             any subsequent <see cref="Controls.Else"/> should be discarded;
///         </item>
///         <item>
///             <c>false</c> means that this is a nested conditional under a <c>false</c>
///             branch, which can never be <c>true</c> (even if there is a branch condition
///             that evaluates to <c>true</c>).
///         </item>
///     </list>
/// </param>
internal readonly record struct Conditional(bool True, bool? Done);

/// <summary>
///     A stack of conditional statements.
/// </summary>
internal struct Conditionals() : IDisposable
{
    private PooledStack<Conditional> _stack = new(ArrayPool<Conditional>.Shared);

    /// <summary>
    ///     Whether the top conditional is <c>true</c>.
    /// </summary>
    public bool True => !_stack.Peek(out var top) || top.True;

    /// <summary>
    ///     Whether the stack of conditionals is empty.
    /// </summary>
    public bool Empty => _stack.Empty;

    /// <summary>
    ///     Handles the provided <see cref="Controls.If{S}"/> statement.
    /// </summary>
    public void Handle<S>(If<S> @if, out Conditional inserted)
    {
        if (True)
            _stack.Push(inserted = new(@if.Condition(@if.State), Done: null));
        else
            _stack.Push(inserted = new(false, Done: false));
    }

    /// <summary>
    ///     Handles the provided <see cref="Controls.If"/> statement.
    /// </summary>
    public void Handle(If @if, out Conditional inserted) =>
        Handle((If<Unit>) @if, out inserted);

    /// <summary>
    ///     Handles the provided <see cref="Controls.Else{S}"/> statement.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Unexpected <see cref="Controls.Else{S}"/>.
    /// </exception>
    public void Handle<S>(Else<S> @else)
    {
        if (!_stack.Pop(out var top))
            throw new InvalidOperationException("Unexpected 'Else' was provided");

        if (top.True)
            _stack.Push(new(false, Done: true));
        else if (top.Done is null)
            _stack.Push(new(@else.Condition(@else.State), Done: null));
        else
            _stack.Push(top);
    }

    /// <summary>
    ///     Handles the provided <see cref="Controls.Else"/> statement.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Unexpected <see cref="Controls.Else"/>.
    /// </exception>
    public void Handle(Else @else) =>
        Handle((Else<Unit>) @else);

    /// <summary>
    ///     Handles the provided <see cref="Controls.End"/> statement and closes the top
    ///     <see cref="Controls.If{S}"/> statement.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Unexpected <see cref="Controls.End"/>.
    /// </exception>
    public void Handle(End end, out Conditional removed)
    {
        if (!_stack.Pop(out removed))
            throw new InvalidOperationException("Unexpected 'End' was provided");
    }

    /// <summary>
    ///     Clears the stack of conditionals and returns the rented array to the pool.
    /// </summary>
    public void Dispose() =>
        _stack.Return();
}
