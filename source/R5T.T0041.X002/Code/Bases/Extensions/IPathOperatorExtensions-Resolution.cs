using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0041;

using Instances = R5T.T0041.X002.Instances;


namespace System
{
    public static partial class IPathOperatorExtensions
    {
        public static string EnsureResolved(this IPathOperator _,
            string pathSegment)
        {
            var isResolved = _.IsResolved(pathSegment);
            if(isResolved)
            {
                return pathSegment;
            }
            else
            {
                var output = _.ResolvePath(pathSegment);
                return output;
            }
        }

        public static string ResolvePath(this IPathOperator _,
            string pathSegment)
        {
            // Do not check whether path is resolved. Do the resolution operations just the same, and give callers the responsibility to first check whether a path is already resolved (perhaps via an EnsurePathIsResolved() extension).

            // Get path parts.
            var pathParts = _.GetPathParts(pathSegment);

            // Now choose path parts, taking into account current directory and parent directory names.
            var pathPartsStack = new Stack<string>();

            foreach (var pathPart in pathParts)
            {
                if (Instances.DirectoryName.IsCurrentDirectory(pathPart))
                {
                    // Do nothing.
                    continue;
                }

                if(Instances.DirectoryName.IsParentDirectory(pathPart))
                {
                    // Throw away the top path part.
                    pathPartsStack.PopOkIfEmpty();

                    continue;
                }

                // Else, add the path part.
                pathPartsStack.Push(pathPart);
            }

            // Recombine chosen path parts, using the directory separator of the input path.
            // Element order must be reversed since stack gives out elements in popped (LIFO) order.
            var chosenPathPartsInOrder = pathPartsStack.Reverse().ToArray();

            // Get the directory separator.
            var directorySeparator = _.DetectDirectorySeparator(pathSegment);

            var combinedPath = _.CombineWithoutModification(chosenPathPartsInOrder, directorySeparator);

            var output = _.MatchEndings(
                combinedPath,
                pathSegment);

            return output;
        }
    }
}
