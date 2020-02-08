using System;
using System.Collections.Generic;

namespace AnalysisOfKnowledge.MathematicalModel.Processing.Interfaces
{
    /// <summary>
    /// Contains matrix functions
    /// </summary>
    internal interface IAlgebraicMatrixFunctions
    {
        /// <summary>
        /// Map by type <para>matrix</para>^T>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>Transposed matrix with dimension {M, N}</returns>
        TMatrix[,] TransposedMatrix<TMatrix>(TMatrix[,] matrix, Func<TMatrix[,], TMatrix[,]> functor = null)
            where TMatrix : ICollection<TMatrix>, IEnumerable<TMatrix>;

        /// <summary>
        ///  /// Map by type <para>matrix</para>^-1>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>Transposed matrix with dimension {M, N}</returns>
        TMatrix[,] InverseMatrix<TMatrix>(TMatrix[,] matrix, Func<TMatrix[,], TMatrix[,]> functor = null)
            where TMatrix : ICollection<TMatrix>, IEnumerable<TMatrix>;

        /// <summary>
        /// Map by type Matrix * Matrix>
        /// </summary>
        /// <param name="firstMatrix">Matrix 1 with dimension {M, N}</param>
        /// <param name="secondMatrix">Matrix 2 with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <typeparam name="TMatrix">The resulting multiplication of two matrices</typeparam>
        /// <returns>The resulting product of two matrices</returns>
        TMatrix[,] MatrixMultiplication<TMatrix>(TMatrix[,] firstMatrix, TMatrix[,] secondMatrix,
            Func<TMatrix[,], TMatrix[,]> functor = null)
            where TMatrix : ICollection<TMatrix>, IEnumerable<TMatrix>;
    }
}