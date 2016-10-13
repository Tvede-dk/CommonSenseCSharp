using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
