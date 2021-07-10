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

            var lookups = RomanNumeralValueLookUps();
            var convertedRomanNumeral = "";

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
