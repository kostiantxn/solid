# SOLID

It's like [Liquid](https://github.com/Shopify/liquid) but in C#.

## Prelude

Source-generating C# files isn't a trivial task, partially due to how inconvenient the standard `StringBuilder` is.
A typical source generated file requires you to chain a tonne of `Append` and `AppendLine` calls, completely obfuscating the real structure of the output file:
consider, for example, the standard [`System.Text.Json`](https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.Json/gen/JsonSourceGenerator.Emitter.cs) context generator or Andrew Lock's wonderful [`NetEscapades.EnumGenerators`](https://github.com/andrewlock/NetEscapades.EnumGenerators/blob/main/src/NetEscapades.EnumGenerators.Generators/SourceGenerationHelper.cs).

SOLID provides an alternative approach to generating source files and building complex strings by utilising a custom [interpolated string handler](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/performance/interpolated-string-handler), which implements indentation controls, conditional `If`/`Else`/`End` blocks, as well as `For` loops.

## Example

Consider the following.
```csharp
code.Append(
    $"""
     {If(ingredients.Length > 0)}
     Recipe:
         {For(ingredients.Select(x => $"- {x}")):\n:}
     {Else}
     No ingredients? 🥺
     {End}
     """);
```

Given a list of `ingredients` that contains `"banana"`, `"oats"`, and `"milk"`, the example above will produce:
```
Recipe:
    - banana
    - oats
    - milk
```
For an empty list of `ingredients`, it will produce:
```
No ingredients? 🥺
```

## Breakdown

### Code

`code` is a variable of type `Code`, the main class in SOLID.
It is a simple wrapper around the standard `StringBuilder`, which handles indentation and interpolation.

### `If`/`Else`/`End`

`If`, `Else`, and `End` are the main control blocks, which are used to handle conditional logic within SOLID interpolation templates, and they behave exactly like what you would expect from a standard `if`/`else` block in C#.

In the example above, the first `If` branch of the conditional will only be appended to the `code` if `ingredients.Length > 0` is `true`; otherwise, the alternative `Else` branch will be appended.
This allows you to control what to append within the interpolated SOLID template itself, instead of branching the logic outside of the template and chaining multiple `Append` calls.

### `For`

`For` allows looping over a collection of items and appending each one of them to the code, with the format parameter `:\n:` indicating the separator character to insert between the items.
The indentation before the `For` loop defines the indentation of every item in the collection.

In the example above, `For` will append every item in the list, separated by `\n`, with indentation level of 4.

> [!NOTE]
> `If`, `Else`, `End`, and `For` are defined in the static `Solid.Templates.Controls.Fluent` class, which you can import via `using static`.

## Licence

The project is licensed under the [MIT](LICENSE) license.