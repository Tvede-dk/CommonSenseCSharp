using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


public static class ArrayUtil {

    public static void ForEach<Ta, Tb>(IEnumerable<Ta> firstList, IEnumerable<Tb> secoundList, Action<Ta, Tb> onEach) {
        Contract.Requires(firstList != null);
        if (firstList == null || secoundList == null && onEach == null) {
            return;
        }
        var minItterations = Math.Min(firstList.Count(), secoundList.Count());
        for (var i = 0; i < minItterations; i++) {
            onEach(firstList.ElementAt(i), secoundList.ElementAt(i));
        }
    }
    public static void ForEach<Ta, Tb, Tc>(IEnumerable<Ta> firstList, IEnumerable<Tb> secoundList, IEnumerable<Tc> thirdList, Action<Ta, Tb, Tc> onEach) {
        if (firstList == null || secoundList == null || thirdList == null || onEach == null) {
            return;
        }
        var minItterations = Math.Min(Math.Min(firstList.Count(), secoundList.Count()), thirdList.Count());
        for (var i = 0; i < minItterations; i++) {
            onEach(firstList.ElementAt(i), secoundList.ElementAt(i), thirdList.ElementAt(i));
        }
    }

    public static void updateOrInsert<T>(this IList<T> lst, T objToInsert, Func<T, bool> predicate) {
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


public static class DictonaryUtil {


    public static T GetSafe<T, K>(this Dictionary<K, T> dict, K lookup, T fallback = default(T)) {

        if (dict != null && lookup != null) {
            if (dict.ContainsKey(lookup)) {
                return dict[lookup];
            }
        }
        return fallback;
    }

    public static Dictionary<K, T> AddF<K, T>(this Dictionary<K, T> dict, K key, T value) {
        dict.Add(key, value);
        return dict;
    }
    public static Dictionary<string, T> RemoveAll<T>(this Dictionary<string, T> dict, params string[] toRemove) {
        toRemove.FlatForeach(dict.Remove);
        return dict;
    }
}