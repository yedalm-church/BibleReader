using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static bool IsValidIndex<T>(this List<T> list, int index)
    {
        return index >= 0 && index < list.Count;
    }

    public static bool IsValidIndex<T>(this T[] array, int index)
    {
        return array != null && index >= 0 && index < array.Length;
    }
}
