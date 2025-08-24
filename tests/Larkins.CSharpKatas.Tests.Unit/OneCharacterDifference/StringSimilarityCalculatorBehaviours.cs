using Larkins.CSharpKatas.OneCharacterDifference;

namespace Larkins.CSharpKatas.Tests.Unit.OneCharacterDifference;

public class StringSimilarityCalculatorBehaviours
{
    [Theory]
    [InlineData("", "", "both are empty strings")]
    [InlineData("", " ", "differ in length by single character")]
    [InlineData("a", "a", "are the same character")]
    [InlineData("a", "aa", "differ in length by single character")]
    [InlineData("a", "b", "single character difference")]
    [InlineData("ab", "abc", "single character difference")]
    [InlineData("bc", "abc", "single character difference")]
    [InlineData("abc", "bc", "single character difference")]
    [InlineData("abc", "ab", "single character difference")]
    [InlineData("ac", "abc", "single non-end character difference")]
    [InlineData("abc", "ac", "single non-end character difference")]
    [InlineData("aabb", "abb", "only first character is different")]
    [InlineData("abcd", "bcd", "only first character is different")]
    public void Two_strings_are_considered_the_same_if_they_differ_by_at_most_one_character(
        string string1,
        string string2,
        string reasonForTestCase)
    {
        var result = StringSimilarityCalculator.IsAtMostOneCharacterDifference(string1, string2);

        result.Should().BeTrue(because: reasonForTestCase);
    }

    [Theory]
    [InlineData("", "  ", "length differs by two characters")]
    [InlineData("  ", "", "length differs by two characters")]
    [InlineData("aa", "bb", "two characters are different")]
    [InlineData("a", "bb", "two characters are different")]
    [InlineData("aa", "abb", "length differs by one character, but two characters are different")]
    [InlineData("bba", "ab", "length differs by one character, but two characters are different")]
    public void Two_strings_are_considered_different_if_they_differ_by_at_least_two_characters(
        string string1,
        string string2,
        string reasonForTestCase)
    {
        var result = StringSimilarityCalculator.IsAtMostOneCharacterDifference(string1, string2);

        result.Should().BeFalse(because: reasonForTestCase);
    }
}
