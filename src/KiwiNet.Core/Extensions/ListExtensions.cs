using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace KiwiNet.Core.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Adds the provided <typeparamref name="T"/> value the specified number of times.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill<T>(this List<T> list, T value, int count)
        {
            Debug.Assert(count >= 0);

            list.EnsureCapacity(list.Count + count);

            for (int i = 0; i < count; i++)
                list.Add(value);
        }

        /// <summary>
        /// Swaps the element at the specified index with the last one in the list and removes.
        /// </summary>
        /// <remarks>
        /// This is done to avoid elements being moved when removing an element in the middle of the list.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapRemove<T>(this List<T> list, int index)
        {
            Debug.Assert(index >= 0 && index < list.Count);

            int lastIndex = list.Count - 1;

            if (index != lastIndex)
                list[index] = list[lastIndex];

            list.RemoveAt(lastIndex);
        }
    }
}
