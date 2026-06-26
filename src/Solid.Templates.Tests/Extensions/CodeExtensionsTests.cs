using Solid.Templates.Extensions;
using Solid.Templates.Tests.Infrastructure.Extensions;

namespace Solid.Templates.Tests.Extensions;

public class CodeExtensionsTests
{
    [Theory]
    [InlineData(0, "", ", ")]
    [InlineData(2, "", ", ")]
    [InlineData(0, "a", ", ", "a")]
    [InlineData(2, "  a", ", ", "a")]
    [InlineData(0, "a, b", ", ", "a", "b")]
    [InlineData(2, "  a, b", ", ", "a", "b")]
    [InlineData(0, "a\nb", "\n", "a", "b")]
    [InlineData(2, "  a\n  b", "\r", "a", "b")]
    public void Join(int indent, string output, string separator, params object[] items) =>
        Code(indent).Join(items, separator).Produces(output);

    private static Code Code(int indent) =>
        new() { Indent = { Level = indent } };
}
