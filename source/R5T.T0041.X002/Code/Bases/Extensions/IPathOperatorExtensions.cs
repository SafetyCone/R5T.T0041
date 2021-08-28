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

        public static string CombineWithoutModification(this IPathOperator _,
            string prefixPath,
            char directorySeparator,
            string suffixPath)
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

        public static string AppendDirectoryNameToDirectoryPath(this IPathOperator _,
            string directoryPath,
            string directoryName)
        {
            var directoryIndicatedDirectoryPath = _.EnsureIsDirectoryIndicated(directoryPath);
            var notRelativeDirectoryName = _.EnsureIsNotRelativeIndicated(directoryName);

            var output = _.CombineWithoutModification(directoryIndicatedDirectoryPath, notRelativeDirectoryName);
            return output;
        }

        public static string EnsureIsNotRelativeIndicated(this IPathOperator _,
            string path)
        {
            var isRelativeIndicated = _.IsRelativeIndicated(path);
            if(isRelativeIndicated)
            {
                var output = path.ExceptFirst();
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
                var directorySeparator = _.DetectDirectorySeparatorOrDefaultCharacter(path);

                var output = path + directorySeparator;
                return output;
            }
        }

        public static string GetFileNameForFilePath(this IPathOperator _,
            string filePath)
        {
            var filePathTokens = filePath.Split(Instances.DirectorySeparator.BothCharacters());

            var fileName = filePathTokens.Last();
            return fileName;
        }

        public static string GetFileNameStemForFilePath(this IPathOperator _,
            string filePath)
        {
            var fileName = _.GetFileNameForFilePath(filePath);

            var fileNameStem = Instances.FileNameOperator.GetFileNameStemFromFileName(fileName);
            return fileNameStem;
        }

        public static string GetFilePath(this IPathOperator _,
            string directoryPath,
            string fileName)
        {
            var directorySeparator = _.DetectDirectorySeparatorOrDefaultCharacter(directoryPath);

            var filePath = _.CombineWithoutModification(directoryPath, directorySeparator, fileName);
            return filePath;
        }

        public static bool IsDirectoryIndicated(this IPathOperator _,
            string path)
        {
            var lastCharacter = path.Last();

            var output = Instances.DirectorySeparator.IsDirectorySeparator(lastCharacter);
            return output;
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

        public static char DetectDirectorySeparatorOrDefaultCharacter(this IPathOperator _,
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

        public static bool IsPathSubPathOfParentPath(this IPathOperator _,
            string path,
            string parentPath)
        {
            var pathStandardized = _.EnsureStandardDirectorySeparator(path);
            var parentPathStandardized = _.EnsureStandardDirectorySeparator(parentPath);

            var output = Instances.StringOperator.BeginsWith(
                pathStandardized,
                parentPathStandardized);

            return output;
        }
        
        public static bool IsFileInDirectoryOrSubDirectories(this IPathOperator _,
            string filePath,
            string directoryPath)
        {
            var output = _.IsPathSubPathOfParentPath(filePath, directoryPath);
            return output;
        }

        public static string GetDirectoryPathOfFilePath(this IPathOperator _,
            string filePath)
        {
            var lastIndex = filePath.LastIndexOfAny(Instances.DirectorySeparator.EitherCharacter());

            var indexFound = StringHelper.IsFound(lastIndex);
            if (!indexFound)
            {
                throw new Exception("Directory separator not found.");
            }

            var output = filePath.BeginningByIndex(lastIndex);
            return output;
        }
    }
}
