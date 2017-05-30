using JetBrains.Annotations;

namespace CommonSenseCSharp.datastructures
{
    public struct SafePair<T, TU>
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