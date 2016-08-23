using System;
using System.Diagnostics.Contracts;

namespace CommonSenseCSharp.datastructures {
    public class SafePair<T, U> {
        public readonly T First;
        public readonly U Second; 
        public SafePair(T a, U b) {
            Contract.Requires<ArgumentNullException>(a != null && b != null, "a and or b cannot be null");
            First = a;
            Second = b;
        }
    }
}
