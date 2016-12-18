using System.Collections.Generic;
using CommonSenseCSharp.datastructures;
using JetBrains.Annotations;

namespace CommonSenseCSharp.extensions
{
    public static class ObjectExtensions
    {
        [NotNull]
        public static NonNullList<T> MapToList<T>([CanBeNull] T obj, [CanBeNull] IEnumerable<T> orList)
        {
            var res = new NonNullList<T>();
            obj?.IfSafe(res.Add);
            return orList?.IfSafe(res.AddRangeFluent) ?? res;
        }

        [NotNull]
        public static NonNullList<T> MapToList<T>([CanBeNull] object obj, [CanBeNull] IEnumerable<object> orList)
            where T : class
        {
            var res = new NonNullList<T>();
            obj?.IfSafeCast<T>(res.Add);
            return orList?.FlatCast<T, object>().IfSafe(res.AddRangeFluent) ?? res;
        }
    }
}