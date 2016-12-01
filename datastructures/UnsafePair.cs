namespace CommonSenseCSharp.datastructures {
    public class UnsafePair<T, TU> {
        public T First { get; set; }
        public TU Second { get; set; }

        public UnsafePair() {

        }
        public UnsafePair(T first, TU second) {
            this.First = first;
            this.Second = second;
        }
    }
}
