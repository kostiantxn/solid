using Solid.Examples.System.Text.Json.Models;

namespace Solid.Examples.System.Text.Json;

internal static class ContextGeneratorExample
{
    public static void Run()
    {
        var code = ContextGenerator.Generate(
            new ContextDefinition(
                new ContextOptions(
                    IsPrimarySourceFile: true),
                "Some.Namespace",
                ["First", "Second"],
                "SomeContext",
                "ISomeContext"));

        Console.WriteLine(code); 
    }
}