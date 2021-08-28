using System;


namespace R5T.T0041
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class FileExtensionSeparator : IFileExtensionSeparator
    {
        #region Static

        public static FileExtensionSeparator Instance { get; } = new();

        #endregion
    }
}