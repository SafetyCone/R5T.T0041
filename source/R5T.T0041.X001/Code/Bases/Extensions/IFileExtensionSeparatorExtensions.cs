using System;

using R5T.T0041;
using R5T.T0041.X001;


namespace System
{
    public static class IFileExtensionSeparatorExtensions
    {
        public static string Default(this IFileExtensionSeparator _)
        {
            return FileExtensionSeparators.Default;
        }

        public static char DefaultCharacter(this IFileExtensionSeparator _)
        {
            return FileExtensionSeparators.DefaultCharacter;
        }
    }
}
