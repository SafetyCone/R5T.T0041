using System;

using R5T.Magyar.T002;

using R5T.T0042;


namespace R5T.T0041.X002
{
    public static class Instances
    {
        public static IDirectorySeparator DirectorySeparator { get; } = T0041.DirectorySeparator.Instance;
        public static IExceptionGenerator ExceptionGenerator { get; } = Magyar.T002.ExceptionGenerator.Instance;
        public static IExceptionMessageGenerator ExceptionMessageGenerator { get; } = Magyar.T002.ExceptionMessageGenerator.Instance;
        public static IFileExtensionSeparator FileExtensionSeparator { get; } = T0041.FileExtensionSeparator.Instance;
        public static IFileNameOperator FileNameOperator { get; } = T0041.FileNameOperator.Instance;
        public static IStringOperator StringOperator { get; } = T0042.StringOperator.Instance;
    }
}
