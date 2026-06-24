using System.Runtime.CompilerServices;

namespace Solid.Templates;

/// <summary>
///     Indentation that is added to the code at the beginning of each line.
/// </summary>
public class Indent(Code code)
{
    /// <summary>
    ///     The current indentation level which defines how many indentation characters to append.
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    ///     The character to use when appending indentation.
    /// </summary>
    public char Character { get; set; } = ' ';

    /// <summary>
    ///     Calculates the current indentation of the last line of code.
    /// </summary>
    public int Current()
    {
        var index = code.Builder.Length - 1;

        while (index >= 0 && code.Builder[index] == Character)
            index--;

        return code.Builder.Length - 1 - index;
    }

    /// <summary>
    ///     Keeps the current indentation of the last line of code.
    /// </summary>
    /// <seealso cref="Current"/>
    public void Keep(out int previous)
    {
        previous = Level;

        Level = Current();
    }

    /// <summary>
    ///     Shifts the current indentation level by the specified delta.
    /// </summary>
    public void Shift(int delta, out int previous)
    {
        previous = Level;

        Level += delta;
    }

    /// <summary>
    ///     Appends indentation to the code.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append()
    {
        if (Level > 0)
            code.Builder.Append(Character, Level);
    }

    /// <summary>
    ///     Appends indentation to the code if the provided flag is <c>true</c>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ref bool when)
    {
        if (when)
        {
            when = false;
            Append();
        }
    }
}
