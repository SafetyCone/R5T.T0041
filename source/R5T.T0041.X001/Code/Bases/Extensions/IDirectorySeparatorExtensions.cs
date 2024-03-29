﻿using System;
using System.Linq;

using R5T.T0041;
using R5T.T0041.X001;


namespace System
{
    public static class IDirectorySeparatorExtensions
    {
        /// <summary>
        /// QoL overload for <see cref="EitherCharacter(IDirectorySeparator)"/>.
        /// </summary>
        public static char[] BothCharacters(this IDirectorySeparator _)
        {
            var output = _.EitherCharacter();
            return output;
        }

        /// <summary>
        /// Defines the Windows directory separator as the Default.
        /// </summary>
        public static string Default(this IDirectorySeparator _)
        {
            return DirectorySeparators.Default;
        }

        /// <summary>
        /// Defines the Windows directory separator as the Default.
        /// </summary>
        public static char DefaultCharacter(this IDirectorySeparator _)
        {
            return DirectorySeparators.DefaultCharacter;
        }

        public static string[] Either(this IDirectorySeparator _)
        {
            var output = new[]
            {
                _.NonWindows(),
                _.Windows()
            };

            return output;
        }

        public static char[] EitherCharacter(this IDirectorySeparator _)
        {
            var output = new[]
            {
                _.NonWindowsCharacter(),
                _.WindowsCharacter()
            };

            return output;
        }

        public static string GetOther(this IDirectorySeparator _,
            string directorySeparator)
        {
            var output = directorySeparator == _.Windows()
                ? _.NonWindows()
                : _.Windows()
                ;

            return output;
        }

        public static char GetOtherCharacter(this IDirectorySeparator _,
            char directorySeparator)
        {
            var output = directorySeparator == _.WindowsCharacter()
                ? _.NonWindowsCharacter()
                : _.WindowsCharacter()
                ;

            return output;
        }

        public static string Invalid(this IDirectorySeparator _)
        {
            return DirectorySeparators.Invalid;
        }

        public static char InvalidCharacter(this IDirectorySeparator _)
        {
            return DirectorySeparators.InvalidCharacter;
        }

        public static bool IsDirectorySeparator(this IDirectorySeparator _,
            char character)
        {
            var output = _.BothCharacters().Contains(character);
            return output;
        }

        public static string NonWindows(this IDirectorySeparator _)
        {
            return DirectorySeparators.NonWindows;
        }

        public static char NonWindowsCharacter(this IDirectorySeparator _)
        {
            return DirectorySeparators.NonWindowsCharacter;
        }

        /// <summary>
        /// Defines the Windows directory separator as the standard.
        /// </summary>
        public static string Standard(this IDirectorySeparator _)
        {
            return DirectorySeparators.Standard;
        }

        /// <summary>
        /// Defines the Windows directory separator as the standard.
        /// </summary>
        public static char StandardCharacter(this IDirectorySeparator _)
        {
            return DirectorySeparators.StandardCharacter;
        }

        public static string Windows(this IDirectorySeparator _)
        {
            return DirectorySeparators.Windows;
        }

        public static char WindowsCharacter(this IDirectorySeparator _)
        {
            return DirectorySeparators.WindowsCharacter;
        }
    }
}