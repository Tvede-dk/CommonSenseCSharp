using System;
using System.Text;

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
    public static string ReplaceAt(this string str, int index, int length, string replace) {
        return str.Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replace ?? "");
    }

    public static int IntValue(this string str) {
        var temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }

    public static StringBuilder Append(this StringBuilder builder, params string[] items) {
        items.FlatForeach(builder.Append);
        return builder;
    }

}
