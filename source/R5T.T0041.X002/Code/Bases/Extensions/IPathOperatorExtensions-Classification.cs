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
        /// Determines whether a path is directory indicated (ends with 
        /// </summary>
        public static bool IsDirectoryIndicated(this IPathOperator _,
            string path)
        {
            _.VerifyIsValid(path);

            var lastCharacter = path.Last();

            var output = Instances.DirectorySeparator.IsDirectorySeparator(lastCharacter);
            return output;
        }

        /// <summary>
        /// Determines whether a path is a directory path.
        /// Note: there is no way to determine with certainty whether a stringly-typed path is a directory path without reference to a file system.
        /// However, this method provides the best possible answer.
        /// </summary>
        public static bool IsDirectoryPath(this IPathOperator _,
            string path)
        {
            var output = _.IsDirectoryIndicated(path);
            return output;
        }

        /// <summary>
        /// Determines whether a path is a file path.
        /// Note: there is no way to determine with certainty whether a stringly-typed path is a file path without reference to a file system.
        /// However, this method provides the best possible answer.
        /// </summary>
        public static bool IsFilePath(this IPathOperator _,
            string path)
        {
            var isDirectoryPath = _.IsDirectoryPath(path);

            // If it's not a directory path, it's a file path.
            var output = !isDirectoryPath;
            return output;
        }

        public static bool IsNullOrEmpty(this IPathOperator _,
            string path)
        {
            var output = Instances.StringOperator.IsNullOrEmpty(path);
            return output;
        }

        public static bool IsRelativeIndicated(this IPathOperator _,
            string path)
        {
            _.VerifyIsValid(path);

            var firstCharacter = path.First();

            var output = Instances.DirectorySeparator.IsDirectorySeparator(firstCharacter);
            return output;
        }

        /// <summary>
        /// Determines whether the path is resolved (no current directory (".") or parent directory ("..") names exist within the path).
        /// </summary>
        public static bool IsResolved(this IPathOperator _,
            string path)
        {
            var isUnresolved = _.IsUnresolved(path);

            var output = !isUnresolved;
            return output;
        }

        /// <summary>
        /// Determines if the path is one of the relative directory names (".", for the current directory, or ".." for the parent directory of the current directory).
        /// </summary>
        public static bool IsRelativeDirectoryName(this IPathOperator _,
            string path)
        {
            var output = false
                || path == Instances.DirectoryName.CurrentDirectory()
                || path == Instances.DirectoryName.ParentDirectory()
                ;

            return output;
        }

        /// <summary>
        /// Determines whether the path is unresolved (any current directory (".") or parent directory ("..") names exist within the path).
        /// </summary>
        public static bool IsUnresolved(this IPathOperator _,
            string path)
        {
            // Is the path simply one of the relative directory names?
            var isRelativeDirectoryName = _.IsRelativeDirectoryName(path);
            if(isRelativeDirectoryName)
            {
                return isRelativeDirectoryName;
            }

            // Since "." certainly, or ".." conceivably, might exist within the path ("." as the file extension separator and ".." perhaps accidentally in a file path), look for these directory names together with either of the directory separator, both before and after the directory name.
            // The relative directory names will not appear alone since that would instead be interpretted as part of a file name or directory name.
            var searchStrings = new[]
            {
                $"{Instances.DirectorySeparator.Windows()}{Instances.DirectoryName.ParentDirectory()}",
                $"{Instances.DirectoryName.ParentDirectory()}{Instances.DirectorySeparator.Windows()}",
                $"{Instances.DirectorySeparator.NonWindows()}{Instances.DirectoryName.CurrentDirectory()}",
                $"{Instances.DirectoryName.CurrentDirectory()}{Instances.DirectorySeparator.NonWindows()}",
            };

            var output = Instances.StringOperator.ContainsAny(path,
                searchStrings);

            return output;
        }

        public static bool IsValid_NotNullAndNotEmpty(this IPathOperator _,
            string path)
        {
            var output = Instances.StringOperator.IsNotNullAndNotEmpty(path);
            return output;
        }
    }
}
