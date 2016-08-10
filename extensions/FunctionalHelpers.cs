﻿using CommonSenseCSharp.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class FunctionalHelpers {
    /// <summary>
    /// Maps a collection to another type of collection using a converter / generator
    /// </summary>
    /// <typeparam name="U">the result type</typeparam>
    /// <typeparam name="T">the input type</typeparam>
    /// <param name="input">the input list</param>
    /// <param name="generator">the generator / converter </param>
    /// <returns>the resulting collection</returns>
    public static List<U> Map<U, T>(this IEnumerable<T> input, Func<T, U> generator) {
        var result = new List<U>(input.Count());
        foreach (var item in input) {
            result.Add(generator(item));
        }
        return result;
    }

    public static NonNullList<U> FlatMap<U, T>(this IEnumerable<T> input, Func<T, U> generator) {
        var result = new NonNullList<U>(input.Count());
        foreach (var item in input) {
            result.Add(generator(item));
        }
        return result;
    }

    /// <summary>
    /// Performs and action over a IEnumerable (given an action.) 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="onEach"></param>
    public static void Foreach<T>(this IEnumerable<T> list, Action<T> onEach) {
        foreach (var item in list) {
            onEach(item);
        }
    }

    public static void FlatForeach<T>(this IEnumerable<T> list, Action<T> onEach) {
        foreach (var item in list) {
            if (item != null) {
                onEach(item);
            }
        }
    }

    public static void FlatForeach<T, V>(this IEnumerable<T> list, Func<T, V> onEach) {
        foreach (var item in list) {
            if (item != null) {
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
    public static void Foreach<T, U>(this IEnumerable<T> list, Func<T, U> onEach) {
        foreach (var item in list) {
            onEach(item);
        }
    }
    /// <summary>
    /// Performs the given action each time starting from 0 upto the value.
    /// Eg. replace a for loop over the variable. 
    /// </summary>
    /// <param name="times"></param>
    /// <param name="callback"></param>
    public static void PerformEachTime(this int times, Action<int> callback) {
        0.PerformTimes(times, callback);
    }

    /// <summary>
    /// Performs the given action each time starting from start; count times.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="count"></param>
    /// <param name="callback"></param>
    public static void PerformTimes(this int start, int count, Action<int> callback) {
        for (int i = start; i < start + count; i++) {
            callback(i);
        }
    }

    /// <summary>
    /// Performs a task the given number of times unless the function returns true. 
    /// </summary>
    /// <param name="times"></param>
    /// <param name="callback"></param>
    /// <returns>the index we exited at (if no true returend it will be times -1 ).</returns>
    public static int PerformEachTimeUntil(this int times, Func<int, bool> callback) {
        for (var i = 0; i < times; i++) {
            if (callback(i) == true) {
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
    public static T PerformEachTimeUntil<T>(this int times, Func<int, T> callback, Func<T,bool> shouldReturn, T atEnd) {
        for (var i = 0; i < times; i++) {
            var tempRes = callback(i);
            if (shouldReturn(tempRes)) {
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
    public static bool DoOnTrue(this bool val, Action ifTrue) {
        if (val) {
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
    public static List<U> Flattern<U>(this IEnumerable<IEnumerable<U>> collection) {
        var result = new List<U>(collection.Count());
        collection.Foreach(result.AddRange);
        return result;
    }
    /// <summary>
    /// Flatterns a list (in i a  list) to a list by moving all elements in the depth into the same list. 
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static NonNullList<U> FlatFlattern<U>(this IEnumerable<IEnumerable<U>> collection) {
        var result = new NonNullList<U>(collection.Count());
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
    public static IEnumerable<U> Filter<U>(this IEnumerable<U> collection, Func<U, bool> includeItem) {
        var result = new List<U>(collection.Count());
        collection.Foreach(x => { if (includeItem(x)) { result.Add(x); } });
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="collection"></param>
    /// <param name="includeItem"></param>
    /// <returns></returns>
    public static NonNullList<U> FlatFilter<U>(this IEnumerable<U> collection, Func<U, bool> includeItem) {
        var result = new NonNullList<U>(collection.Count());
        collection.Foreach(x => { if (includeItem(x)) { result.Add(x); } });
        return result;
    }


    public static NonNullList<T> FlatTakeFrom<T>(this IEnumerable<int> indexes, IEnumerable<T> collectionToTakeFrom) {
        var result = new NonNullList<T>();
        int max = collectionToTakeFrom.Count();
        foreach (var item in indexes) {
            if (item < max && item >= 0) { //in range
                result.Add(collectionToTakeFrom.ElementAt(item));//add (iff not null, inforced by collection).
            }
        }
        return result;
    }


}