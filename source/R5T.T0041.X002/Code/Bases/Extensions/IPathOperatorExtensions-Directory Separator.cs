using System;
using System.Linq;

using R5T.Magyar;

using R5T.T0041;

using Path = System.IO.Path;

using Instances = R5T.T0041.X002.Instances;


namespace System
{
    public static partial class IPathOperatorExtensions
    {
        public static char DetectDirectorySeparatorOrDefault(this IPathOperator _,
            string pathSegment)
        {
            _.TryDetectDirectorySeparator(pathSegment, out char output, Instances.DirectorySeparator.DefaultCharacter());

            return output;
        }

        /// <summary>
        /// Chooses <see cref="DetectDirectorySeparatorOrDefault(IPathOperator, string)"/> as the default.
        /// </summary>
        public static char DetectDirectorySeparator(this IPathOperator _,
            string pathSegment)
        {
            var output = _.DetectDirectorySeparatorOrDefault(pathSegment);
            return output;
        }

        public static char DetectDirectorySeparatorOrStandard(this IPathOperator _,
            string pathSegment)
        {
            _.TryDetectDirectorySeparator(pathSegment, out char output, Instances.DirectorySeparator.StandardCharacter());

            return output;
        }

        /// <summary>
        /// Selects the standard directory separator.
        /// </summary>
        public static string EnsureStandardDirectorySeparator(this IPathOperator _,
            string pathSegment)
        {
            var output = _.EnsureDirectorySeparator(pathSegment,
                Instances.DirectorySeparator.StandardCharacter());

            return output;
        }

        public static string EnsureWindowsDirectorySeparator(this IPathOperator _,
            string pathSegment)
        {
            var output = _.EnsureDirectorySeparator(pathSegment,
                Instances.DirectorySeparator.WindowsCharacter());

            return output;
        }

        public static string EnsureNonWindowsDirectorySeparator(this IPathOperator _,
            string pathSegment)
        {
            var output = _.EnsureDirectorySeparator(pathSegment,
                Instances.DirectorySeparator.NonWindowsCharacter());

            return output;
        }

        public static string EnsureDirectorySeparator(this IPathOperator _,
            string pathSegment,
            char directorySeparator)
        {
            var otherDirectorySeparator = Instances.DirectorySeparator.GetOtherCharacter(directorySeparator);

            var output = pathSegment.Replace(
                otherDirectorySeparator,
                directorySeparator);

            return output;
        }

        public static WasFound<char> HasDirectorySeparatorCharacter(this IPathOperator _,
            string pathSegment)
        {
            var exists = _.TryDetectDirectorySeparatorOrDefault(pathSegment, out var directorySeparator);

            var output = WasFound.From(exists, directorySeparator);
            return output;
        }

        /// <summary>
        /// Attempts to detect the directory separator (Windows or non-Windows) used within a path segment.
        /// Returns true if a directory separator can be detected, false otherwise.
        /// If no default directory separator is detected, the output <paramref name="directorySeparator"/> is set to the <see cref="R5T.T0041.X001.DirectorySeparators.Default"/> value.
        /// </summary>
        public static bool TryDetectDirectorySeparatorOrDefault(this IPathOperator _,
            string pathSegment, out char directorySeparator)
        {
            var output = _.TryDetectDirectorySeparator(pathSegment, out directorySeparator, Instances.DirectorySeparator.DefaultCharacter());
            return output;
        }

        /// <summary>
        /// Attempts to detect the directory separator (Windows or non-Windows) used within a path segment.
        /// Returns true if the a directory separator can be detected, and sets the output <paramref name="directorySeparator"/> to the detected value.
        /// Returns false if a directory separator cannot be detected, and sets the output <paramref name="directorySeparator"/> to the provided <paramref name="defaultDirectorySeparator"/> value.
        /// Returns true if both (mixed) directory separators are detected, and sets the sets the output <paramref name="directorySeparator"/> to the dominant value.
        /// A path segment might have both Windows and non-Windows directory separators. Whichever directory separator occurs first in the path segment (thus, closer to the root) is dominant, and is returned as the path segment's directory separator.
        /// </summary>
        public static bool TryDetectDirectorySeparator(this IPathOperator _,
            string pathSegment, out char directorySeparator, char defaultDirectorySeparator)
        {
            var firstIndexOfDirectorySeparator = pathSegment.IndexOfAny(Instances.DirectorySeparator.EitherCharacter());

            var exists = StringHelper.IsFound(firstIndexOfDirectorySeparator);

            directorySeparator = exists
                ? pathSegment[firstIndexOfDirectorySeparator]
                : defaultDirectorySeparator
                ;

            return exists;
        }

        /// <summary>
        /// Attempts to detect the directory separator (Windows or non-Windows) used within a path segment.
        /// Returns true if the a directory separator can be detected, and sets the output <paramref name="directorySeparator"/> to the detected value.
        /// Returns false if a directory separator cannot be detected, and sets the output <paramref name="directorySeparator"/> to the provided <paramref name="defaultDirectorySeparator"/> value.
        /// Returns true if both (mixed) directory separators are detected, and sets the sets the output <paramref name="directorySeparator"/> to the dominant value.
        /// A path segment might have both Windows and non-Windows directory separators. Whichever directory separator occurs first in the path segment (thus, closer to the root) is dominant, and is returned as the path segment's directory separator.
        /// </summary>
        public static bool TryDetectDirectorySeparator(this IPathOperator _,
            string pathSegment, out string directorySeparator, char defaultDirectorySeparator)
        {
            var output = _.TryDetectDirectorySeparator(pathSegment, out char directorySeparatorCharacter, defaultDirectorySeparator);

            directorySeparator = directorySeparatorCharacter.ToString();

            return output;
        }
    }
}
