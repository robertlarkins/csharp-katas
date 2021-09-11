using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Larkins.CSharpKatas.Tests.Unit
{
    /// <summary>
    /// Tests for permutations.
    /// </summary>
    public class PermutationsTests
    {
        /// <summary>
        /// Are all permutations generated.
        /// </summary>
        /// <remarks>
        /// How to get this to pass? https://dotnetfiddle.net/axwQxt.
        /// Note: the order of expected is not necessarily the order of the results.
        /// </remarks>
        [Fact]
        public void Are_all_permutations_generated()
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
            result.Should().BeEquivalentTo(expected, options => options
                .WithStrictOrderingFor(info => info.RuntimeType == typeof(int[])));
        }

        [Fact]
        public void Are_all_permutations_generated_when_all_elements_are_the_same()
        {
            // Arrange
            var sut = new PermutationIterator<int>(new[] { 5, 5, 5 }, true);

            // Act
            var result = sut.ToList();

            // Assert
            result.Should()
                .HaveCount(6).And
                .AllBeEquivalentTo(new[] { 5, 5, 5 });
        }
    }
}
