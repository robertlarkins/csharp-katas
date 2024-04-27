using System.Collections;
using System.Collections.ObjectModel;
using Larkins.CSharpKatas.Extensions;

namespace Larkins.CSharpKatas.Permutations;

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
        var totalElements = currentArray.Length;
        var stackState = new int[totalElements];

        while (true)
        {
            yield return GetPermutation();

            var index1 = CalculateIndex1();

            if (index1 >= totalElements)
            {
                yield break;
            }

            var index2 = CalculateIndex2(index1);

            currentArray.SwapElements(index1, index2);
        }

        int CalculateIndex1()
        {
            var index1 = 1;

            while (index1 < totalElements && stackState[index1] >= index1)
            {
                stackState[index1] = 0;
                index1++;
            }

            return index1;
        }

        int CalculateIndex2(int index1)
        {
            var index2 = index1.IsEven() ? 0 : stackState[index1];
            stackState[index1]++;

            return index2;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private ReadOnlyCollection<T> GetPermutation()
    {
        var a = isNewArrayGenerated ? (T[])currentArray.Clone() : currentArray;

        return Array.AsReadOnly(a);
    }
}
