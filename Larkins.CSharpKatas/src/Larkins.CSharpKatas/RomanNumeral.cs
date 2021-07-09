using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Larkins.CSharpKatas
{
    public class RomanNumeral
    {
        public RomanNumeral(int arabicNumeral, string romanNumeral)
        {
            IntValue = arabicNumeral;
            Value = romanNumeral;
        }

        public int IntValue { get; }

        public string Value { get; }

        public static Result<RomanNumeral> Create(string romanNumeral)
        {
            if (!IsValid(romanNumeral))
            {
                return Result.Failure<RomanNumeral>($"'{romanNumeral}' is an invalid Roman Numeral");
            }

            var intValue = 0;

            for (var i = 0; i < romanNumeral.Length; i++)
            {
                var currentCharacterResult = RomanNumeralCharacter.Create(romanNumeral[i]);

                if (currentCharacterResult.IsFailure)
                {
                    return Result.Failure<RomanNumeral>(currentCharacterResult.Error);
                }

                var currentValue = currentCharacterResult.Value;
                RomanNumeralCharacter nextCharacter = null;
                if (i < romanNumeral.Length - 1)
                {
                    var nextCharacterResult = RomanNumeralCharacter.Create(romanNumeral[i + 1]);

                    if (nextCharacterResult.IsFailure)
                    {
                        return Result.Failure<RomanNumeral>(nextCharacterResult.Error);
                    }

                    nextCharacter = nextCharacterResult.Value;
                }

                if (currentValue.IsSubtractable && currentValue.Value < nextCharacter?.Value)
                {
                    intValue -= currentValue.Value;
                    intValue += nextCharacter.Value;
                    i++;
                }
                else
                {
                    intValue += currentValue.Value;
                }
            }

            return new RomanNumeral(intValue, romanNumeral);

            static bool IsValid(string romanNumeral)
            {
                if (string.IsNullOrEmpty(romanNumeral))
                {
                    return false;
                }

                var romanNumeralPattern = "^M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$";
                var regex = new Regex(romanNumeralPattern);

                return regex.IsMatch(romanNumeral);
            }
        }

        public static Result<RomanNumeral> Create(int arabicNumeral)
        {
            if (arabicNumeral is <= 0 or >= 4000)
            {
                return Result.Failure<RomanNumeral>($"Arabic numeral {arabicNumeral} must be between 1 and 3999.");
            }

            var rm = "";

            var divider = 1000;

            while (divider > 0)
            {
                var digit = arabicNumeral / divider;
                arabicNumeral %= divider;

                if (divider == 1000)
                {
                    rm += DigitCharacter(digit, "M", "", "");
                }
                else if (divider == 100)
                {
                    rm += DigitCharacter(digit, "C", "D", "M");
                }
                else if (divider == 10)
                {
                    rm += DigitCharacter(digit, "X", "L", "C");
                }
                else if (divider == 1)
                {
                    rm += DigitCharacter(digit, "I", "V", "X");
                }

                divider /= 10;
            }

            return new RomanNumeral(arabicNumeral, rm);

            static string DigitCharacter(int digit, string one, string five, string ten)
            {
                if (digit == 9)
                {
                    return one + ten;
                }

                if (digit >= 5)
                {
                    var rm = five;

                    for (var i = 0; i < digit - 5; i++)
                    {
                        rm += one;
                    }

                    return rm;
                }

                if (digit == 4)
                {
                    return one + five;
                }

                var rm2 = "";

                for (var i = 0; i < digit; i++)
                {
                    rm2 += one;
                }

                return rm2;
            }
        }

        private class RomanNumeralCharacter
        {
            private RomanNumeralCharacter(char character, int value)
            {
                Character = character;
                Value = value;
            }

            public char Character { get; }

            public int Value { get; }

            public bool IsSubtractable => new List<int> { 1, 10, 100 }.Contains(Value);

            public static Result<RomanNumeralCharacter> Create(char character)
            {
                var valueResult = RomanNumeralValueLookUp(character);

                if (valueResult.IsFailure)
                {
                    return Result.Failure<RomanNumeralCharacter>(valueResult.Error);
                }

                return new RomanNumeralCharacter(character, valueResult.Value);
            }

            public static Result<RomanNumeralCharacter> Create(int value)
            {
                var charResult = RomanNumeralCharacterLookUp(value);

                if (charResult.IsFailure)
                {
                    return Result.Failure<RomanNumeralCharacter>(charResult.Error);
                }

                return new RomanNumeralCharacter(charResult.Value, value);
            }

            private static Result<int> RomanNumeralValueLookUp(char character)
            {
                return character switch
                {
                    'I' => 1,
                    'V' => 5,
                    'X' => 10,
                    'L' => 50,
                    'C' => 100,
                    'D' => 500,
                    'M' => 1000,
                    _ => Result.Failure<int>("Unrecognised character.")
                };
            }

            private static Result<char> RomanNumeralCharacterLookUp(int value)
            {
                return value switch
                {
                    1 => 'I',
                    5 => 'V',
                    10 => 'X',
                    50 => 'L',
                    100 => 'C',
                    500 => 'D',
                    1000 => 'M',
                    _ => Result.Failure<char>("No character associated with this value.")
                };
            }
        }
    }
}
