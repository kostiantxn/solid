using System.Runtime.CompilerServices;
using Solid.Templates.Extensions;
using Solid.Templates.Controls;
using Unit = System.ValueTuple;

namespace Solid.Templates;

public partial class Code
{
    /// <summary>
    ///     Async local context that allows the code to be reused in nested interpolations.
    /// </summary>
    private static readonly AsyncLocal<Code> Context = new();

    /// <summary>
    ///     An interpolated string handler that can be used to format code using conditional
    ///     statements and for loops (similar to Liquid templates).
    /// </summary>
    /// <example>
    ///     <inheritdoc cref="Controls.Fluent"/>
    /// </example>
    [InterpolatedStringHandler]
    public struct Interpolation : IDisposable
    {
        private readonly Code _code;

        private Conditionals _conditionals = new();

        private bool _trim;
        private int _until;

        /// <summary>
        ///     The <see cref="Code"/> instance that the handler appends code to.
        /// </summary>
        public Code Code => _code;

        /// <summary>
        ///     Creates a new instance of <see cref="Interpolation"/>.
        /// </summary>
        public Interpolation(int length, int count, Code? code = null)
        {
            _code = code ?? Context.Value ?? new Code();

            _ = length;
            _ = count;
        }

        /// <summary>
        ///     Appends the provided string literal.
        /// </summary>
        /// <remarks>
        ///     The literal is skipped if it is under a <c>false</c> conditional.
        /// </remarks>
        public void AppendLiteral(string? text)
        {
            if (_conditionals.True)
                AppendLiteralWithoutCheck(text);
        }

        /// <summary>
        ///     Appends the specified literal.
        /// </summary>
        /// <remarks>
        ///     Does not check whether the top condition is <c>true</c>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AppendLiteralWithoutCheck(string? text)
        {
            if (text is null)
                return;

            if (_trim)
            {
                var index = _code.Builder.TrimEndIndex(_code.Indent.Character, _until);
                if (index is -1 || _code.Builder.EndsWith(Lines.Default, from: index))
                {
                    _code.Builder.Length = index + Lines.Default.Length;
                    _code._line = true;
                    _until = index + 1;
                }

                if (text.StartsWith(Lines.All, out var start))
                    _code.Append(text.AsMemory(start!.Length));
                else
                    _code.Append(text);

                _trim = false;
            }
            else
                _code.Append(text);
        }

        /// <summary>
        ///     Appends the provided string literal.
        /// </summary>
        /// <remarks>
        ///     The literal is skipped if it is under a <c>false</c> conditional.
        /// </remarks>
        public void AppendFormatted(string? text) =>
            AppendLiteral(text);

        /// <summary>
        ///     Appends the provided boolean.
        /// </summary>
        /// <remarks>
        ///     The boolean is skipped if it is under a <c>false</c> conditional.
        /// </remarks>
        public void AppendFormatted(bool boolean)
        {
            if (_conditionals.True)
            {
                _code.Append(boolean);
                _trim = false;
            }
        }

        /// <summary>
        ///     Appends the provided number.
        /// </summary>
        /// <remarks>
        ///     The number is skipped if it is under a <c>false</c> conditional.
        /// </remarks>
        public void AppendFormatted(byte number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(sbyte number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(short number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(ushort number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(int number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(uint number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(nint number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(nuint number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(long number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(ulong number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(float number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(double number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <inheritdoc cref="AppendFormatted(byte)"/>
        public void AppendFormatted(decimal number)
        {
            if (_conditionals.True)
            {
                _code.Append(number);
                _trim = false;
            }
        }

        /// <summary>
        ///     Appends the provided value converted to string.
        /// </summary>
        /// <remarks>
        ///     The value is skipped if it is under a <c>false</c> conditional.
        /// </remarks>
        public void AppendFormatted(object? value, string? format = null)
        {
            if (value is IFormattable formattable)
                AppendFormatted(formattable, format);
            else if (_conditionals.True)
                AppendLiteralWithoutCheck(value?.ToString());
        }

        /// <inheritdoc cref="AppendFormatted(object?, string?)"/>
        public void AppendFormatted<T>(T? value, string? format = null) where T : IFormattable
        {
            if (_conditionals.True)
                AppendLiteralWithoutCheck(value?.ToString(format, null));
        }

        /// <inheritdoc cref="AppendFormatted{S}(Controls.If{S})"/>
        public void AppendFormatted(Func<If> @if) =>
            AppendFormatted(@if());

        /// <inheritdoc cref="AppendFormatted{S}(Controls.If{S})"/>
        public void AppendFormatted(If @if) =>
            AppendFormatted((If<Unit>) @if);

        /// <inheritdoc cref="AppendFormatted{S}(Controls.If{S})"/>
        public void AppendFormatted<S>(Func<If<S>> @if) =>
            AppendFormatted(@if());

        /// <inheritdoc cref="Controls.Conditionals.Handle{S}(Controls.If{S}, out Controls.Conditional)"/>
        public void AppendFormatted<S>(If<S> @if)
        {
            _conditionals.Handle(@if, out var inserted);

            // If the inserted conditional is already done and `false` (i.e., this is a nested
            // `If` statement inside a `false` branch, which can never be `true`), then there is
            // nothing we need to trim since neither preceding nor subsequent literals wouldn't
            // have been appended.
            if (inserted.Done is not false)
                _trim = true;
        }

        /// <inheritdoc cref="AppendFormatted{S}(Controls.Else{S})"/>
        public void AppendFormatted(Func<Else> @else) =>
            AppendFormatted(@else());

        /// <inheritdoc cref="AppendFormatted{S}(Controls.Else{S})"/>
        public void AppendFormatted(Else @else) =>
            AppendFormatted((Else<Unit>) @else);

        /// <inheritdoc cref="AppendFormatted{S}(Controls.Else{S})"/>
        public void AppendFormatted<S>(Func<Else<S>> @else) =>
            AppendFormatted(@else());

        /// <inheritdoc cref="Controls.Conditionals.Handle{S}(Controls.Else{S})"/>
        public void AppendFormatted<S>(Else<S> @else) =>
            _conditionals.Handle(@else);

        /// <inheritdoc cref="AppendFormatted(Controls.End)"/>
        public void AppendFormatted(Func<End> end) =>
            AppendFormatted(default(End));

        /// <inheritdoc cref="Controls.Conditionals.Handle(Controls.End, out Controls.Conditional)"/>
        public void AppendFormatted(End end)
        {
            _conditionals.Handle(end, out var removed);
 
            // If the removed conditional is already done and `false` (i.e., this is a nested
            // `If` statement inside a `false` branch, which can never be `true`), then there is
            // nothing we need to trim since neither preceding nor subsequent literals wouldn't
            // have been appended.
            if (removed.Done is false)
                return;

            if (!_trim || _code.Builder.Length > 0)
            {
                var index = _code.Builder.TrimEndIndex(_code.Indent.Character, _until);
                if (index is -1)
                    _code.Builder.Length = 0;
                else if (index >= _until && _code.Builder.EndsWith(Lines.Default, from: index))
                    _code.Builder.Length = index;

                _trim = false;
            }
            else
                _trim = true;

            _until = _code.Builder.Length;
        }

        /// <summary>
        ///     Appends the provided control block to the code.
        /// </summary>
        /// <remarks>
        ///     The value is skipped if it is under a <c>false</c> conditional.
        /// </remarks>
        public void AppendFormatted<C>(in C control, int? alignment = null, string? format = null) where C : IControl
        {
            if (!_conditionals.True)
                return;

            // Save the code we're currently appending to in the async local context, to allow
            // blocks to reuse it when creating nested interpolations.
            var replaced = Context.Value;

            Context.Value = _code;

            if (!control.Append(_code, alignment, format))
                _trim = true;

            Context.Value = replaced;
        }

        /// <summary>
        ///     Invokes the provided action to append text, taking into account any
        ///     preceding indentation.
        /// </summary>
        /// <remarks>
        ///     The value is skipped if it is under a <c>false</c> conditional.
        /// </remarks>
        /// <example>
        ///     The following template:
        ///     <code>
        ///     $$"""
        ///     {
        ///         {{Body}}
        ///     }
        ///     """
        ///     </code>
        ///     will indent every line produced by the <c>Body</c> method and generate something
        ///     similar to the following:
        ///     <code>
        ///     {
        ///         // Some
        ///         // class
        ///         // body.
        ///     }
        ///     </code>
        /// </example>
        public void AppendFormatted(Action<Code>? append) =>
            AppendFormatted(new Block<Action<Code>?>(append, static (code, append) =>
            {
                if (append is not null)
                    code.Append(append);
            }));

        /// <summary>
        ///     Throws an exception if the interpolated template is invalid (e.g., there is an open
        ///     <see cref="Controls.If"/> without a corresponding <see cref="Controls.End"/> to
        ///     close it).
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     The interpolated template is invalid.
        /// </exception>
        public void Validate()
        {
            if (!_conditionals.Empty)
                throw new InvalidOperationException("A conditional statement has been left open");
        }

        /// <summary>
        ///     Disposes of the interpolation template.
        /// </summary>
        public void Dispose() =>
            _conditionals.Dispose();

        /// <summary>
        ///     Completes the interpolation.
        /// </summary>
        public void Complete()
        {
            Validate();

            // If `_trim` is `true` when we complete the interpolation, then it means that the very
            // last appended object was a no-op control statement/action, which was supposed to
            // trim the first newline character of the subsequent literal. However, since we are
            // now completing the interpolation, there won't be any more literals to trim, so we
            // will try to trim that newline character from the end instead.
            if (_trim)
                _code.Builder.TrimEnd(Lines.Default, count: 1);

            Dispose();
        }
    }
}