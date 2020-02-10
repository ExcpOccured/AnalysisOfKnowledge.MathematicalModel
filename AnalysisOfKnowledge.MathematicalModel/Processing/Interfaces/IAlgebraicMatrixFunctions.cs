using System;

namespace AnalysisOfKnowledge.MathematicalModel.Processing.Interfaces
{
    /// <summary>
    /// Contains matrix functions
    /// </summary>
    public interface IAlgebraicMatrixFunctions
    {
        /// <summary>
        /// Map by type <para>matrix</para>^T>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>Transposed matrix with dimension {M, N}</returns>
        double[,] TransposedMatrix(ref double[,] matrix, Func<double[,], double[,]> functor = null);

        /// <summary>
        ///  /// Map by type <para>matrix</para>^-1>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>Transposed matrix with dimension {M, N}</returns>
        double[,] InverseMatrix(ref double[,] matrix, Func<double[,], double[,]> functor = null);

        /// <summary>
        /// Map by type Matrix * Matrix>
        /// </summary>
        /// <param name="firstMatrix">Matrix 1 with dimension {M, N}</param>
        /// <param name="secondMatrix">Matrix 2 with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>The resulting product of two matrices</returns>
        double[,] MatrixMultiplication(ref double[,] firstMatrix, ref double[,] secondMatrix,
            Func<double[,], double[,]> functor = null);


        /// <summary>
        /// Map by type Matrix * Matrix>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="coefficient">Numerical coefficient</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>The resulting product of two matrices</returns>
        double[,] MatrixMultiplication(ref double[,] matrix, double coefficient,
            Func<double[,], double[,]> functor = null);
    }
}