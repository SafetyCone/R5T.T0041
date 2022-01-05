using System;

using R5T.T0041;

using Instances = R5T.T0041.X002.Instances;


namespace System
{
    public static partial class IPathOperatorExtensions
    {
        public static string EnsureIsRelativeIndicated(this IPathOperator _,
            string path)
        {
            var pathIsRelativeIndicated = _.IsRelativeIndicated(path);
            if (pathIsRelativeIndicated)
            {
                return path;
            }
            else
            {
                var output = _.MakeRelativeIndicated(path);
                return output;
            }
        }

        public static string MakeRelativeIndicated(this IPathOperator _,
            string path,
            char directorySeparator)
        {
            var output = directorySeparator + path;
            return output;
        }

        public static string MakeRelativeIndicated(this IPathOperator _,
            string path)
        {
            var directorySeparator = _.DetectDirectorySeparator(path);

            var output = _.MakeRelativeIndicated(path,
                directorySeparator);

            return output;
        }

        public static string MakeRelativeIndicated(this IPathOperator _,
           string path,
           char directorySeparator,
           bool relativeIndicated)
        {
            var output = relativeIndicated
                ? _.MakeRelativeIndicated(path, directorySeparator)
                : _.MakeRelativeNotIndicated(path)
                ;

            return output;
        }

        public static string MakeRelativeIndicated(this IPathOperator _,
            string path,
            bool relativeIndicated)
        {
            var directorySeparator = _.DetectDirectorySeparator(path);

            var output = _.MakeRelativeIndicated(
                path,
                directorySeparator,
                relativeIndicated);

            return output;
        }

        public static string MakeRelativeNotIndicated(this IPathOperator _,
            string path)
        {
            var output = Instances.StringOperator.TrimStart(path,
                Instances.DirectorySeparator.BothCharacters());

            return output;
        }

        public static string MatchRelativeIndication(this IPathOperator _,
            string path,
            string referencePath)
        {
            var relativeIndicated = _.IsRelativeIndicated(referencePath);

            var output = _.MakeRelativeIndicated(path,
                relativeIndicated);

            return output;
        }
    }
}
