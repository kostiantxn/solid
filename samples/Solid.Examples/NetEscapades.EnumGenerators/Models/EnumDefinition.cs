namespace Solid.Examples.NetEscapades.EnumGenerators.Models;

internal record struct EnumDefinition(
    EnumOptions Options,
    string? Namespace,
    string Name,
    List<EnumMember> Members,
    string Accessibility,
    bool Flags = false)
{
    public string FullyQualifiedName =>
        field ??= Namespace is not null ? $"{Namespace}.{Name}" : Name;

    public string GlobalFullyQualifiedName =>
        field ??= "global::" + FullyQualifiedName;

    public string SerializationTransform =>
        field ??= Options.HasRuntimeDependencies
            ? "global::NetEscapades.EnumGenerators.SerializationTransform"
            : GlobalFullyQualifiedName + ".SerializationTransform";

    public string SerializationOptions =>
        field ??= Options.HasRuntimeDependencies
            ? "global::NetEscapades.EnumGenerators.SerializationOptions"
            : GlobalFullyQualifiedName + ".SerializationOptions";
}