using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Larkins.CSharpKatas
{
    /// <summary>
    /// Permutation iterator. This generates every permutation of the given array.
    /// As it is an iterator the next permutation is only determined at call time.
    /// This reduces the memory cost especially with large arrays.
    /// </summary>
    /// <remarks>
    /// Written based on https://en.wikipedia.org/wiki/Heap%27s_algorithm.
    /// </remarks>
    /// <typeparam name="T">The type held in the array.</typeparam>
    public class PermutationIterator<T> : IEnumerable<ReadOnlyCollection<T>>
    {
        private readonly bool isNewArrayGenerated;
        private readonly T[] currentArray;
        private readonly int[] stackState;
        private int i = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermutationIterator{T}"/> class.
        /// </summary>
        /// <param name="array">The array to permutate.</param>
        /// <param name="isNewArrayGenerated">if set to <c>true</c> generate new array on each iteration.</param>
        public PermutationIterator(IEnumerable<T> array, bool isNewArrayGenerated)
        {
            currentArray = (T[])array.ToArray().Clone();
            stackState = new int[currentArray.Length];

            this.isNewArrayGenerated = isNewArrayGenerated;
        }

        /// <summary>
        /// The Enumerator.
        /// </summary>
        /// <remarks>
        /// The recommendation is to use ReadOnly for the object being returned,
        /// and not just relying on the return type being cast to IReadOnlyValue
        /// https://stackoverflow.com/questions/45164799/ireadonlycollection-vs-readonlycollection.
        /// </remarks>
        /// <returns>The Enumerator for the permutations.</returns>
        public IEnumerator<ReadOnlyCollection<T>> GetEnumerator()
        {
            yield return GetPermutation();

            while (i < currentArray.Length)
            {
                if (stackState[i] < i)
                {
                    var isEven = i % 2 == 0;
                    var j = isEven ? 0 : stackState[i];

                    Swap(i, j);

                    yield return GetPermutation();

                    stackState[i]++;
                    i = 1;
                }
                else
                {
                    stackState[i] = 0;
                    i++;
                }
            }

            ReadOnlyCollection<T> GetPermutation()
            {
                var a = isNewArrayGenerated ? (T[])currentArray.Clone() : currentArray;

                return Array.AsReadOnly(a);
            }
        }

        /// <summary>
        /// The default enumerator.
        /// </summary>
        /// <returns>Blah.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Swap(int index1, int index2)
        {
            (currentArray[index1], currentArray[index2]) = (currentArray[index2], currentArray[index1]);
        }
    }
}
