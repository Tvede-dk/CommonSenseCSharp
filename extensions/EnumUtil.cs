using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSenseCSharp.extensions {
    public static class EnumUtil {
        public static IEnumerable<TEnum> GetAllValues<TEnum>()
            where TEnum : struct, IConvertible, IComparable, IFormattable {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }
        public static TEnum TryParseOrDefault<TEnum>(string value, bool ignoreCase, TEnum defaultVal)
        where TEnum : struct, IConvertible, IComparable, IFormattable {
            TEnum current;
            if (!Enum.TryParse(value, ignoreCase, out current)) {
                current = defaultVal;
            }
            return current;
        }
    }
}
