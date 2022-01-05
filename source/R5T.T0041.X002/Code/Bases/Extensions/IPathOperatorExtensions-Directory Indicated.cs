using System;

using R5T.T0041;

using Instances = R5T.T0041.X002.Instances;


namespace System
{
    public static partial class IPathOperatorExtensions
    {
        public static string EnsureIsDirectoryIndicated(this IPathOperator _,
            string path)
        {
            var pathIsDirectoryIndicated = _.IsDirectoryIndicated(path);
            if (pathIsDirectoryIndicated)
            {
                return path;
            }
            else
            {
                var output = _.MakeDirectoryIndicated(path);
                return output;
            }
        }

        public static string EnsureIsDirectoryIndicated(this IPathOperator _,
            string path,
            bool directoryIndicated)
        {
            var output = directoryIndicated
                ? _.EnsureIsDirectoryIndicated(path)
                : _.EnsureIsNotDirectoryIndicated(path)
                ;

            return output;
        }

        public static string EnsureIsNotDirectoryIndicated(this IPathOperator _,
            string path)
        {
            var pathIsDirectoryIndicated = _.IsDirectoryIndicated(path);
            if (pathIsDirectoryIndicated)
            {
                var output = _.MakeDirectoryNotIndicated(path);
                return output;
            }
            else
            {
                return path;
            }
        }

        public static string MakeDirectoryIndicated(this IPathOperator _,
            string path,
            char directorySeparator)
        {
            var output = path + directorySeparator;
            return output;
        }

        public static string MakeDirectoryIndicated(this IPathOperator _,
            string path)
        {
            var directorySeparator = _.DetectDirectorySeparator(path);

            var output = _.MakeDirectoryIndicated(path,
                directorySeparator);

            return output;
        }

        public static string MakeDirectoryIndicated(this IPathOperator _,
            string path,
            char directorySeparator,
            bool directoryIndicated)
        {
            var output = directoryIndicated
                ? _.MakeDirectoryIndicated(path, directorySeparator)
                : _.MakeDirectoryNotIndicated(path)
                ;

            return output;
        }

        public static string MakeDirectoryIndicated(this IPathOperator _,
            string path,
            bool directoryIndicated)
        {
            var directorySeparator = _.DetectDirectorySeparator(path);

            var output = _.MakeDirectoryIndicated(
                path,
                directorySeparator,
                directoryIndicated);

            return output;
        }

        public static string MakeDirectoryNotIndicated(this IPathOperator _,
            string path)
        {
            var output = Instances.StringOperator.TrimEnd(path,
                Instances.DirectorySeparator.BothCharacters());

            return output;
        }

        public static string MatchDirectoryIndication(this IPathOperator _,
            string path,
            string referencePath)
        {
            var directoryIndicated = _.IsDirectoryIndicated(referencePath);

            var output = _.MakeDirectoryIndicated(path,
                directoryIndicated);

            return output;
        }
    }
}
