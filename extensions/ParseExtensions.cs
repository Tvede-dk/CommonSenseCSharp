using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace CommonSenseCSharp.extensions
{
    public static class ParseExtensions
    {
        public static float FloatSafeParse([CanBeNull] string str)
        {
            float f = 0;
            float.TryParse(str, out f);
            return f;
        }
    }
}