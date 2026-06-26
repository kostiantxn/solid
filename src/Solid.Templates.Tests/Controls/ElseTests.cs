using Solid.Templates.Tests.Infrastructure.Extensions;
using static Solid.Templates.Controls.Fluent;

namespace Solid.Templates.Tests.Controls;

// ReSharper disable RedundantStringInterpolation
// ReSharper disable RawStringCanBeSimplified
public class ElseTests
{
    [Fact]
    public void FalseFalse()
    {
        Code.Append(
            $"""
            {If(false)}
            first
            {Else(false)}
            second
            {End}
            """)
            .Empty();
    }

    [Fact]
    public void FalseTrue()
    {
        Code.Append(
            $"""
            {If(false)}
            first
            {Else(true)}
            second
            {End}
            """)
            .Produces(
            $"""
            second
            """);
    }

    [Fact]
    public void TrueFalse()
    {
        Code.Append(
            $"""
            {If(true)}
            first
            {Else(false)}
            second
            {End}
            """)
            .Produces(
            $"""
            first
            """);
    }

    [Fact]
    public void TrueTrue()
    {
        Code.Append(
            $"""
            {If(true)}
            first
            {Else(true)}
            second
            {End}
            """)
            .Produces(
            $"""
            first
            """);
    }

    [Fact]
    public void Error_NoIf()
    {
        Assert.Throws<InvalidOperationException>(() =>
            Code.Append(
                $"""
                {Else}
                """));
    }

    private static Code Code =>
        new();
}
