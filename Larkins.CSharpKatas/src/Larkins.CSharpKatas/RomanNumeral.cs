using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
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

            var lookups = RomanNumeralValueLookUps();

            var intValue = 0;

            for (var i = 0; i < romanNumeral.Length; i++)
            {
                var currentValue = lookups.ToList().Find(x => x.RomanNumeral == romanNumeral[i].ToString()).Value;
                var nextCharacterValue = 0;

                if (i < romanNumeral.Length - 1)
                {
                    nextCharacterValue = lookups.ToList().Find(x => x.RomanNumeral == romanNumeral[i + 1].ToString()).Value;
                }

                if (new List<int> {1, 10, 100}.Contains(currentValue) && currentValue < nextCharacterValue)
                {
                    intValue -= currentValue;
                    intValue += nextCharacterValue;
                    i++;
                }
                else
                {
                    intValue += currentValue;
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
    }
}
