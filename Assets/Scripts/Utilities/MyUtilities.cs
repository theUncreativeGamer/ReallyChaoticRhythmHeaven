using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public static class MyUtilities
{
    public static bool ListsContainSameElements<T>(List<T> list1, List<T> list2)
    {
        if (list1 == null || list2 == null)
            return false;

        if (list1.Count != list2.Count)
            return false;

        var sortedList1 = list1.OrderBy(x => x).ToList();
        var sortedList2 = list2.OrderBy(x => x).ToList();

        return sortedList1.SequenceEqual(sortedList2);
    }
    public static T GetRandom<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException("Cannot select a random element from an empty or null list.");
        }
        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }
}


[System.Serializable]
public struct Range<T> where T : struct, IComparable<T>
{
    public T min;
    public T max;

    public Range(T min, T max)
    {
        if (min.CompareTo(max) > 0)
            throw new ArgumentException("min should be less than or equal to max");

        this.min = min;
        this.max = max;
    }

    public readonly bool Contains(T value)
    {
        return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }

    public readonly T Clamp(T value)
    {
        if (value.CompareTo(min) < 0) return min;
        if (value.CompareTo(max) > 0) return max;
        return value;
    }
}

