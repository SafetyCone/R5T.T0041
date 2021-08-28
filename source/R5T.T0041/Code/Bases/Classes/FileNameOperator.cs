using System;


namespace R5T.T0041
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class FileNameOperator : IFileNameOperator
    {
        #region Static

        public static FileNameOperator Instance { get; } = new();

        #endregion
    }
}