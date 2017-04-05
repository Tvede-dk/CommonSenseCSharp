using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using CommonSenseCSharp.datastructures;
using CommonSenseCSharp.extensions;
using JetBrains.Annotations;

public static class DictonaryUtil {
    public static T GetSafe<T, TK>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK lookup) => dict.GetSafe(lookup, default(T));

    public static T GetSafe<T, TK>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK lookup,
        [CanBeNull] T fallback) => dict.ContainsKey(lookup) ? dict[lookup] : fallback;

    public static NonNullList<T> GetAllSafe<T, TK>([NotNull] this Dictionary<TK, T> dict, params TK[] lookups) => lookups.FlatMap(dict.GetSafe);

    public static void UseValue<T, TK>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK lookup,
        [NotNull] Action<T> onAction) => dict.GetSafe(lookup)?.IfSafe(onAction);

    public static void UseAndFlatTransform<T, TK, TU>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK lookup,
        [NotNull] Func<T, TU> transform, [NotNull] Action<TU> onSuccess) => dict.FlatPerformIfContainsKey(lookup, x => { transform(x)?.IfSafe(onSuccess); });

    [NotNull]
    public static TU UseAndFlatTransform<T, TK, TU>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK lookup,
        [NotNull] Func<T, TU> transform) where TU : struct => dict.GetSafe(lookup).IfSafe(transform);

    [NotNull]
    public static TU UseAndFlatTransform<T, TK, TU>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK lookup,
        [NotNull] Func<T, TU> transform, [NotNull] TU defaultValue) where TU : class => dict.GetSafe(lookup).IfSafe(transform) ?? defaultValue;


    public static void AddAll<TK, T>([NotNull] this Dictionary<TK, T> dict, [NotNull] Func<T, TK> transformer,
        [CanBeNull] params T[] items) => items?.FlatForeach(item => dict.Add(transformer(item), item)); //todo flatmap first ??? or too slow ?

    public static void PerformIfContains<TK, T>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK key,
        [NotNull] Action<T> onContains) {
        if (dict.ContainsKey(key)) {
            onContains(dict[key]);
        }
    }

    public static void FlatPerformIfContainsKey<TK, T>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK key,
        [NotNull] Action<T> onContains) {
        if (dict.ContainsKey((key))) {
            dict[key]?.IfSafe(onContains);
        }
    }

    public static void FlatPerformIfContainsKey<TK, T>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK key,
        [NotNull] Action<T> onContains, [NotNull] Action ifNot) {
        if (dict.ContainsKey((key))) {
            dict[key]?.IfSafe(onContains);
        } else {
            ifNot();
        }
    }

    public static void Add<TK, T>([NotNull] this Dictionary<TK, T> dict, KeyValuePair<TK, T> data) => dict.Add(data.Key, data.Value);

    [NotNull]
    public static Dictionary<TK, T> AddF<TK, T>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK key,
        [NotNull] T value) {
        dict.Add(key, value);
        return dict;
    }

    [NotNull]
    public static Dictionary<string, T> RemoveAll<T>([NotNull] this Dictionary<string, T> dict,
        [CanBeNull] params string[] toRemove) {
        toRemove?.FlatForeach(dict.Remove);
        return dict;
    }

    [NotNull]
    public static void ClearAndSet<T>([NotNull] this Dictionary<string, T> dict, IDictionary<string, T> otherDictToCopy) {
        dict.Clear();
        otherDictToCopy.FlatForeach(dict.Add);
    }

    [CanBeNull]
    public static T GetAndRemove<TK, T>([NotNull] this Dictionary<TK, T> dict,[NotNull] TK key) {
        var temp = dict.GetSafe(key);
        dict.Remove(key);
        return temp;
    }

    public static void InsertInto<TKey, TValue, U>([NotNull] this Dictionary<TKey, U> dict,
        [NotNull] TKey key, [NotNull] TValue value, Func<U> listCreator) where U : IList<TValue> {
        if (!dict.ContainsKey(key)) {
            dict.Add(key, listCreator());
        }
        dict[key].Add(value);
    }
}