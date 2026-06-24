using System.Runtime.CompilerServices;
using System.Text;
using Solid.Templates.Extensions;

namespace Solid.Templates;

/// <summary>
///     A simple wrapper around <see cref="StringBuilder"/> to make it easier to generate code
///     with correct indentations.
/// </summary>
public partial class Code
{
    private bool _line = true;

    /// <summary>
    ///     Initializes a new instance of <see cref="Code"/>.
    /// </summary>
    public Code(StringBuilder? builder = null)
    {
        Builder = builder ?? new();
        Indent = new(this);
    }

    /// <summary>
    ///     Returns the current length of the code.
    /// </summary>
    public int Length => Builder.Length;

    /// <inheritdoc cref="Indent"/>
    public Indent Indent { get; }

    /// <summary>
    ///     The underlying string builder.
    /// </summary>
    public StringBuilder Builder { get; }

    /// <summary>
    ///     Appends the provided value to the code.
    /// </summary>
    /// <remarks>
    ///     The provided string is split by newline strings to allow every individual line
    ///     to be indented correctly.
    /// </remarks>
    public Code Append(object? value) =>
        value is IFormattable formattable ? Append(formattable) : Append(value?.ToString());

    /// <inheritdoc cref="Append(object?)"/>
    public Code Append<T>(T? value, string? format = null) where T : IFormattable =>
        Append(value?.ToString(format, null));

    /// <inheritdoc cref="Append(object?)"/>
    public Code Append(string? value) =>
        value is not null ? Append(value.AsMemory()) : this;

    /// <inheritdoc cref="Append(object?)"/>
    public Code Append(ReadOnlyMemory<char> value)
    {
        Indent.Append(ref _line);

        var first = true;

        foreach (var line in value.Lines())
        {
            if (first)
                first = false;
            else
            {
                Builder.Append(Lines.Default);
                Indent.Append();
            }

            Builder.Append(line);
        }

        return this;
    }

    /// <summary>
    ///     Appends the provided character to the code.
    /// </summary>
    public Code Append(char character)
    {
        if (character is '\r' or '\n')
            return Line();

        Indent.Append(ref _line);
        Builder.Append(character);
        return this;
    }

    /// <summary>
    ///     Appends the provided character to the code specified number of times.
    /// </summary>
    public Code Append(char character, int count)
    {
        Indent.Append(ref _line);
        Builder.Append(character, count);
        return this;
    }

    /// <summary>
    ///     Appends the specified boolean value to the code.
    /// </summary>
    public Code Append(bool boolean)
    {
        Indent.Append(ref _line);
        Builder.Append(boolean);
        return this;
    }

    /// <summary>
    ///     Appends the provided number to the code.
    /// </summary>
    public Code Append(byte number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(sbyte number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(short number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(ushort number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(int number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(uint number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(nint number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(nuint number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(long number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(ulong number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(float number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(double number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <inheritdoc cref="Append(byte)"/>
    public Code Append(decimal number)
    {
        Indent.Append(ref _line);
        Builder.Append(number);
        return this;
    }

    /// <summary>
    ///     Invokes the provided action to append code.
    /// </summary>
    public Code Append(Action<Code> action)
    {
        action(this);
        return this;
    }

    /// <summary>
    ///     Appends an interpolated string to the code.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Interpolation is invalid (e.g., there is an open <see cref="Controls.If{S}"/> without
    ///     a corresponding <see cref="Controls.End"/> to close it).
    /// </exception>
    public Code Append([InterpolatedStringHandlerArgument("")] Interpolation interpolation)
    {
        // Check if the provided interpolation already appended to the code. This can normally
        // happen if the code was resued from the async local context.
        if (!ReferenceEquals(this, interpolation.Code))
            Append(interpolation.Code.ToString());

        interpolation.Complete();
        return this;
    }

    /// <summary>
    ///     Appends a newline string.
    /// </summary>
    public Code Line()
    {
        Indent.Append(ref _line);
        Builder.Append(Lines.Default);

        _line = true;

        return this;
    }

    /// <inheritdoc/>
    public override string ToString() =>
        Builder.ToString();

    internal static class Lines
    {
        public const string Crlf = "\r\n";
        public const string Cr = "\r";
        public const string Lf = "\n";

        public const string Default = Lf;

        public static readonly string[] All = [Crlf, Cr, Lf];
    }
}
