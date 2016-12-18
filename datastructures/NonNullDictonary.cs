using System.Collections.Generic;
using JetBrains.Annotations;

namespace CommonSenseCSharp.datastructures
{
    public class NonNullDictonary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public void AddIfNotThere([NotNull] TKey key, [NotNull] TValue item)
        {
            if (!ContainsKey(key))
            {
                Add(key, item);
            }
        }
    }
}