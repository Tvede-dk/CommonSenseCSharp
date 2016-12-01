using System;
using System.Diagnostics.Contracts;

namespace CommonSenseCSharp.datastructures {
    public class SafePair<T, TU> {
        public readonly T First;
        public readonly TU Second; 
        public SafePair(T a, TU b) {
            Contract.Requires<ArgumentNullException>(a != null && b != null, "a and or b cannot be null");
            First = a;
            Second = b;
        }
    }
}
