using System;
using System.Text;
using JetBrains.Annotations;

public static class StringBuilderExtensions {
    /// <summary>
    /// chops off the request amount of chars, and if not possible (eg the stringbuilder does not have that many chars) it returns an empty string.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="chopCharsOffAtEnd"></param>
    /// <returns></returns>
    public static string ToStringSafeEnd(this StringBuilder builder, int chopCharsOffAtEnd) {
        if (builder.Length > chopCharsOffAtEnd) {
            return builder.ToString(0, builder.Length - chopCharsOffAtEnd);
        } else {
            return "";
        }
    }

    //// str - the source string
    //// index- the start location to replace at (0-based)
    //// length - the number of characters to be removed before inserting
    //// replace - the string that is replacing characters
    public static string ReplaceAt(this string str, int index, int length, string replace) => str.Remove(index, Math.Min(length, str.Length - index))
            .Insert(index, replace ?? "");

    public static string AppendIf(this string str, string otherStr, bool shouldAppend) {
        if (shouldAppend) {
            return str + otherStr;
        }
        return str;
    }

    public static int IntValue([NotNull] this string str, int otherWise) => int.TryParse(str, out var temp) ? temp : otherWise;

    public static float FloatValue([NotNull] this string str, float otherWise) => float.TryParse(str, out var temp) ? temp : otherWise;

    public static long LongValue([NotNull] this string str, long otherWise) => long.TryParse(str, out var temp) ? temp : otherWise;

    public static double IntValue([NotNull] this string str, double otherWise) => double.TryParse(str, out var temp) ? temp : otherWise;

    [NotNull]
    public static string Repeate([NotNull] this string str, int numberOfTimesToRepeate) {
        if (numberOfTimesToRepeate < 10 && str.Length < 50) { //simple heuristic, only use stringbuilder when nessary. might need tweeaking.
            var result = str;
            numberOfTimesToRepeate.PerformEachTime(_ => result += str);
            return result;
        }
        //it might be now.
        var sb = new StringBuilder(str.Length * numberOfTimesToRepeate);
        numberOfTimesToRepeate.PerformEachTime(_ => sb.Append(str));
        return sb.ToString();
    }

    [NotNull]
    public static StringBuilder Append([NotNull] this StringBuilder builder, [NotNull] params string[] items) {
        items.FlatForeach(builder.Append);
        return builder;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strs"></param>
    /// <returns>true if all the strings are contained.</returns>
    public static bool ContainsAll([NotNull] this string str, [NotNull] params string[] strs) =>
        //TODO use a better impl, as this is pretty slow.
        strs.FlatMap(str.Contains).FlatFlattern(true, (x, y) => x &= y);
}