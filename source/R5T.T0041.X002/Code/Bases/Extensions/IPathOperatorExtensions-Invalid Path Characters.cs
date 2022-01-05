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

        public static WasFound<char[]> HasInvalidPathCharacters(this IPathOperator _,
            string path)
        {
            var allInvalidPathCharacters = _.GetInvalidPathCharacters();

            var pathInvalidPathCharacters = allInvalidPathCharacters.Intersect(path).ToArray();

            var output = WasFound.FromArray(pathInvalidPathCharacters);
            return output;
        }

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
    }
}
