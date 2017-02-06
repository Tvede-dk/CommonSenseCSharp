using JetBrains.Annotations;

namespace CommonSenseCSharp.extensions{
    public static class CharExtensions{

        [NotNull]
        public static char AsLower([NotNull] this char c){
            return char.ToLower(c);
        }

    }
}