using System;
using System.Linq;

using R5T.Magyar;

using R5T.T0041;

using Instances = R5T.T0041.X002.Instances;
using Path = System.IO.Path;


namespace System
{
    public static class IPathOperatorExtensions
    {
        public static void VerifyNoInvalidPathCharacters(this IPathOperator _,
            string path)
        {
            var hasInvalidPathCharacters = _.HasInvalidPathCharacters(path);
            if (hasInvalidPathCharacters)
            {
                throw Instances.ExceptionGenerator.PathContainsInvalidPathCharacters(
                    path,
                    hasInvalidPathCharacters.Result,
                    nameof(path));
            }
        }

        public static WasFound<char[]> HasInvalidPathCharacters(this IPathOperator _,
            string path)
        {
            var allInvalidPathCharacters = _.GetInvalidPathCharacters();

            var pathInvalidPathCharacters = allInvalidPathCharacters.Intersect(path).ToArray();

            var output = WasFound.FromArray(pathInvalidPathCharacters);
            return output;
        }

        public static bool ContainsInvalidPathCharacters(this IPathOperator _,
            string path)
        {
            var invalidPathCharacters = _.GetInvalidPathCharacters();

            var output = Instances.StringOperator.ContainsAny(path, invalidPathCharacters);
            return output;
        }

        public static char[] GetInvalidPathCharacters(this IPathOperator _)
        {
            var output = Path.GetInvalidPathChars();
            return output;
        }

        /// <summary>
        /// Chooses <see cref="CombineWithoutModification(IPathOperator, string, char, string)"/> as the default.
        /// </summary>
        public static string Combine(this IPathOperator _,
            string prefixPathPart,
            char directorySeparator,
            string suffixPathPart)
        {
            var output = _.CombineWithoutModification(
                prefixPathPart,
                directorySeparator,
                suffixPathPart);

            return output;
        }

        /// <summary>
        /// Uses <see cref="DetectDirectorySeparatorOrStandardCharacter(IPathOperator, string)"/> to get a directory separator, and chooses <see cref="CombineEnsuringDirectorySeparator(IPathOperator, string, char, string)"/> as the default.
        /// </summary>
        public static string Combine(this IPathOperator _,
            string prefixPathPart,
            string postfixPathPart)
        {
            var directorySeparator = _.DetectDirectorySeparatorOrStandardCharacter(prefixPathPart);

            var output = _.CombineEnsuringDirectorySeparator(prefixPathPart, directorySeparator, postfixPathPart);
            return output;
        }

        public static string CombineWithoutModification(this IPathOperator _,
            string prefixPath,
            char directorySeparator,
            string suffixPath)
        {
            var output = $"{prefixPath}{directorySeparator}{suffixPath}";
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
            string prefixPath,
            string suffixPath)
        {
            var output = $"{prefixPath}{suffixPath}";
            return output;
        }

        public static string AppendDirectoryRelativePathToDirectoryPath(this IPathOperator _,
            string parentDirectoryPath,
            string childRelativeDirectoryPath)
        {
            var directoryIndicatedDirectoryPath = _.EnsureIsDirectoryIndicated(parentDirectoryPath);
            var notRelativeDirectoryRelativePath = _.EnsureIsNotRelativeIndicated(childRelativeDirectoryPath);

            var possiblyMixedDirectorySeparatorDirectoryPath = _.CombineWithoutModification(directoryIndicatedDirectoryPath, notRelativeDirectoryRelativePath);

            var directorySeparator = _.DetectDirectorySeparatorOrStandardCharacter(directoryIndicatedDirectoryPath);

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

        public static bool IsRelativeIndicated(this IPathOperator _,
            string path)
        {
            var firstCharacter = path.First();

            var output = Instances.DirectorySeparator.IsDirectorySeparator(firstCharacter);
            return output;
        }

        public static string EnsureIsDirectoryIndicated(this IPathOperator _,
            string path)
        {
            var pathIsDirectoryIndicated = _.IsDirectoryIndicated(path);
            if(pathIsDirectoryIndicated)
            {
                return path;
            }
            else
            {
                var directorySeparator = _.DetectDirectorySeparatorOrStandardCharacter(path);

                var output = path + directorySeparator;
                return output;
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
            var lastIndex = path.LastIndexOfAny(Instances.DirectorySeparator.EitherCharacter());

            var indexFound = StringHelper.IsFound(lastIndex);
            if (!indexFound)
            {
                throw new Exception("Directory separator not found.");
            }

            var output = path.BeginningByIndex(lastIndex);
            return output;
        }

        public static string GetFilePath(this IPathOperator _,
            string directoryPath,
            string fileRelativePath)
        {
            var directoryIndicatedDirectoryPath = _.EnsureIsDirectoryIndicated(directoryPath);
            var notRelativeFileRelativePath = _.EnsureIsNotRelativeIndicated(fileRelativePath);

            var possiblyMixedDirectorySeparatorFilePath = _.CombineWithoutModification(directoryIndicatedDirectoryPath, notRelativeFileRelativePath);

            var directorySeparator = _.DetectDirectorySeparatorOrStandardCharacter(directoryPath);

            var filePath = _.EnsureDirectorySeparator(possiblyMixedDirectorySeparatorFilePath, directorySeparator);
            return filePath;
        }

        public static bool IsDirectoryIndicated(this IPathOperator _,
            string path)
        {
            var lastCharacter = path.Last();

            var output = Instances.DirectorySeparator.IsDirectorySeparator(lastCharacter);
            return output;
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

        public static WasFound<char> HasDirectorySeparatorCharacter(this IPathOperator _,
            string path)
        {
            var firstIndexOfDirectorySeparator = path.IndexOfAny(Instances.DirectorySeparator.EitherCharacter());

            var exists = StringHelper.IsFound(firstIndexOfDirectorySeparator);

            var directorySeparator = exists
                ? path[firstIndexOfDirectorySeparator]
                : Instances.DirectorySeparator.InvalidCharacter()
                ;

            var output = WasFound.From(exists, directorySeparator);
            return output;
        }

        public static char DetectDirectorySeparatorOrStandardCharacter(this IPathOperator _,
            string path)
        {
            var wasFound = _.HasDirectorySeparatorCharacter(path);

            var output = wasFound
                ? wasFound.Result
                : Instances.DirectorySeparator.StandardCharacter()
                ;

            return output;
        }

        /// <summary>
        /// Selects Windows as the standard directory separator.
        /// </summary>
        public static string EnsureStandardDirectorySeparator(this IPathOperator _,
            string path)
        {
            var output = _.EnsureWindowsDirectorySeparator(path);
            return output;
        }

        public static string EnsureWindowsDirectorySeparator(this IPathOperator _,
            string path)
        {
            var output = path.Replace(
                Instances.DirectorySeparator.NonWindowsCharacter(),
                Instances.DirectorySeparator.WindowsCharacter());

            return output;
        }

        public static string EnsureNonWindowsDirectorySeparator(this IPathOperator _,
            string path)
        {
            var output = path.Replace(
                Instances.DirectorySeparator.WindowsCharacter(),
                Instances.DirectorySeparator.NonWindowsCharacter());

            return output;
        }

        public static string EnsureDirectorySeparator(this IPathOperator _,
            string path,
            char directorySeparator)
        {
            var otherDirectorySeparator = Instances.DirectorySeparator.GetOtherCharacter(directorySeparator);

            var output = path.Replace(
                otherDirectorySeparator,
                directorySeparator);

            return output;
        }

        public static string[] GetPathParts(this IPathOperator _,
            string path)
        {
            var output = path.Split(Instances.DirectorySeparator.BothCharacters(), StringSplitOptions.RemoveEmptyEntries);
            return output;
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
    }
}
