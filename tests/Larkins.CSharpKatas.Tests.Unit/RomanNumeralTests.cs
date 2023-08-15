namespace Larkins.CSharpKatas.Tests.Unit;

/// <summary>
/// Tests for Roman Numeral.
/// </summary>
public class RomanNumeralTests
{
    /// <summary>
    /// Romans the numeral converts to associated number.
    /// </summary>
    /// <param name="romanNumeral">The roman numeral.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData("I", 1)]
    [InlineData("II", 2)]
    [InlineData("III", 3)]
    [InlineData("IV", 4)]
    [InlineData("V", 5)]
    [InlineData("VI", 6)]
    [InlineData("VII", 7)]
    [InlineData("VIII", 8)]
    [InlineData("IX", 9)]
    [InlineData("X", 10)]
    [InlineData("XVI", 16)]
    [InlineData("XXVII", 27)]
    [InlineData("XXXII", 32)]
    [InlineData("LVIII", 58)]
    [InlineData("CLXXXIII", 183)]
    [InlineData("DLV", 555)]
    [InlineData("XLV", 45)]
    [InlineData("L", 50)]
    [InlineData("XCVIII", 98)]
    [InlineData("XCIX", 99)]
    [InlineData("C", 100)]
    [InlineData("CD", 400)]
    [InlineData("D", 500)]
    [InlineData("CMXCIV", 994)]
    [InlineData("CM", 900)]
    [InlineData("CMXCIX", 999)]
    [InlineData("M", 1000)]
    [InlineData("MMMCMXCIX", 3999)]
    public void Roman_numeral_converts_to_associated_number(string romanNumeral, int expected)
    {
        // Arrange
        var sut = RomanNumeral.Create(romanNumeral).Value;

        // Act
        var result = sut.IntValue;

        // Assert
        result.Should().Be(expected);
    }

    /// <summary>
    /// Unknowns the roman numeral is invalid.
    /// </summary>
    /// <param name="romanNumeral">The roman numeral.</param>
    [Theory]
    [InlineData("")]
    [InlineData("ABCD")]
    [InlineData("IIII")]
    [InlineData("VV")]
    [InlineData("VIV")]
    [InlineData("   ")]
    [InlineData("MMMDD")]
    [InlineData("MMMM")]
    [InlineData("IXC")]
    [InlineData("IXCM")]
    [InlineData("XCM")]
    public void Unknown_roman_numeral_is_invalid(string romanNumeral)
    {
        var result = RomanNumeral.Create(romanNumeral);

        result.IsFailure.Should().BeTrue();
    }

    /// <summary>
    /// Numbers the outside roman numeral range is invalid.
    /// </summary>
    /// <param name="number">The number.</param>
    [Theory]
    [InlineData(-10)]
    [InlineData(0)]
    [InlineData(4000)]
    [InlineData(12345)]
    public void Number_outside_roman_numeral_range_is_invalid(int number)
    {
        var result = RomanNumeral.Create(number);

        result.IsFailure.Should().BeTrue();
    }

    /// <summary>
    /// Numbers the converts to roman numeral.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="expected">The expected.</param>
    [Theory]
    [InlineData(1, "I")]
    [InlineData(2, "II")]
    [InlineData(3, "III")]
    [InlineData(4, "IV")]
    [InlineData(5, "V")]
    [InlineData(6, "VI")]
    [InlineData(7, "VII")]
    [InlineData(8, "VIII")]
    [InlineData(9, "IX")]
    [InlineData(10, "X")]
    [InlineData(16, "XVI")]
    [InlineData(27, "XXVII")]
    [InlineData(32, "XXXII")]
    [InlineData(58, "LVIII")]
    [InlineData(183, "CLXXXIII")]
    [InlineData(555, "DLV")]
    [InlineData(45, "XLV")]
    [InlineData(50, "L")]
    [InlineData(98, "XCVIII")]
    [InlineData(99, "XCIX")]
    [InlineData(100, "C")]
    [InlineData(400, "CD")]
    [InlineData(500, "D")]
    [InlineData(994, "CMXCIV")]
    [InlineData(900, "CM")]
    [InlineData(999, "CMXCIX")]
    [InlineData(1000, "M")]
    [InlineData(3999, "MMMCMXCIX")]
    public void Number_converts_to_roman_numeral(int value, string expected)
    {
        // Arrange
        var sut = RomanNumeral.Create(value).Value;

        // Act
        var result = sut.Value;

        // Assert
        result.Should().Be(expected);
    }
}
