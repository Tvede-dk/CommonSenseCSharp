using System.Collections.Generic;
using CommonSenseCSharp.datastructures;
using JetBrains.Annotations;

namespace CommonSenseCSharp.extensions {
    public static class KeyPairExtensions {
        public static KeyValuePair<TB, TA> Reverse<TA, TB>(this KeyValuePair<TA, TB> key) => new KeyValuePair<TB, TA>(key.Value, key.Key);

        public static SafePair<TB, TA> Reverse<TA, TB>(this SafePair<TA, TB> pair) => new SafePair<TB, TA>(pair.Second, pair.First);
    }
}