using System;

using R5T.Magyar;


namespace R5T.T0041.X001
{
    public static class DirectorySeparators
    {
        public static string Invalid => Strings.Underscore;
        public static char InvalidCharacter => Characters.Underscore;
        public static string NonWindows => Strings.Slash;
        public static char NonWindowsCharacter => Characters.Slash;
        public static string Windows => Strings.Backslash;
        public static char WindowsCharacter => Characters.Backslash;

        /// <summary>
        /// Chooses the <see cref="DirectorySeparators.Windows"/> value as the standard, for use when standardization is desired.
        /// </summary>
        public static string Standard => DirectorySeparators.Windows;
        public static char StandardCharacter => DirectorySeparators.WindowsCharacter;

        /// <summary>
        /// Chooses the <see cref="DirectorySeparators.Windows"/> value as the default, for use when none is available.
        /// </summary>
        public static string Default => DirectorySeparators.Windows;
        public static char DefaultCharacter => DirectorySeparators.WindowsCharacter;
    }
}