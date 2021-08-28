using System;


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
    }
}