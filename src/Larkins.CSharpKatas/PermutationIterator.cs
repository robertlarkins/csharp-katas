﻿using System.Collections;
using System.Collections.ObjectModel;

namespace Larkins.CSharpKatas;

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
    /// <param name="isNewArrayGenerated">
    /// If set to <c>true</c> generate new array on each iteration.
    /// This allows the returned array to be modified without changing the internally stored array.
    /// </param>
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
            if (stackState[i] >= i)
            {
                stackState[i] = 0;
                i++;
                continue;
            }

            var isEven = i % 2 == 0;
            var j = isEven ? 0 : stackState[i];

            Swap(i, j);

            yield return GetPermutation();

            stackState[i]++;
            i = 1;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private ReadOnlyCollection<T> GetPermutation()
    {
        var a = isNewArrayGenerated ? (T[])currentArray.Clone() : currentArray;

        return Array.AsReadOnly(a);
    }

    private void Swap(int index1, int index2)
    {
        (currentArray[index1], currentArray[index2]) = (currentArray[index2], currentArray[index1]);
    }
}
