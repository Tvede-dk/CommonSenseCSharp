using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSenseCSharp.datastructures {
    public class UnsafePair<T, U> {
        public T first { get; set; }
        public U second { get; set; }

        public UnsafePair() {

        }
        public UnsafePair(T first, U second) {
            this.first = first;
            this.second = second;
        }
    }
}
