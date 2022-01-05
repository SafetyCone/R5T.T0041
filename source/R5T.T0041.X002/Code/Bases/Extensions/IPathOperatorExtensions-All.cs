using System;
using System.Linq;

using R5T.Magyar;

using R5T.T0041;

using Instances = R5T.T0041.X002.Instances;


namespace System
{
    public static partial class IPathOperatorExtensions
    {
        public static string AppendDirectoryRelativePathToDirectoryPath(this IPathOperator _,
            string parentDirectoryPath,
            string childRelativeDirectoryPath)
        {
            var directoryIndicatedDirectoryPath = _.EnsureIsDirectoryIndicated(parentDirectoryPath);
            var notRelativeDirectoryRelativePath = _.EnsureIsNotRelativeIndicated(childRelativeDirectoryPath);

            var possiblyMixedDirectorySeparatorDirectoryPath = _.CombineWithoutModification(directoryIndicatedDirectoryPath, notRelativeDirectoryRelativePath);

            var directorySeparator = _.DetectDirectorySeparatorOrStandard(directoryIndicatedDirectoryPath);

            var output = _.EnsureDirectorySeparator(possiblyMixedDirectorySeparatorDirectoryPath, directorySeparator);
            return output;
        }

        public static string EnsureIsNotRelativeIndicated(this IPathOperator _,
            string path)
        {
            var isRelativeIndicated = _.IsRelativeIndicated(path);
            if(isRelativeIndicated)
            {
                var output = path.ExceptFirstCharacter();
                return output;
            }
            else
            {
                return path;
            }
        }

        public static string GetLastPathToken(this IPathOperator _,
            string path)
        {
            // Remove empty entries in case path is a directory path and is directory-indicated (final character is a directory separator).
            var pathTokens = path.Split(Instances.DirectorySeparator.BothCharacters(), StringSplitOptions.RemoveEmptyEntries);

            var fileName = pathTokens.Last();
            return fileName;
        }

        public static string GetFileNameForFilePath(this IPathOperator _,
            string filePath)
        {
            var output = _.GetLastPathToken(filePath);
            return output;
        }

        public static string GetFileNameStemForFilePath(this IPathOperator _,
            string filePath)
        {
            var fileName = _.GetFileNameForFilePath(filePath);

            var fileNameStem = Instances.FileNameOperator.GetFileNameStemFromFileName(fileName);
            return fileNameStem;
        }

        public static string GetParentDirectoryPath(this IPathOperator _,
            string path)
        {
            /// Note: cannot use <see cref="System.IO.Path.GetDirectoryName(string?)"/>, since that produces directory paths that are not directory indicated.

            _.VerifyIsValid(path);

            var verifiedResolvedPath = _.EnsureResolved(path);

            // Is the path a file or a directory? At the path level, all we can tell is whether it is directory-indicated (ends with a directory separator), or not.
            var isFile = _.IsFilePath(verifiedResolvedPath);

            var lastIndex = isFile
                // If file, then get all characters up to (and including) the final directory separator.
                ? verifiedResolvedPath.LastIndexOfAny(Instances.DirectorySeparator.EitherCharacter())
                // If a directory, then get all characters up to (and including) the second-to-last directory separator.
                : verifiedResolvedPath.NthLastIndexOfAny(Instances.DirectorySeparator.EitherCharacter(), 2)
                ;

            var indexFound = StringHelper.IsFound(lastIndex);
            if (!indexFound)
            {
                throw new Exception("Directory separator not found.");
            }

            var output = path.BeginningByIndex(lastIndex + 1);
            return output;
        }

        public static string GetFilePath(this IPathOperator _,
            string directoryPath,
            string fileRelativePath)
        {
            var directoryIndicatedDirectoryPath = _.EnsureIsDirectoryIndicated(directoryPath);
            var notRelativeFileRelativePath = _.EnsureIsNotRelativeIndicated(fileRelativePath);

            var possiblyMixedDirectorySeparatorFilePath = _.CombineWithoutModification(directoryIndicatedDirectoryPath, notRelativeFileRelativePath);

            var directorySeparator = _.DetectDirectorySeparatorOrStandard(directoryPath);

            var filePath = _.EnsureDirectorySeparator(possiblyMixedDirectorySeparatorFilePath, directorySeparator);
            return filePath;
        }

        public static bool HasParentDirectory(this IPathOperator _,
            string path)
        {
            // If a path has at least two directory separators, then it has a parent.
            var directorySeparators = Instances.DirectorySeparator.BothCharacters();

            var countOfDirectorySeparators = path
                .Where(character => directorySeparators.Contains(character))
                .Count();

            var hasParentDirectory = countOfDirectorySeparators > 1;
            return hasParentDirectory;
        }

        public static bool IsPathSubPathOfParentPath(this IPathOperator _,
            string path,
            string parentPath)
        {
            var pathParts = _.GetPathParts(path);
            var parentPathParts = _.GetPathParts(parentPath);

            var countOfParentPathParts = parentPathParts.Length;
            var maxParentPathPartIndex = countOfParentPathParts - 1; // Index equals count minus one.

            var isPathInParent = true;
            for (int iPathPart = 0; iPathPart < countOfParentPathParts; iPathPart++)
            {
                var currentPathPartIndex = maxParentPathPartIndex - iPathPart;

                var partPart = pathParts[currentPathPartIndex];
                var parentPathPart = parentPathParts[currentPathPartIndex];

                if(partPart != parentPathPart)
                {
                    isPathInParent = false;
                    break;
                }
            }

            return isPathInParent;
        }

        public static bool IsFileInDirectoryOrSubDirectoriesOfFileDirectory(this IPathOperator _,
            string filePath,
            string parentFilePath)
        {
            var parentDirectoryPath = _.GetDirectoryPathOfFilePath(parentFilePath);

            var output = _.IsFileInDirectoryOrSubDirectories(filePath, parentDirectoryPath);
            return output;
        }


        public static bool IsFileInDirectoryOrSubDirectories(this IPathOperator _,
            string filePath,
            string directoryPath)
        {
            var output = _.IsPathSubPathOfParentPath(filePath, directoryPath);
            return output;
        }

        public static string GetDirectoryPath(this IPathOperator _,
            string parentDirectoryPath,
            string childDirectoryRelativePath)
        {
            var output = _.AppendDirectoryRelativePathToDirectoryPath(parentDirectoryPath, childDirectoryRelativePath);
            return output;
        }

        public static string GetDirectoryPath(this IPathOperator _,
            string parentDirectoryPath,
            params string[] subPathParts)
        {
            var output = parentDirectoryPath;
            foreach (var subPathPart in subPathParts)
            {
                output = _.AppendDirectoryRelativePathToDirectoryPath(parentDirectoryPath, subPathPart);
            }

            return output;
        }

        public static string GetDirectoryPathOfFilePath(this IPathOperator _,
            string filePath)
        {
            var output = _.GetParentDirectoryPath(filePath);
            return output;
        }

        public static string GetDirectoryNameOfDirectoryPath(this IPathOperator _,
            string directoryPath)
        {
            var output = _.GetLastPathToken(directoryPath);
            return output;
        }

        public static string MatchEndings(this IPathOperator _,
            string path,
            string referencePath)
        {
            var output = path;

            output = _.MatchRelativeIndication(output, referencePath);
            output = _.MatchDirectoryIndication(output, referencePath);

            return output;
        }
    }
}
