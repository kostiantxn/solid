using Solid.Templates.Extensions;

namespace Solid.Templates.Tests.Extensions;

public class ReadOnlyMemoryExtensionsTests
{
    [Fact]
    public void Empty()
    {
        var enumerator = "".AsMemory().Lines();

        Assert.True(enumerator.MoveNext());
        Assert.Equal(enumerator.Current, "");
        Assert.False(enumerator.MoveNext());
    }

    [Theory]
    [InlineData(" ", " ")]
    [InlineData("a", "a")]
    [InlineData("a\r", "a", "")]
    [InlineData("a\n", "a", "")]
    [InlineData("a\r\n", "a", "")]
    [InlineData("a\n\r", "a", "", "")]
    [InlineData("\ra", "", "a")]
    [InlineData("\na", "", "a")]
    [InlineData("\r\na", "", "a")]
    [InlineData("\n\ra", "", "", "a")]
    [InlineData("a\rb", "a", "b")]
    [InlineData("a\nb", "a", "b")]
    [InlineData("a\r\nb", "a", "b")]
    [InlineData("a\n\rb", "a", "", "b")]
    [InlineData(" \ra \n b\r\nc\n\r ", " ", "a ", " b", "c", "", " ")]
    public void Lines(string input, params string[] outputs)
    {
        var enumerator = input.AsMemory().Lines().GetEnumerator();

        foreach (var output in outputs)
        {
            Assert.True(enumerator.MoveNext());
            Assert.Equal(enumerator.Current, output);
        }

        Assert.False(enumerator.MoveNext());
    }
}
