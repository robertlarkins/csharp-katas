﻿using System.Text.RegularExpressions;
using Larkins.CSharpKatas.ResultType;

namespace Larkins.CSharpKatas;

public class RomanNumeral
{
    private RomanNumeral(int arabicNumeral, string romanNumeral)
    {
        IntValue = arabicNumeral;
        Value = romanNumeral;
    }

    public int IntValue { get; }

    public string Value { get; }

    /// <summary>
    /// Creates a roman numeral from the given string.
    /// </summary>
    /// <param name="romanNumeral">The roman numeral.</param>
    /// <returns>The roman numeral result.</returns>
    public static Result<RomanNumeral> Create(string romanNumeral)
    {
        if (!IsRomanNumeralStringValid(romanNumeral))
        {
            return Result.Failure<RomanNumeral>($"'{romanNumeral}' is an invalid Roman Numeral.");
        }

        var intValue = ConvertRomanNumeralToIntValue(romanNumeral);

        return new RomanNumeral(intValue, romanNumeral);
    }

    /// <summary>
    /// Creates a roman numeral from the given arabic numeral.
    /// </summary>
    /// <param name="arabicNumeral">The arabic numeral.</param>
    /// <returns>The roman numeral result.</returns>
    public static Result<RomanNumeral> Create(int arabicNumeral)
    {
        if (arabicNumeral is <= 0 or >= 4000)
        {
            return Result.Failure<RomanNumeral>($"Arabic numeral {arabicNumeral} must be between 1 and 3999.");
        }

        var lookups = RomanNumeralValueLookUps();
        var convertedRomanNumeral = string.Empty;

        foreach (var (romanNumeral, value) in lookups)
        {
            while (arabicNumeral >= value)
            {
                convertedRomanNumeral += romanNumeral;
                arabicNumeral -= value;
            }
        }

        return new RomanNumeral(arabicNumeral, convertedRomanNumeral);
    }

    private static bool IsRomanNumeralStringValid(string romanNumeral)
    {
        if (string.IsNullOrEmpty(romanNumeral))
        {
            return false;
        }

        var romanNumeralPattern = "^M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$";
        var regex = new Regex(romanNumeralPattern);

        return regex.IsMatch(romanNumeral);
    }

    private static int ConvertRomanNumeralToIntValue(string romanNumeral)
    {
        var valueLookup = RomanNumeralFromValueDictionary();
        var intValue = valueLookup[romanNumeral.Last().ToString()];

        for (var i = romanNumeral.Length - 2; i >= 0; i--)
        {
            var currentCharacterValue = valueLookup[romanNumeral[i].ToString()];
            var previousCharacterValue = valueLookup[romanNumeral[i + 1].ToString()];

            if (currentCharacterValue < previousCharacterValue)
            {
                intValue -= currentCharacterValue;
            }
            else
            {
                intValue += currentCharacterValue;
            }
        }

        return intValue;
    }

    private static IEnumerable<(string RomanNumeral, int Value)> RomanNumeralValueLookUps()
    {
        return new List<(string RomanNumeral, int Value)>
        {
            ("M", 1000),
            ("CM", 900),
            ("D", 500),
            ("CD", 400),
            ("C", 100),
            ("XC", 90),
            ("L", 50),
            ("XL", 40),
            ("X", 10),
            ("IX", 9),
            ("V", 5),
            ("IV", 4),
            ("I", 1)
        };
    }

    private static Dictionary<string, int> RomanNumeralFromValueDictionary()
    {
        var lookups = RomanNumeralValueLookUps();

        return lookups.ToDictionary(x => x.RomanNumeral, x => x.Value);
    }
}
