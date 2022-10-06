using System;


namespace R5T.T0041
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class DirectorySeparator : IDirectorySeparator
    {
        #region Static

        public static IDirectorySeparator Instance { get; } = new DirectorySeparator();

        #endregion
    }
}