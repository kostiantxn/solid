namespace Solid.Templates.Tests.Infrastructure.Extensions;

internal static class CodeExtensions
{
    extension(Code code)
    {
        public void Empty() =>
            Assert.Empty(code.ToString());

        public void Produces(string expected, string newline = "\n") =>
            Assert.Equal(expected.ReplaceLineEndings(newline), code.ToString());
    }
}
