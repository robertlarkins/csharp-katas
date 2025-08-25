using Larkins.CSharpKatas.OneCharacterDifference;

namespace Larkins.CSharpKatas.Tests.Unit.OneCharacterDifference;

public class StringSimilarityCalculatorBehaviours
{
    [Theory]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("bb", "bb")]
    public void Two_strings_that_are_the_same_are_not_one_edit_away(
        string string1,
        string string2)
    {
        var result = StringSimilarityCalculator.IsAtMostOneCharacterDifference(string1, string2);

        result.Should().BeFalse(because: "the strings are the same");
    }

    [Theory]
    [InlineData(" ", "", "single white space difference")]
    [InlineData("aa", "a", "single extra character")]
    [InlineData("abc", "ab", "remove character at end")]
    [InlineData("abc", "ac", "remove character in center")]
    [InlineData("abc", "bc", "remove character at start")]
    public void A_string_is_one_edit_away_from_another_string_if_deleting_a_character_makes_them_the_same(
        string string1,
        string string2,
        string reasonForTestCase)
    {
        var result = StringSimilarityCalculator.IsAtMostOneCharacterDifference(string1, string2);

        result.Should().BeTrue(because: reasonForTestCase);
    }

    [Theory]
    [InlineData("", " ", "single white space difference")]
    [InlineData("a", "aa", "single extra character")]
    [InlineData("ab", "abc", "insert character at end")]
    [InlineData("ac", "abc", "insert character in center")]
    [InlineData("bc", "abc", "insert character at start")]
    public void A_string_is_one_edit_away_from_another_string_if_inserting_a_character_makes_them_the_same(
        string string1,
        string string2,
        string reasonForTestCase)
    {
        var result = StringSimilarityCalculator.IsAtMostOneCharacterDifference(string1, string2);

        result.Should().BeTrue(because: reasonForTestCase);
    }

    [Theory]
    [InlineData("a", " ", "single character difference")]
    [InlineData("abc", "adc", "update center character")]
    [InlineData("abc", "dbc", "update start character")]
    [InlineData("abc", "abd", "update end character")]
    public void A_string_is_one_edit_away_from_another_string_if_updating_a_character_makes_them_the_same(
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
    public void Two_strings_are_not_one_edit_away_if_they_differ_by_at_least_two_characters(
        string string1,
        string string2,
        string reasonForTestCase)
    {
        var result = StringSimilarityCalculator.IsAtMostOneCharacterDifference(string1, string2);

        result.Should().BeFalse(because: reasonForTestCase);
    }
}
