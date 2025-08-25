namespace Larkins.CSharpKatas.OneCharacterDifference;

public static class StringSimilarityCalculator
{
    public static bool IsAtMostOneCharacterDifference(
        string input1,
        string input2)
    {
        var lengthDifference = Math.Abs(input1.Length - input2.Length);

        if (lengthDifference > 1)
        {
            return false;
        }

        var differenceCount = 0;

        for (int i = 0, j = 0; i < input1.Length && j < input2.Length; i++, j++)
        {
            if (input1[i] == input2[j])
            {
                continue;
            }

            differenceCount++;

            if (differenceCount > 1)
            {
                return false;
            }

            if (lengthDifference == 0)
            {
                continue;
            }

            // Continue checking from the current character in the shorter string
            // Ignore the current character in the longer string as it doesn't match.
            if (input1.Length < input2.Length)
            {
                i--;
            }
            else
            {
                j--;
            }
        }

        return differenceCount == 1 || differenceCount == 0 && lengthDifference == 1;
    }
}
