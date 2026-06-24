// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

#pragma warning disable CS9113

[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class InterpolatedStringHandlerArgumentAttribute(params string[] arguments) : Attribute;
