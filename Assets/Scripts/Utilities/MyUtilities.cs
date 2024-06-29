using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
}
