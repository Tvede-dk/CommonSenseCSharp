using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace CommonSenseCSharp.extensions{
    public static class StringBuilderExtensions{
        [NotNull]
        public static StringBuilder AppendLines([NotNull] this StringBuilder sb,
            [NotNull] IEnumerable<string> listOfStrings){
            listOfStrings.FlatForeach(sb.AppendLine);
            return sb;
        }

        /// <summary>
        /// Returns the index of the start of the contents in a StringBuilder
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="searchingFor">The string to find</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="ignoreCase">if set to <c>true</c> it will ignore case</param>
        /// <returns></returns>
        [IndexOfRangeAttribute]
        public static int IndexOf([NotNull] StringBuilder stringBuilder, [NotNull] string searchingFor,
            [PositiveIntRange] int startIndex = 0, bool ignoreCase = false){
            if (ignoreCase){
                return IndexOfComparar(stringBuilder, searchingFor,
                    (charA, charB) => charA.AsLower() == charB.AsLower());
            }
            return IndexOfComparar(stringBuilder, searchingFor,
                (charA, charB) => charA == charB);
        }

        [IndexOfRangeAttribute]
        public static int IndexOfComparar([NotNull] StringBuilder stringBuilder, [NotNull] string value,
            [NotNull] Func<char, char, bool> compFunc,
            [PositiveIntRange] int startIndex = 0){
            var maxSearchLength = (stringBuilder.Length - value.Length) + 1;

            for (var i = startIndex; i < maxSearchLength; i++){
                if (compFunc(stringBuilder[i], value[0])){
                    var index = 0;
                    bool isEqual;
                    do{
                        isEqual = compFunc(stringBuilder[i + index], value[index]);
                        index++;
                    } while (isEqual && index < value.Length);

                    if (index + 1 == value.Length){
                        return index + i;
                    }
                }
            }

            return IndexOfRangeAttribute.NotFound;
        }
    }
}