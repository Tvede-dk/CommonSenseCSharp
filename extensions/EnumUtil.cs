using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonSenseCSharp.extensions {
    public static class EnumUtil {
        public static IEnumerable<TEnum> GetAllValues<TEnum>()
            where TEnum : struct, IComparable, IFormattable => Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        public static TEnum TryParseOrDefault<TEnum>(string value, bool ignoreCase, TEnum defaultVal)
        where TEnum : struct, IComparable, IFormattable {
            if (!Enum.TryParse(value, ignoreCase, out TEnum current)) {
                current = defaultVal;
            }
            return current;
        }
    }
}
