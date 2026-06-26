using Solid.Templates.Tests.Infrastructure.Extensions;

namespace Solid.Templates.Tests;

public class CodeTests
{
    [Fact]
    public void Empty() =>
        Code().Empty();

    [Theory]
    [InlineData(0, null, "")]
    [InlineData(2, null, "")]
    [InlineData(0, "ab", "ab")]
    [InlineData(2, "ab", "  ab")]
    [InlineData(0, 1234, "1234")]
    [InlineData(2, 1234, "  1234")]
    public void Append_Object(int indent, object? input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0, null, null, "")]
    [InlineData(2, null, null, "")]
    [InlineData(0, 1234, null, "1234")]
    [InlineData(2, 1234, null, "  1234")]
    [InlineData(0, 1234, "F1", "1234.0")]
    [InlineData(2, 1234, "F2", "  1234.00")]
    public void Append_Formattable(int indent, IFormattable? input, string? format, string output) =>
        Code(indent).Append(input, format).Produces(output);

    [Theory]
    [InlineData(0, "", "")]
    [InlineData(2, "", "")]
    [InlineData(0, " ", " ")]
    [InlineData(2, " ", "   ")]
    [InlineData(0, "ab", "ab")]
    [InlineData(2, "ab", "  ab")]
    [InlineData(0, "\r", "\n")]
    [InlineData(2, "\n", "  \n  ")]
    [InlineData(0, "abc\n\r123\nxyz\r\n", "abc\n\n123\nxyz\n")]
    [InlineData(2, "abc\n\r123\nxyz\r\n", "  abc\n  \n  123\n  xyz\n  ")]
    public void Append_String(int indent, string input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0, 'a', "a")]
    [InlineData(2, 'a', "  a")]
    [InlineData(0, '\r', "\n")]
    [InlineData(0, '\n', "\n")]
    [InlineData(2, '\r', "  \n")]
    [InlineData(2, '\n', "  \n")]
    public void Append_Char(int indent, char input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0, 'a', 0, "")]
    [InlineData(0, 'a', 3, "aaa")]
    [InlineData(2, 'a', 0, "")]
    [InlineData(2, 'a', 3, "  aaa")]
    [InlineData(0, '\r', 0, "")]
    [InlineData(0, '\n', 0, "")]
    [InlineData(0, '\r', 2, "\n\n")]
    [InlineData(0, '\n', 2, "\n\n")]
    [InlineData(2, '\r', 0, "")]
    [InlineData(2, '\n', 0, "")]
    [InlineData(2, '\r', 2, "  \n  \n")]
    [InlineData(2, '\n', 2, "  \n  \n")]
    public void Append_Char_Count(int indent, char input, int count, string output) =>
        Code(indent).Append(input, count).Produces(output);

    [Theory]
    [InlineData(0, true, "True")]
    [InlineData(0, false, "False")]
    [InlineData(2, true, "  True")]
    [InlineData(2, false, "  False")]
    public void Append_Bool(int indent, bool input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0, 123, "123")]
    [InlineData(2, 123, "  123")]
    public void Append_Byte(int indent, byte input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123, "123")]
    [InlineData(0, -123, "-123")]
    [InlineData(2,  123, "  123")]
    [InlineData(2, -123, "  -123")]
    public void Append_SByte(int indent, sbyte input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123, "123")]
    [InlineData(0, -123, "-123")]
    [InlineData(2,  123, "  123")]
    [InlineData(2, -123, "  -123")]
    public void Append_Short(int indent, short input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123, "123")]
    [InlineData(2,  123, "  123")]
    public void Append_UShort(int indent, ushort input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123, "123")]
    [InlineData(0, -123, "-123")]
    [InlineData(2,  123, "  123")]
    [InlineData(2, -123, "  -123")]
    public void Append_Int(int indent, int input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123, "123")]
    [InlineData(2,  123, "  123")]
    public void Append_UInt(int indent, uint input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123, "123")]
    [InlineData(0, -123, "-123")]
    [InlineData(2,  123, "  123")]
    [InlineData(2, -123, "  -123")]
    public void Append_Long(int indent, long input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123, "123")]
    [InlineData(2,  123, "  123")]
    public void Append_ULong(int indent, ulong input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123.4, "123.4")]
    [InlineData(0, -123.4, "-123.4")]
    [InlineData(2,  123.4, "  123.4")]
    [InlineData(2, -123.4, "  -123.4")]
    public void Append_Float(int indent, float input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123.4, "123.4")]
    [InlineData(0, -123.4, "-123.4")]
    [InlineData(2,  123.4, "  123.4")]
    [InlineData(2, -123.4, "  -123.4")]
    public void Append_Double(int indent, double input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0,  123.4, "123.4")]
    [InlineData(0, -123.4, "-123.4")]
    [InlineData(2,  123.4, "  123.4")]
    [InlineData(2, -123.4, "  -123.4")]
    public void Append_Decimal(int indent, decimal input, string output) =>
        Code(indent).Append(input).Produces(output);

    [Theory]
    [InlineData(0, "x", "xx")]
    [InlineData(2, "x", "  xx")]
    [InlineData(0, 123, "123123")]
    [InlineData(2, 123, "  123123")]
    public void Append_Action(int indent, object? input, string output) =>
        Code(indent).Append(x => x.Append(input).Append(input)).Produces(output);

    [Theory]
    [InlineData(0, null, "")]
    [InlineData(0, 1234, "1234")]
    [InlineData(2, null, "")]
    [InlineData(2, "  ", "    ")]
    [InlineData(2, 12.3, "  12.3")]
    public void Append_Interpolation(int indent, object? input, string output) =>
        Code(indent).Append($"{input}").Produces(output);

    [Theory]
    [InlineData(0, 1, "\n")]
    [InlineData(0, 3, "\n\n\n")]
    [InlineData(2, 1, "  \n")]
    [InlineData(2, 3, "  \n  \n  \n")]
    public void Line(int indent, int lines, string output)
    {
        var code = Code(indent);

        while (--lines >= 0)
            code.Line();

        code.Produces(output);
    }

    [Theory]
    [InlineData(0,  0,  0)]
    [InlineData(0, +1, +1)]
    [InlineData(0, -1, -1)]
    [InlineData(3, +1, +4)]
    [InlineData(3, -1, +2)]
    public void Indented(int initial, int delta, int expected) =>
        Assert.Equal(expected, Code(initial).Indented(by: delta).Indent.Level);

    private static Code Code(int indent = 0) =>
        new() { Indent = { Level = indent } };
}
