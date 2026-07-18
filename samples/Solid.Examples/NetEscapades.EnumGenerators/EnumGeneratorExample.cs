using Solid.Examples.NetEscapades.EnumGenerators.Models;

namespace Solid.Examples.NetEscapades.EnumGenerators;

internal static class EnumGeneratorExample
{
    public static void Run()
    {
        var code = EnumGenerator.Generate(
            new EnumDefinition(
                new EnumOptions(
                    HasRuntimeDependencies: false,
                    HasOverloadResolutionPriority: true,
                    HasMetadataNames: true),
                "Some.Namespace",
                "Colour",
                [
                    new EnumMember("Red", "Red", "1"),
                    new EnumMember("Green", "Green", "2"),
                    new EnumMember("Blue", "Blue", "3"),
                ],
                "public"));

        Console.WriteLine(code);
    }
}