using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace CommonSenseCSharp.datastructures
{
    public class SafePair<T, TU>
    {
        [NotNull] public readonly T First;
        [NotNull] public readonly TU Second;

        public SafePair([NotNull] T a, [NotNull] TU b)
        {
            First = a;
            Second = b;
        }
    }
}