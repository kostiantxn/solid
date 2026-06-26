namespace Solid.Examples.NetEscapades.EnumGenerators.Models;

internal record struct EnumOptions(
    bool HasRuntimeDependencies,
    bool HasOverloadResolutionPriority,
    bool HasMetadataNames);
