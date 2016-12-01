using System;
using System.Collections.Generic;
using JetBrains.Annotations;

public static class DictonaryUtil
{
    public static T GetSafe<T, TK>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK lookup,
        [CanBeNull] T fallback = default(T))
    {
        return dict.ContainsKey(lookup) ? dict[lookup] : fallback;
    }

    public static void AddAll<TK, T>([NotNull] this Dictionary<TK, T> dict, [NotNull] Func<T, TK> transformer,
        [CanBeNull] params T[] items)
    {
        items?.FlatForeach(item => dict.Add(transformer(item), item));
    }

    public static void PerformIfContains<TK, T>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK key,
        [NotNull] Action<T> onContains)
    {
        if (dict.ContainsKey((key)))
        {
            onContains(dict[key]);
        }
    }


    public static Dictionary<TK, T> AddF<TK, T>([NotNull] this Dictionary<TK, T> dict, [NotNull] TK key,
        [NotNull] T value)
    {
        dict.Add(key, value);
        return dict;
    }

    public static Dictionary<string, T> RemoveAll<T>([NotNull] this Dictionary<string, T> dict,
        [CanBeNull] params string[] toRemove)
    {
        toRemove?.FlatForeach(dict.Remove);
        return dict;
    }
}