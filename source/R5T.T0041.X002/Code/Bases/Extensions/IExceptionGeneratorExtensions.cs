using System;

using R5T.Magyar.T002;

using Instances = R5T.T0041.X002.Instances;


namespace System
{
    public static class IExceptionGeneratorExtensions
    {
        public static ArgumentException PathContainsInvalidPathCharacters(this IExceptionGenerator _,
            string path,
            char[] invalidPathCharactersInPath,
            string parameterName)
        {
            var message = Instances.ExceptionMessageGenerator.PathContainsInvalidPathCharacters(
                path,
                invalidPathCharactersInPath);

            var output = new ArgumentException(message, parameterName);
            return output;
        }
    }
}
