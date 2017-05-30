using CommonSenseCSharp.datastructures;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace CommonSenseCSharp.extensions {
    public static class ListExtensions {

        [NotNull]
        public static NonNullList<T> SafeCast<T, U>([NotNull] this IEnumerable<U> items) where T : class => items.FlatMap(x => x as T);


    }
}
