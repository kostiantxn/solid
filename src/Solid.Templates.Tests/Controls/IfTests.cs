using Solid.Templates.Tests.Infrastructure.Extensions;
using static Solid.Templates.Controls.Fluent;

namespace Solid.Templates.Tests.Controls;

// ReSharper disable RedundantStringInterpolation
// ReSharper disable RawStringCanBeSimplified
public class IfTests
{
    [Fact]
    public void False()
    {
        Code.Append(
            $"""
            {If(false)}
            whatever
            {End}
            """)
            .Empty();
    }

    [Fact]
    public void False_LeadingWhitespace()
    {
        Code.Append(
            $"""
            
              {If(false)}
            whatever
            {End}
            """)
            .Produces(
            $"""
            
            """);
    }

    [Fact]
    public void False_TrailingWhitespace()
    {
        Code.Append(
            $"""
            {If(false)}
            whatever
            {End}
            
            """)
            .Produces(
            $"""
            
            """);
    }

    [Fact]
    public void False_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
              {If(false)}
            whatever
            {End}
            
            """)
            .Produces(
            $"""
            
            
            """);
    }

    [Fact]
    public void False_LeadingAndTrailingWhitespace_AndContent()
    {
        Code.Append(
            $"""
             leading 
            
            {If(false)}
            whatever
            {End}
            
             trailing 
            """)
            .Produces(
            $"""
             leading 
            
            
             trailing 
            """);
    }

    [Fact]
    public void False_NestedFalse()
    {
        Code.Append(
            $"""
            {If(false)}
            {If(false)}
            whatever
            {End}
            {End}
            """)
            .Empty();
    }

    [Fact]
    public void False_NestedFalse_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
             {If(false)}
            
              {If(false)}
            
            whatever
            
             {End}
            
              {End}
            
            """)
            .Produces(
            $"""
            
            
            """);
    }

    [Fact]
    public void False_NestedTrue()
    {
        Code.Append(
            $"""
            {If(false)}
            {If(true)}
            whatever
            {End}
            {End}
            """)
            .Empty();
    }

    [Fact]
    public void False_NestedTrue_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
             {If(false)}
            
              {If(true)}
            
            whatever
            
             {End}
            
              {End}
            
            """)
            .Produces(
            $"""
            
            
            """);
    }

    [Fact]
    public void False_SubsequentFalse()
    {
        Code.Append(
            $"""
            {If(false)}
            first
            {End}
            {If(false)}
            second
            {End}
            """)
            .Empty();
    }

    [Fact]
    public void False_SubsequentFalse_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
             {If(false)}
            
            first
            
             {End}
            
              {If(false)}
            second
              {End}
            
            """)
            .Produces(
            $"""
            
            
            
            """);
    }

    [Fact]
    public void False_SubsequentTrue()
    {
        Code.Append(
            $"""
            {If(false)}
            first
            {End}
            {If(true)}
            second
            {End}
            """)
            .Produces(
            $"""
            second
            """);
    }

    [Fact]
    public void False_SubsequentTrue_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
             {If(false)}
            
            first
            
             {End}
            
              {If(true)}
            
             second
            
              {End}
            
            """)
            .Produces(
            $"""
            
            
            
             second
            
            
            """);
    }

    [Fact]
    public void True()
    {
        Code.Append(
            $"""
            {If(true)}
              whatever  
            {End}
            """)
            .Produces(
            $"""
              whatever  
            """);
    }

    [Fact]
    public void True_LeadingWhitespace()
    {
        Code.Append(
            $"""
            
              {If(true)}
              whatever  
            {End}
            """)
            .Produces(
            $"""
            
              whatever  
            """);
    }

    [Fact]
    public void True_TrailingWhitespace()
    {
        Code.Append(
            $"""
            {If(true)}
              whatever  
              {End}
            
            """)
            .Produces(
            $"""
              whatever  
            
            """);
    }

    [Fact]
    public void True_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
              {If(true)}
              whatever  
              {End}
            
            """)
            .Produces(
            $"""
            
              whatever  
            
            """);
    }

    [Fact]
    public void True_LeadingAndTrailingWhitespace_AndContent()
    {
        Code.Append(
            $"""
             leading 
            
              {If(true)}
              whatever  
              {End}
            
             trailing 
            """)
            .Produces(
            $"""
             leading 
            
              whatever  
            
             trailing 
            """);
    }

    [Fact]
    public void True_NestedTrue()
    {
        Code.Append(
            $"""
            {If(true)}
            {If(true)}
            whatever
            {End}
            {End}
            """)
            .Produces(
            $"""
            whatever
            """);
    }

    [Fact]
    public void True_NestedTrue_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
            {If(true)}
            
              {If(true)}
            
             whatever 
            
            {End}
            
             {End}
            
            """)
            .Produces(
            $"""
            
            
            
             whatever 
            
            
            
            """);
    }

    [Fact]
    public void True_NestedFalse()
    {
        Code.Append(
            $"""
            {If(true)}
            {If(false)}
            whatever
            {End}
            {End}
            """)
            .Empty();
    }

    [Fact]
    public void True_NestedFalse_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
            {If(true)}
            
              {If(false)}
            
             whatever 
            
            {End}
            
             {End}
            
            """)
            .Produces(
            $"""
            
            
            
            
            """);
    }

    [Fact]
    public void True_SubsequentTrue()
    {
        Code.Append(
            $"""
            {If(true)}
            first
            {End}
            {If(true)}
            second
            {End}
            """)
            .Produces(
            $"""
            first
            second
            """);
    }

    [Fact]
    public void True_SubsequentTrue_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
            {If(true)}
            
             first 
            
             {End}
            
              {If(true)}
            
            second
            
            {End}
            
            """)
            .Produces(
            $"""
            
            
             first 
            
            
            
            second
            
            
            """);
    }

    [Fact]
    public void True_SubsequentFalse()
    {
        Code.Append(
            $"""
            {If(true)}
            first
            {End}
            {If(false)}
            second
            {End}
            """)
            .Produces(
            $"""
            first
            """);
    }

    [Fact]
    public void True_SubsequentFalse_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
            
            {If(true)}
              
             first 
            
             {End}
            
              {If(false)}
            
            second
            
            {End}
            
            """)
            .Produces(
            $"""
            
              
             first 
            
            
            
            """);
    }

    [Fact]
    public void Inline_False()
    {
        Code.Append(
            $"""
            a{If(false)}b{End}c
            """)
            .Produces(
            $"""
            ac
            """);
    }

    [Fact]
    public void Inline_False_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
             a  {If(false)}   b    {End}     c 
            """)
            .Produces(
            $"""
             a       c 
            """);
    }

    [Fact]
    public void Inline_False_LeadingAndTrailingWhitespace_Indented()
    {
        Code.Indented(by: 2).Append(
            $"""
             a  {If(false)}   b    {End}     c 
            """)
            .Produces(
            $"""
               a       c 
            """);
    }

    [Fact]
    public void Inline_True()
    {
        Code.Append(
            $"""
            a{If(true)}b{End}c
            """)
            .Produces(
            $"""
            abc
            """);
    }

    [Fact]
    public void Inline_True_LeadingAndTrailingWhitespace()
    {
        Code.Append(
            $"""
             a  {If(true)}   b    {End}     c 
            """)
            .Produces(
            $"""
             a     b         c 
            """);
    }

    [Fact]
    public void Inline_True_LeadingAndTrailingWhitespace_Indented()
    {
        Code.Indented(by: 1).Append(
            $"""
             a  {If(true)}   b    {End}     c 
            """)
            .Produces(
            $"""
              a     b         c 
            """);
    }

    [Fact]
    public void Complex()
    {
        Code.Indented(by: 2).Append(
            $"""
            hello
            
            {If(true)}
                {If(true)}
                    {If(true)}
                        deep
                    {End}
                {End}
                {If(false)}
                    nope
                {End}
                
                {If(true)} and {End} {If(true)}  another  {End} 
            {End}
            {If(false)}
                
                {If(true)}
                    {If(false)}
                    {End}
                {End}
                
            {End}
            
            a {If(true)}
            b
            {End} c
            
            bye 
            {If(true)} {End} {If(true)}xoxo{End}
            """)
            .Produces(
            $"""
              hello
              
                          deep
                  
               and    another   
              
              a b c
              
              bye 
                xoxo
            """);
    }

    [Fact]
    public void All()
    {
        Code.Indented(by: 2).Append(
            $"""
            {If(true)}
            a
            {End}
            """)
            .Produces(
            $"""
              a
            """);

        Code.Indented(by: 2).Append(
            $"""
            a {If(true)}
            b
            {End}
            """)
            .Produces(
            $"""
              a b
            """);

        Code.Indented(by: 2).Append(
            $"""
            a {If(true)}
            b{End} c
            """)
            .Produces(
            $"""
              a b c
            """);

        Code.Indented(by: 2).Append(
            $"""
            a {If(true)}
            b
            {End} c
            """)
            .Produces(
            $"""
              a b c
            """);

        Code.Indented(by: 2).Append(
            $"""
            a {If(true)}b{End} c
            """)
            .Produces(
            $"""
              a b c
            """);

        Code.Indented(by: 2).Append(
            $"""
            a {If(true)}b{End} c
            """)
            .Produces(
            $"""
              a b c
            """);

        Code.Indented(by: 2).Append(
            $"""
            a
            {If(true)}
            b
            {End}
            c
            """)
            .Produces(
            $"""
              a
              b
              c
            """);

        Code.Indented(by: 2).Append(
            $"""
            a
            {If(true)}
            b
            {End}
            c
            """)
            .Produces(
            $"""
              a
              b
              c
            """);
    }

    [Fact]
    public void Error_NoEnd()
    {
        Assert.Throws<InvalidOperationException>(() =>
            Code.Append(
                $"""
                {If(true)}
                """));
    }

    [Fact]
    public void Error_NoIf()
    {
        Assert.Throws<InvalidOperationException>(() =>
            Code.Append(
                $"""
                {End}
                """));
    }

    [Fact]
    public void NestedError_NoEnd()
    {
        Assert.Throws<InvalidOperationException>(() =>
            Code.Append(
                $"""
                {If(true)}
                {If(false)}
                {End}
                """));
    }

    [Fact]
    public void NestedError_NoIf()
    {
        Assert.Throws<InvalidOperationException>(() =>
            Code.Append(
                $"""
                {If(false)}
                {End}
                {End}
                """));
    }

    private static Code Code =>
        new();
}
