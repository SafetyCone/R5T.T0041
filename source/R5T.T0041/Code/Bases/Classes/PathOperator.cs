using System;


namespace R5T.T0041
{
    /// <summary>
    /// Empty implementation as base for extension methods.
    /// </summary>
    public class PathOperator : IPathOperator
    {
        #region Static

        public static IPathOperator Instance { get; } = new PathOperator();

        #endregion
    }
}