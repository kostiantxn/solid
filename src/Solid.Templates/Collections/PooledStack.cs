using System.Buffers;

namespace Solid.Templates.Collections;

/// <summary>
///     A stack implementation that uses the provided pool to rent backing arrays.
/// </summary>
internal struct PooledStack<T>(ArrayPool<T> pool)
{
    private T[] _array = [];
    private int _length;

    /// <summary>
    ///     Whether the stack is empty.
    /// </summary>
    public bool Empty => _length is 0;

    /// <summary>
    ///     Returns the item at the top of the stack.
    /// </summary>
    public bool Peek(out T? top)
    {
        if (_length > 0)
        {
            top = _array[_length - 1];
            return true;
        }
        else
        {
            top = default;
            return false;
        }
    }

    /// <summary>
    ///     Adds an item to the top of the stack.
    /// </summary>
    public void Push(T item)
    {
        if (_length >= _array.Length)
        {
            if (_array.Length > 0)
            {
                var rented = pool.Rent(_length * Constants.Multiplier);

                Array.Copy(_array, 0, rented, 0, _array.Length);

                pool.Return(_array);

                _array = rented;
            }
            else
                _array = pool.Rent(Constants.Capacity);
        }

        _array[_length++] = item;
    }

    /// <summary>
    ///     Removes an item from the top of the stack.
    /// </summary>
    public bool Pop(out T? top)
    {
        if (_length > 0)
        {
            top = _array[--_length];
            return true;
        }
        else
        {
            top = default;
            return false;
        }
    }

    /// <summary>
    ///     Clears the stack and returns the rented array to the pool.
    /// </summary>
    public void Return()
    {
        if (_array.Length > 0)
        {
            pool.Return(_array);

            _array = [];
            _length = 0;
        }
    }

    private static class Constants
    {
        public const int Capacity = 8;
        public const int Multiplier = 2;
    }
}