using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace CommonSenseCSharp.extensions{
    public static class StringBuilderExtensions{
        [NotNull]
        public static StringBuilder AppendLines([NotNull] this StringBuilder sb, [NotNull] IEnumerable<string> listOfStrings){
            listOfStrings.FlatForeach(sb.AppendLine);
            return sb;
        }
    }
}