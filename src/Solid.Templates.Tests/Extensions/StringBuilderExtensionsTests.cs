using System.Text;
using Solid.Templates.Extensions;

namespace Solid.Templates.Tests.Extensions;

public class StringBuilderExtensionsTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("123", "123")]
    [InlineData("a\n\rb", "a\n\rb")]
    public void Append_ReadOnlySpan(string input, string output)
    {
        var builder = new StringBuilder();

        StringBuilderExtensions.Append(builder, input.AsSpan());

        Assert.Equal(output, builder.ToString());
    }
}
