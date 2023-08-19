namespace Larkins.CSharpKatas.Extensions;

public static class ArrayExtensions
{
    public static void SwapElements<T>(this T[] array, int index1, int index2)
    {
        (array[index1], array[index2]) = (array[index2], array[index1]);
    }
}
