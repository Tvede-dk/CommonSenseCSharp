using CommonSenseCSharp.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

public static class FunctionalHelpers
{
    /// <summary>
    /// Maps a collection to another type of collection using a converter / generator
    /// </summary>
    /// <typeparam name="U">the result type</typeparam>
    /// <typeparam name="T">the input type</typeparam>
    /// <param name="input">the input list</param>
    /// <param name="generator">the generator / converter </param>
    /// <returns>the resulting collection</returns>
    public static List<TU> Map<TU, T>(this IEnumerable<T> input, [NotNull] Func<T, TU> generator)
    {
        var result = new List<TU>(input.Count());
        foreach (var item in input)
        {
            result.Add(generator(item));
        }
        return result;
    }

    public static NonNullList<TU> FlatMap<TU, T>(this IEnumerable<T> input, [NotNull] Func<T, TU> generator)
    {
        var result = new NonNullList<TU>(input.Count());
        foreach (var item in input)
        {
            if (item != null)
            {
                result.Add(generator(item));
            }
        }
        return result;
    }


    public static NonNullList<TU> FlatMapWithGlue<TU, T>(this IEnumerable<T> input, [NotNull] Func<T, TU> map,
        [NotNull] Func<TU, TU> glue)
    {
        var result = new NonNullList<TU>();
        input.FlatForeachWithGlue(x => { result.Add(glue(map(x))); },
            null,
            x => { result.Add(map(x)); });
        return result;
    }

    public static void FlatForeachWithGlue<T>(this IEnumerable<T> input, Action<T> foreachCall, Action<T> glue)
    {
        input.FlatForeachWithGlue(foreachCall, glue, foreachCall);
    }

    public static void FlatForeachWithGlue<T>(this IEnumerable<T> input, [NotNull] Action<T> foreachCall,
        [CanBeNull] Action<T> glue,
        Action<T> last)
    {
        var safeInput = new NonNullList<T>(input);
        var lenght = safeInput.Count();
        if (lenght <= 0)
        {
            return;
        }
        for (var i = 0; i < lenght - 1; i++)
        {
            var item = safeInput.ElementAt(i);
            foreachCall?.Invoke(item);
            glue?.Invoke(item);
        }
        last?.Invoke(safeInput.Last());
    }

    /// <summary>
    /// Performs and action over a IEnumerable (given an action.)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="onEach"></param>
    public static void Foreach<T>(this IEnumerable<T> list, [NotNull] Action<T> onEach)
    {
        foreach (var item in list)
        {
            onEach(item);
        }
    }

    public static void FlatForeach<T>(this IEnumerable<T> list, [NotNull] Action<T> onEach)
    {
        foreach (var item in list)
        {
            if (item != null)
            {
                onEach(item);
            }
        }
    }

    public static void FlatForeach<T, TV>(this IEnumerable<T> list, [NotNull] Func<T, TV> onEach)
    {
        foreach (var item in list)
        {
            if (item != null)
            {
                onEach(item);
            }
        }
    }


    /// <summary>
    /// Performs and action over a IEnumerable (given a function) ignoring the result of a function
    /// this is convenince function, if the result should be used, consider using map.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="list"></param>
    /// <param name="onEach"></param>
    public static void Foreach<T, TU>(this IEnumerable<T> list, [NotNull] Func<T, TU> onEach)
    {
        foreach (var item in list)
        {
            onEach(item);
        }
    }

    /// <summary>
    /// Performs the given action each time starting from 0 upto the value.
    /// Eg. replace a for loop over the variable.
    /// </summary>
    /// <param name="times"></param>
    /// <param name="callback"></param>
    public static void PerformEachTime(this int times, [NotNull] Action<int> callback)
    {
        0.PerformTimes(times, callback);
    }

    /// <summary>
    /// Performs the given action each time starting from start; count times.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <param name="callback"></param>
    public static void PerformTimes(this int start, int count, [NotNull] Action<int> callback)
    {
        for (var i = start; i < start + count; i++)
        {
            callback(i);
        }
    }

    /// <summary>
    /// Performs a task the given number of times unless the function returns true.
    /// </summary>
    /// <param name="times"></param>
    /// <param name="callback"></param>
    /// <returns>the index we exited at (if no true returend it will be times -1 ).</returns>
    public static int PerformEachTimeUntil(this int times, [NotNull] Func<int, bool> callback)
    {
        for (var i = 0; i < times; i++)
        {
            if (callback(i) == true)
            {
                return i;
            }
        }
        return times - 1;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="times"></param>
    /// <param name="callback"></param>
    /// <param name="shouldReturn"></param>
    /// <param name="atEnd"></param>
    /// <returns></returns>
    public static T PerformEachTimeUntil<T>(this int times, [NotNull] Func<int, T> callback,
        [NotNull] Func<T, bool> shouldReturn, T atEnd)
    {
        for (var i = 0; i < times; i++)
        {
            var tempRes = callback(i);
            if (shouldReturn(tempRes))
            {
                return tempRes;
            }
        }
        return atEnd;
    }

    /// <summary>
    /// Performs an action if a value is true.
    /// </summary>
    /// <param name="val"></param>
    /// <param name="ifTrue"></param>
    /// <returns></returns>
    public static bool DoOnTrue(this bool val, [NotNull] Action ifTrue)
    {
        if (val)
        {
            ifTrue();
        }
        return val;
    }

    /// <summary>
    /// Flatterns a list (in i a  list) to a list by moving all elements in the depth into the same list.
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static List<TU> Flattern<TU>(this IEnumerable<IEnumerable<TU>> collection)
    {
        var result = new List<TU>(collection.Count());
        collection.Foreach(result.AddRange);
        return result;
    }

    /// <summary>
    /// Flatterns a list (in i a  list) to a list by moving all elements in the depth into the same list.
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static NonNullList<TU> FlatFlattern<TU>(this IEnumerable<IEnumerable<TU>> collection)
    {
        var result = new NonNullList<TU>(collection.Count());
        collection.Foreach(result.AddRange);
        return result;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="collection"></param>
    /// <param name="includeItem"></param>
    /// <returns></returns>
    public static IEnumerable<TU> Filter<TU>(this IEnumerable<TU> collection, [NotNull] Func<TU, bool> includeItem)
    {
        var result = new List<TU>(collection.Count());
        collection.Foreach(x =>
        {
            if (includeItem(x))
            {
                result.Add(x);
            }
        });
        return result;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="collection"></param>
    /// <param name="includeItem"></param>
    /// <returns></returns>
    public static NonNullList<TU> FlatFilter<TU>(this IEnumerable<TU> collection, [NotNull] Func<TU, bool> includeItem)
    {
        var result = new NonNullList<TU>(collection.Count());
        collection.Foreach(x =>
        {
            if (includeItem(x))
            {
                result.Add(x);
            }
        });
        return result;
    }


    public static NonNullList<T> FlatTakeFrom<T>(this IEnumerable<int> indexes,
        [NotNull] IEnumerable<T> collectionToTakeFrom)
    {
        var result = new NonNullList<T>();
        var max = collectionToTakeFrom.Count();
        foreach (var item in indexes)
        {
            if (item < max && item >= 0)
            {
                //in range
                result.Add(collectionToTakeFrom.ElementAt(item)); //add (iff not null, inforced by collection).
            }
        }
        return result;
    }


    /// <summary>
    /// zips the 2 lists, meaning that element from listA and listB are besides each other, so they form a pair.
    /// if any part is null, then that is keept (hereas the name "UnsafePair").
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <param name="listA"></param>
    /// <param name="listB"></param>
    /// <returns></returns>
    public static NonNullList<UnsafePair<T, TU>> FlatZip<T, TU>(this IEnumerable<T> listA,
        [NotNull] IEnumerable<TU> listB)
    {
        var result = new NonNullList<UnsafePair<T, TU>>();
        var enA = listA.GetEnumerator();
        var enB = listB.GetEnumerator();
        var enAContains = enA.MoveNext();
        var enBContains = enB.MoveNext();
        while (enAContains || enBContains)
        {
            var a = default(T);
            var b = default(TU);
            if (enAContains)
            {
                a = enA.Current;
                enAContains = enA.MoveNext();
            }
            if (enBContains)
            {
                b = enB.Current;
                enBContains = enB.MoveNext();
            }
            result.Add(new UnsafePair<T, TU>(a, b));
        }
        enA.Dispose();
        enB.Dispose();
        return result;
    }

    /// <summary>
    /// will do the same as zip, except it will chop to the shortest list, and make sure all items are not null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="listA"></param>
    /// <param name="listB"></param>
    /// <returns></returns>
    public static NonNullList<SafePair<T, TU>> FlatZipSafe<T, TU>(this NonNullList<T> listA,
        [NotNull] NonNullList<TU> listB)
    {
        var result = new NonNullList<SafePair<T, TU>>(Math.Min(listA.Count(), listB.Count()));
        ArrayUtil.ForEach(listA, listB, (x, y) => result.Add(new SafePair<T, TU>(x, y)));
        return result;
    }

    public static void FlatForeach<T>([NotNull] this IList<T> list, int start, int endExclusive,
        [NotNull] Action<T> onItem)
    {
        //validate indexes are valid and in order..
        if (start >= endExclusive || start < 0 || endExclusive < 0 || start >= list.Count() ||
            endExclusive > list.Count())
        {
            return;
        }
        //then itterate in that range.
        for (var i = start; i < endExclusive; i++)
        {
            onItem(list.ElementAt(i));
        }
    }

    public static void IfSafe<T>([CanBeNull] this T obj, Action<T> action)
    {
        if (obj != null)
        {
            action(obj);
        }
    }

    public static U IfSafe<T, U>([CanBeNull] this T obj, Func<T, U> action)
    {
        return obj != null ? action(obj) : default(U);
    }
}