﻿using System;
using JetBrains.Annotations;

namespace CommonSenseCSharp.extensions {
    public static class PrimitiveExtensions {
        public static bool OnTrue(this bool theBool, [NotNull] Action onTrue) {
            if (theBool) {
                onTrue();
            }
            return theBool;
        }

        public static bool OnFalse(this bool theBool, [NotNull] Action onFalse) {
            if (!theBool) {
                onFalse();
            }
            return theBool;
        }

        [CanBeNull]
        public static T IfElse<T>(this bool theBool, [NotNull] Func<T> onTrue, [NotNull] Func<T> onFalse) => theBool ? onTrue() : onFalse();

        [NotNull]
        public static T IfElseSafe<T>(this bool theBool, [NotNull] Func<T> onTrue, [NotNull] Func<T> onFalse) => theBool ? onTrue() : onFalse();

        public static void IfElseSafeVoid<T>(this bool theBool,
            [NotNull] T value,
            [NotNull] Action<T> onTrue,
            [NotNull] Action<T> onFalse) {
            if (theBool) {
                onTrue(value);
            } else {
                onFalse(value);
            }
        }

        public static void IfElseSafeVoid<T>(this bool theBool,
            [NotNull] T value,
            [NotNull] Action<T> onTrue,
            [NotNull] Action onFalse) {
            if (theBool) {
                onTrue(value);
            } else {
                onFalse();
            }
        }
    }
}