using System;

using R5T.Magyar.T002;


namespace System
{
    public static class IExceptionMessageGeneratorExtensions
    {
        public static string PathContainsInvalidPathCharacters(this IExceptionMessageGenerator _,
            string path,
            char[] invalidPathCharactersInPath)
        {
            var joinedPathInvalidPathCharacters = String.Join(Strings.CommaSeparatedListSpacedSeparator, invalidPathCharactersInPath);

            var output = $"The path contains characters that are invalid for a path. Characters:\n{joinedPathInvalidPathCharacters}\nPath:\n{path}";
            return output;
        }
    }
}
