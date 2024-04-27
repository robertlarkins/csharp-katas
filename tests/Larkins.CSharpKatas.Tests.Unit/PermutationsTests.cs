using System.Collections.ObjectModel;
using System.Linq;
using Larkins.CSharpKatas.Permutations;

namespace Larkins.CSharpKatas.Tests.Unit;

public class PermutationsTests
{
    [Fact]
    public void All_permutations_generated()
    {
        // Arrange
        var sut = new PermutationIterator<int>(new[] { 1, 2, 3 }, true);

        var expected = new List<int[]>
        {
            new[] { 1, 2, 3 },
            new[] { 1, 3, 2 },
            new[] { 2, 1, 3 },
            new[] { 2, 3, 1 },
            new[] { 3, 1, 2 },
            new[] { 3, 2, 1 }
        };

        // Act
        var result = sut.ToList();

        // Assert
        // This FluentAssertion is based on:
        // https://stackoverflow.com/questions/68527627/fluent-assertions-equivalency-of-two-lists-of-arrays-with-arrays-needing-stric
        // Note: the order of expected is not necessarily the order of the results.
        // The strict ordering is for the values in each array.
        result.Should().BeEquivalentTo(expected, options => options
            .WithStrictOrderingFor(info => info.RuntimeType == typeof(int[])));
    }

    [Fact]
    public void A_single_item_only_has_one_permutation()
    {
        // Arrange
        var sut = new PermutationIterator<int>(new[] { 1 }, true);

        var expected = new List<int[]>
        {
            new[] { 1 }
        };

        // Act
        var result = sut.ToList();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void All_permutations_are_generated_when_all_elements_are_the_same()
    {
        var sut = new PermutationIterator<int>(new[] { 5, 5, 5 }, true);

        var result = sut.ToList();

        result.Should()
            .HaveCount(6).And
            .AllBeEquivalentTo(new[] { 5, 5, 5 });
    }

    [Fact]
    public void Enumerator_test()
    {
        // Arrange
        var sut = new PermutationIterator<int>(new[] { 1, 2, 3, 4 }, true);

        // Act
        var enumerator = sut.GetEnumerator();
    }

    [Fact]
    public void Enumerator_test_2()
    {
        // Arrange
        var sut = new PermutationEnumerator<int>(new[] { 1, 2, 3 }, true);

        var expected = new List<int[]>
        {
            new[] { 1, 2, 3 },
            new[] { 1, 3, 2 },
            new[] { 2, 1, 3 },
            new[] { 2, 3, 1 },
            new[] { 3, 1, 2 },
            new[] { 3, 2, 1 }
        };

        // Act
        var result = new List<ReadOnlyCollection<int>>();

        while (sut.MoveNext())
        {
            result.Add(sut.Current);
        }

        // Assert
        result.Should().BeEquivalentTo(expected, options => options
            .WithStrictOrderingFor(info => info.RuntimeType == typeof(int[])));
    }

    [Fact]
    public void Enumerator_Current_is_empty_before_the_enumerator_has_started()
    {
        // Arrange
        var sut = new PermutationEnumerator<int>(new[] { 1, 2, 3 }, true);

        // Act
        var result = sut.Current;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Enumerator_Current_is_empty_after_the_enumerator_has_completed()
    {
        // Arrange
        var sut = new PermutationEnumerator<int>(new[] { 1, 2, 3 }, true);

        // Act
        while (sut.MoveNext())
        {
        }

        var result = sut.Current;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Enumerator_Reset_allows_enumeration_to_begin_again()
    {
        // Arrange
        var sut = new PermutationEnumerator<int>(new[] { 1, 2, 3 }, true);

        var expected = new List<int[]>
        {
            new[] { 1, 2, 3 },
            new[] { 1, 3, 2 },
            new[] { 2, 1, 3 },
            new[] { 2, 3, 1 },
            new[] { 3, 1, 2 },
            new[] { 3, 2, 1 }
        };

        // Act
        sut.MoveNext();
        sut.Reset();

        var result = new List<ReadOnlyCollection<int>>();

        while (sut.MoveNext())
        {
            result.Add(sut.Current);
        }

        // Assert
        result.Should().BeEquivalentTo(expected, options => options
            .WithStrictOrderingFor(info => info.RuntimeType == typeof(int[])));
    }
}
