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
        /// <summary>
        /// Chooses <see cref="CombineWithoutModification(IPathOperator, string, string, char)"/> as the default.
        /// </summary>
        public static string Combine(this IPathOperator _,
            string prefixPathPart,
            char directorySeparator,
            string suffixPathPart)
        {
            var output = _.CombineWithoutModification(
                prefixPathPart,
                suffixPathPart,
                directorySeparator);

            return output;
        }

        /// <summary>
        /// Uses <see cref="DetectDirectorySeparatorOrStandard(IPathOperator, string)"/> to get a directory separator, and chooses <see cref="CombineEnsuringDirectorySeparator(IPathOperator, string, char, string)"/> as the default.
        /// </summary>
        public static string Combine(this IPathOperator _,
            string prefixPathPart,
            string postfixPathPart)
        {
            var directorySeparator = _.DetectDirectorySeparatorOrStandard(prefixPathPart);

            var output = _.CombineEnsuringDirectorySeparator(prefixPathPart, directorySeparator, postfixPathPart);
            return output;
        }

        public static string CombineEnsuringDirectorySeparator(this IPathOperator _,
            string prefixPath,
            char directorySeparator,
            string suffixPath)
        {
            var possiblyMixedDirectorySeparatorPath = $"{prefixPath}{directorySeparator}{suffixPath}";

            var output = _.EnsureDirectorySeparator(possiblyMixedDirectorySeparatorPath, directorySeparator);
            return output;
        }

        public static string CombineWithoutModification(this IPathOperator _,
            string[] pathSegments,
            char directorySeparator)
        {
            var output = Instances.StringOperator.Join(directorySeparator, pathSegments);
            return output;
        }

        public static string CombineWithoutModification(this IPathOperator _,
            string prefixPath,
            string suffixPath,
            char directorySeparator)
        {
            var output = $"{prefixPath}{directorySeparator}{suffixPath}";
            return output;
        }

        public static string CombineWithoutModification(this IPathOperator _,
            string prefixPath,
            string suffixPath)
        {
            var output = $"{prefixPath}{suffixPath}";
            return output;
        }
    }
}
