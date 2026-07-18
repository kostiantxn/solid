namespace Solid.Examples.System.Text.Json.Models;

internal record struct ContextDefinition(
    ContextOptions Options,
    string? Namespace,
    List<string>? Containers,
    string Name,
    string? Interface)
{
    
}