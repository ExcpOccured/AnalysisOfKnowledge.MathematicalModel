using System;
using System.Collections.Generic;
using AnalysisOfKnowledge.MathematicalModel.Models;
using AnalysisOfKnowledge.MathematicalModel.Processing.Interfaces;

namespace AnalysisOfKnowledge.MathematicalModel.Processing
{
    /// <summary>
    /// Implementation of <see cref="IAlgebraicMatrixFunctions"/>>
    /// </summary>
    internal class AlgebraicMatrixFunctions : IAlgebraicMatrixFunctions
    {
        /// <summary>
        /// Map by type <para>matrix</para>^T>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>Transposed matrix with dimension {M, N}</returns>
        public TMatrix[,] TransposedMatrix<TMatrix>(TMatrix[,] matrix, Func<TMatrix[,], TMatrix[,]> functor = null)
            where TMatrix : ICollection<TMatrix>
        {
            var rows = matrix.GetLength(0);
            var cells = matrix.GetLength(1);

            var resultingMatrix = new TMatrix[rows, cells];

            // Probably inline LINQ method Aggregate will be better then O(n^2)
            FunctionOverMatrixIndexValue(cells, rows,
                (cell, row) => resultingMatrix[cell, row] = matrix[row, cell]);

            return !(functor is null)
                ? functor(resultingMatrix)
                : resultingMatrix;
        }

        /// <summary>
        ///  /// Map by type <para>matrix</para>^-1>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>Transposed matrix with dimension {M, N}</returns>
        public TMatrix[,] InverseMatrix<TMatrix>(TMatrix[,] matrix, Func<TMatrix[,], TMatrix[,]> functor = null)
            where TMatrix : ICollection<TMatrix>
        {
            var rows = matrix.GetLength(0);
            var cells = matrix.GetLength(1);
        }

        /// <summary>
        /// Map by type Matrix * Matrix>
        /// </summary>
        /// <param name="firstMatrix">Matrix 1 with dimension {M, N}</param>
        /// <param name="secondMatrix">Matrix 2 with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <typeparam name="TMatrix">The resulting multiplication of two matrices</typeparam>
        /// <returns>The resulting product of two matrices</returns>
        public TMatrix[,] MatrixMultiplication<TMatrix>(TMatrix[,] firstMatrix, TMatrix[,] secondMatrix,
            Func<TMatrix[,], TMatrix[,]> functor = null)
            where TMatrix : ICollection<TMatrix>, IEnumerable<TMatrix>
        {
            var firstMatrixRows = firstMatrix.GetLength(0);
            var firstMatrixCells = firstMatrix.GetLength(1);

            var secondMatrixRows = secondMatrix.GetLength(0);
            var secondMatrixCells = secondMatrix.GetLength(1);

            var resultingMatrix = new TMatrix[firstMatrixRows, secondMatrixCells];

            if (firstMatrixCells != secondMatrixRows)
            {
                throw new ArgumentException(ExceptionMessages.NotCompatibleMatricesExceptionMessage);
            }

            for (var row = 0; row < firstMatrixRows; row++)
            {
                for (var cell = 0; cell < secondMatrixCells; cell++)
                {
                    double buffer = 0;

                    for (var cellIndex = 0; cellIndex < firstMatrixCells; cellIndex++)
                    {
                        // C# doesnt support SRTP ب_ب
                        // So i have to use boxing
                        try
                        {
                            double.TryParse(((object) firstMatrix[row, cellIndex]).ToString(), out var leftOperand);
                            double.TryParse(((object) secondMatrix[row, cellIndex]).ToString(), out var rightOperand);

                            buffer += leftOperand * rightOperand;
                        }
                        catch
                        {
                            throw new ArithmeticException(ExceptionMessages.ArithmeticalExceptionMessage +
                                                          $"{row},{cellIndex}");
                        }

                        resultingMatrix[row, cell] = (TMatrix) (buffer as object);
                    }
                }
            }

            return !(functor is null)
                ? functor(resultingMatrix)
                : resultingMatrix;
        }

        /// <summary>
        /// Encapsulates an operation over the elements of the matrix
        /// </summary>
        /// <param name="upperBound">Dimension M of the matrix {M, N}</param>
        /// <param name="lowerBound">Dimension N of the matrix {M, N}</param>
        /// <param name="functor">Operation on matrix elements</param>
        private void FunctionOverMatrixIndexValue(int upperBound, int lowerBound, Action<int, int> functor)
        {
            for (var upperIndex = 0; upperIndex < upperBound; upperIndex++)
            {
                for (var lowerIndex = 0; lowerIndex < lowerBound; lowerIndex++)
                {
                    functor(upperIndex, lowerIndex);
                }
            }
        }

        /// <summary>
        /// Calculates the determinant of the matrix
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <returns>Determinant of the matrix</returns>
        private double MatrixDeterminant<TMatrix>(TMatrix[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cells = matrix.GetLength(1);

            if (rows != cells)
            {
                throw new ArithmeticException(ExceptionMessages.NonSquareMatrixExceptionMessage);
            }

            if (cells == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
        }
    }
}