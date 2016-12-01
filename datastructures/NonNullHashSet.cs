using System.Collections.Generic;

namespace CommonSenseCSharp.datastructures {
    public class NonNullHashSet<T> : HashSet<T> {
        public new void Add(T item) {
            if (item == null) {
                return;
            }
            base.Add(item);
        }


    }
}
