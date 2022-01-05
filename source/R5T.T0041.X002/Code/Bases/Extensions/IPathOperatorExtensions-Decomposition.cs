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
        public static string[] GetAllPathParts(this IPathOperator _,
            string path)
        {
            var directorySeparators = Instances.DirectorySeparator.BothCharacters();

            // Preserve empty entries even though they don't count during path navigation.
            var output = Instances.StringOperator.Split(path, directorySeparators, StringSplitOptions.None);
            return output;
        }

        public static string[] GetNonEmptyPathParts(this IPathOperator _,
            string path)
        {
            var allPathParts = _.GetAllPathParts(path);

            var output = allPathParts
                .Where(xPathPart => Instances.StringOperator.IsNotNullAndNotEmpty(xPathPart))
                .ToArray();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetNonEmptyPathParts(IPathOperator, string)"/> as the default.
        /// </summary>
        public static string[] GetPathParts(this IPathOperator pathOperator,
            string path)
        {
            var output = pathOperator.GetNonEmptyPathParts(path);
            return output;
        }
    }
}
