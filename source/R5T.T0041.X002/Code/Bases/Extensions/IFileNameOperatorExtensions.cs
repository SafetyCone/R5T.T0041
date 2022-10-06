using System;

using R5T.T0041;

using Instances = R5T.T0041.X002.Instances;
using Path = System.IO.Path;


namespace System
{
    public static class IFileNameOperatorExtensions
    {
        public static string GetFileExtensionFromFileName(this IFileNameOperator _,
            string fileName)
        {
            var index = fileName.LastIndexOf(Instances.FileExtensionSeparator.DefaultCharacter());

            // Handle file names that might have dots in them.
            var fileNameStem = fileName.Substring(index + 1);
            return fileNameStem;
        }

        public static string GetFileName(this IFileNameOperator _,
            string fileNameStem,
            string fileExtension)
        {
            var output = $"{fileNameStem}{Instances.FileExtensionSeparator.Default()}{fileExtension}";
            return output;
        }

        public static string GetFileNameStemFromFileName(this IFileNameOperator _,
            string fileName)
        {
            var index = fileName.LastIndexOf(Instances.FileExtensionSeparator.DefaultCharacter());

            // Handle file names that might have dots in them.
            var fileNameStem = fileName.Beginning(index);
            return fileNameStem;
        }

        public static char[] GetInvalidFileNameCharacters(this IFileNameOperator _)
        {
            var output = Path.GetInvalidFileNameChars();
            return output;
        }

        public static bool ContainsInvalidFileNameCharacters(this IFileNameOperator _,
            string fileName)
        {
            var invalidFileNameCharacters = _.GetInvalidFileNameCharacters();

            var output = Instances.StringOperator.ContainsAny(fileName, invalidFileNameCharacters);
            return output;
        }
    }
}
