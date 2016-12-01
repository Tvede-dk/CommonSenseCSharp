using System;
using System.Collections.Generic;
using System.Linq;


public static class ArrayUtil {

    public static void ForEach<TA, TB>(IEnumerable<TA> firstList, IEnumerable<TB> secoundList, Action<TA, TB> onEach) {
        if (firstList == null || secoundList == null && onEach == null) {
            return;
        }
        var minItterations = Math.Min(firstList.Count(), secoundList.Count());
        for (var i = 0; i < minItterations; i++) {
            onEach(firstList.ElementAt(i), secoundList.ElementAt(i));
        }
    }
    public static void ForEach<TA, TB, TC>(IEnumerable<TA> firstList, IEnumerable<TB> secoundList, IEnumerable<TC> thirdList, Action<TA, TB, TC> onEach) {
        if (firstList == null || secoundList == null || thirdList == null || onEach == null) {
            return;
        }
        var minItterations = Math.Min(Math.Min(firstList.Count(), secoundList.Count()), thirdList.Count());
        for (var i = 0; i < minItterations; i++) {
            onEach(firstList.ElementAt(i), secoundList.ElementAt(i), thirdList.ElementAt(i));
        }
    }

    public static void UpdateOrInsert<T>(this IList<T> lst, T objToInsert, Func<T, bool> predicate) {
        var i = 0;
        foreach (var item in lst) {
            if (predicate(item)) {
                break;
            }
            i++;
        }
        if (i >= lst.Count) {
            lst.Add(objToInsert);
        } else {
            lst.RemoveAt(i);
            lst.Insert(i, objToInsert);
        }
    }


}

