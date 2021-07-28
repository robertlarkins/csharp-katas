using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Larkins.CSharpKatas
{
    /// <summary>
    /// Perm. Written based on https://en.wikipedia.org/wiki/Heap%27s_algorithm.
    /// </summary>
    /// <typeparam name="T">The type held in the array.</typeparam>
    public class PermutationIterator<T> : IEnumerable<ReadOnlyCollection<T>>
    {
        private readonly bool isNewArrayGenerated;
        private readonly T[] currentArray;
        private readonly int n;
        private readonly int[] c;
        private int i = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermutationIterator{T}"/> class.
        /// </summary>
        /// <param name="array">The array to permutate.</param>
        /// <param name="isNewArrayGenerated">if set to <c>true</c> generate new array on each iteration.</param>
        public PermutationIterator(IEnumerable<T> array, bool isNewArrayGenerated)
        {
            currentArray = (T[])array.ToArray().Clone();
            n = currentArray.Length;
            c = new int[n];

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

            while (i < n)
            {
                if (c[i] < i)
                {
                    var isEven = i % 2 == 0;
                    var j = isEven ? 0 : c[i];

                    Swap(i, j);

                    yield return GetPermutation();

                    c[i]++;
                    i = 1;
                }
                else
                {
                    c[i] = 0;
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
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Swap(int index1, int index2)
        {
            var temp = currentArray[index1];
            currentArray[index1] = currentArray[index2];
            currentArray[index2] = temp;
        }
    }
}
