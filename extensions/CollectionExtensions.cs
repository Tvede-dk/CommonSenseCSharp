using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommonSenseCSharp.datastructures;
using JetBrains.Annotations;

namespace CommonSenseCSharp.extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// returns the element IFF its safe to retrive (valid index) otherwise, default(T) is returned
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [CanBeNull]
        public static T GetSafe<T>([NotNull] this ICollection<T> collection, int index,
            [CanBeNull] T defaultValue = default(T))
        {
            if (collection.Count > index && index >= 0)
            {
                return collection.ElementAt(index);
            }
            return defaultValue;
        }

        public static void ClearAndSet<T>([NotNull] this IList<T> list, [NotNull] IEnumerable<T> otherList)
        {
            list.Clear();
            otherList.FlatForeach(list.Add);
        }

        [CanBeNull]
        public static object FirstOrDefault([NotNull] this IList collection, [CanBeNull] object defaultValue)
        {
            return collection.Count > 0 ? collection[0] : defaultValue;
        }

        [CanBeNull]
        public static T FirstOrDefault<T>([NotNull] this IList collection, [CanBeNull] T defaultValue) where T : class
        {
            return collection.FirstOrDefault((object) defaultValue) as T;
        }


        public static void UseFirst<T>([NotNull] this IList collection, Action<T> onFirst) where T : class
        {
            var item = collection[0] as T;
            if (collection.Count > 0 && item != null)
            {
                onFirst(item);
            }
        }

        public static void UseSafe<T>([NotNull] this IList collection, Action<IEnumerable<T>> onCollection)
            where T : class
        {
            if (collection.Count > 0 && collection[0] is T)
            {
                onCollection(collection.Cast<T>());
            }
        }

        [NotNull]
        public static T ReverseResult<T, U>([NotNull] this T collection) where T : List<U>
        {
            collection.Reverse();
            return collection;
        }

        [NotNull]
        public static List<T> ReverseResult<T>([NotNull] this List<T> collection)
        {
            collection.Reverse();
            return collection;
        }

        [NotNull]
        public static NonNullList<T> ReverseResult<T>([NotNull] this NonNullList<T> collection)
        {
            collection.Reverse();
            return collection;
        }

        [NotNull]
        public static T AddReturn<T>([NotNull] this ICollection<T> list, [NotNull] T obj)
        {
            list.Add(obj);
            return obj;
        }

        [NotNull]
        public static ICollection<T> AddFluent<T>([NotNull] this ICollection<T> list, [NotNull] T obj)
        {
            list.Add(obj);
            return list;
        }
    }
}