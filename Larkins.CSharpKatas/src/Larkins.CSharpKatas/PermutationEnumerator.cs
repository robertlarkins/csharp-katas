using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Larkins.CSharpKatas
{
    /// <summary>
    /// Permutation Enumerator.
    /// </summary>
    /// <typeparam name="T">The array type.</typeparam>
    public class PermutationEnumerator<T> : IEnumerator<ReadOnlyCollection<T>>
    {
        private readonly bool isNewArrayGenerated;
        private readonly T[] originalArray;
        private readonly int arrayLength;

        private T[] currentArray;
        private int[] stackState;
        private int i;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermutationEnumerator{T}"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="isNewArrayGenerated">
        /// If set to <c>true</c> generate new array on each iteration.
        /// This does a shallow clone of the array, allowing the returned array to be modified
        /// without changing the internally stored array.
        /// </param>
        public PermutationEnumerator(IEnumerable<T> array, bool isNewArrayGenerated)
        {
            this.isNewArrayGenerated = isNewArrayGenerated;
            originalArray = (T[])array.ToArray().Clone();
            currentArray = (T[])originalArray.Clone();

            arrayLength = originalArray.Length;

            stackState = new int[arrayLength];
        }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <remarks>
        /// https://stackoverflow.com/questions/40774231/ienumeratort-current-beyond-movenext-false.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator.current?view=net-5.0#System_Collections_IEnumerator_Current.
        /// </remarks>
        public ReadOnlyCollection<T> Current => GetCurrent();

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        ///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            if (i == 0)
            {
                i = 1;
                return true;
            }

            while (i < arrayLength)
            {
                if (stackState[i] < i)
                {
                    var isEven = i % 2 == 0;
                    var j = isEven ? 0 : stackState[i];

                    Swap(i, j);

                    stackState[i]++;
                    i = 1;

                    return true;
                }

                stackState[i] = 0;
                i++;
            }

            return false;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            currentArray = (T[])originalArray.Clone();
            stackState = new int[arrayLength];
            i = 0;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// Nothing to dispose of for this class.
        /// </summary>
        /// <remarks>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1.getenumerator.
        /// </remarks>
        public void Dispose()
        {
        }

        private void Swap(int index1, int index2)
        {
            (currentArray[index1], currentArray[index2]) = (currentArray[index2], currentArray[index1]);
        }

        private ReadOnlyCollection<T> GetCurrent()
        {
            var a = Array.Empty<T>();

            if (i > 0 && i < arrayLength)
            {
                a = isNewArrayGenerated ? (T[])currentArray.Clone() : currentArray;
            }

            return Array.AsReadOnly(a);
        }
    }
}
