﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


}