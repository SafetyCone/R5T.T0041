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
        public static void VerifyPath_NotNullAndNotEmpty(this IPathOperator _,
            string path)
        {
            var isValid = _.IsValid_NotNullAndNotEmpty(path);
            if(!isValid)
            {
                throw Instances.ExceptionGenerator.PathWasNullOrEmpty();
            }
        }

        /// <summary>
        /// The primary method to call when wishing to check that a path is valid.
        /// </summary>
        public static void VerifyIsValid(this IPathOperator _,
            string path)
        {
            _.VerifyPath_NotNullAndNotEmpty(path);
        }
    }
}
