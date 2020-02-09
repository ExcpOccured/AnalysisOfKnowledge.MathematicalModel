using System;
using AnalysisOfKnowledge.MathematicalModel.Exceptions;
using AnalysisOfKnowledge.MathematicalModel.Models.Constants;
using AnalysisOfKnowledge.MathematicalModel.Models.Delegates;
using AnalysisOfKnowledge.MathematicalModel.Processing.Interfaces;

namespace AnalysisOfKnowledge.MathematicalModel.Processing
{
    /// <summary>
    /// Implementation of <see cref="IAlgebraicMatrixFunctions"/>>
    /// </summary>
    internal class AlgebraicMatrixFunctions : IAlgebraicMatrixFunctions
    {
        private double PrecalculatedDeterminant { get; set; } = double.NaN;

        /// <summary>
        /// Map by type <para>matrix</para>^T>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>Transposed matrix with dimension {M, N}</returns>
        public double[,] TransposedMatrix(ref double[,] matrix, Func<double[,], double[,]> functor = null)
        {
            var rows = matrix.GetLength(0);
            var cells = matrix.GetLength(1);

            var resultingMatrix = new double[rows, cells];

            // Probably inline LINQ method Aggregate will be better then O(n^2)
            FunctionOverMatrixIndexValue(cells, rows, ref matrix,
                (int cell, int row, ref double[,] computedMatrix) =>
                    resultingMatrix[cell, row] = computedMatrix[row, cell]);

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
        public double[,] InverseMatrix(ref double[,] matrix, Func<double[,], double[,]> functor = null)
        {
            var rows = matrix.GetLength(0);
            var cells = matrix.GetLength(1);

            if (rows != cells)
            {
                throw new ArithmeticException(ExceptionMessages.NonSquareMatrixExceptionMessage);
            }

            var determinant = MatrixDeterminant(ref matrix);

            // Limitation of the numerical method
            if (Math.Abs(determinant) < MatrixFunctionsConstants.DoubleComparisonDelta)
            {
                throw new ArithmeticException(ExceptionMessages.NonSquareMatrixExceptionMessage);
            }

            var resultingMatrix = new double[rows, rows];

            FunctionOverMatrixIndexValue(rows, cells, ref matrix,
                (int row, int cell, ref double[,] calculatedMatrix) =>
                {
                    resultingMatrix[row, cell] = ((row + cell) % 2 == 1 ? -1 : 1) * CalculateMinor(row, cell,
                                                     ref calculatedMatrix,
                                                     CreateMatrixWithoutCell, CreateMatrixWithoutRow,
                                                     MatrixDeterminant) / determinant;
                });

            return TransposedMatrix(ref resultingMatrix);
        }

        /// <summary>
        /// Map by type Matrix * Matrix>
        /// </summary>
        /// <param name="firstMatrix">Matrix 1 with dimension {M, N}</param>
        /// <param name="secondMatrix">Matrix 2 with dimension {M, N}</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>The resulting product of two matrices</returns>
        public double[,] MatrixMultiplication(double[,] firstMatrix, double[,] secondMatrix,
            Func<double[,], double[,]> functor = null)
        {
            var firstMatrixRows = firstMatrix.GetLength(0);
            var firstMatrixCells = firstMatrix.GetLength(1);

            var secondMatrixRows = secondMatrix.GetLength(0);
            var secondMatrixCells = secondMatrix.GetLength(1);

            var resultingMatrix = new double[firstMatrixRows, secondMatrixCells];

            if (firstMatrixCells != secondMatrixRows)
            {
                throw new ArgumentException(ExceptionMessages.NotCompatibleMatricesExceptionMessage);
            }

            FunctionOver2MatricesIndexValue(firstMatrixRows, secondMatrixCells, ref firstMatrix,
                ref secondMatrix,
                (int row, int cell, ref double[,] firstComputingMatrix, ref double[,] secondComputingMatrix) =>
                {
                    double buffer = 0;

                    for (var cellIndex = 0; cellIndex < firstMatrixCells; cellIndex++)
                    {
                        buffer += firstComputingMatrix[row, cellIndex] * secondComputingMatrix[row, cellIndex];
                    }

                    resultingMatrix[row, cell] = buffer;
                });

            return !(functor is null)
                ? functor(resultingMatrix)
                : resultingMatrix;
        }

        /// <summary>
        /// Calculates the determinant of the matrix
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <returns>Determinant of the matrix</returns>
        private double MatrixDeterminant(ref double[,] matrix)
        {
            if (!double.IsNaN(PrecalculatedDeterminant))
            {
                return PrecalculatedDeterminant;
            }

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

            double buffer = 0;

            for (var index = 0; index < cells; index++)
            {
                buffer += (index % 2 == 1 ? 1 : -1) * matrix[1, index] *
                          CalculateMinor(1, index, ref matrix,
                              CreateMatrixWithoutCell, CreateMatrixWithoutRow,
                              MatrixDeterminant);
            }

            PrecalculatedDeterminant = buffer;

            return buffer;
        }

        /// <summary>
        /// Creates a vector
        /// </summary>
        private double[,] CreateMatrixWithoutCell(int cell, ref double[,] mainMatrix)
        {
            var rows = mainMatrix.GetLength(0);
            var cells = mainMatrix.GetLength(1);

            if (cell < 0 || cell >= cells)
            {
                throw new ArithmeticException(ExceptionMessages.ArithmeticalExceptionMessage);
            }

            var resultingMatrix = new double[rows, cells - 1];

            FunctionOverMatrixIndexValue(rows, cells, ref mainMatrix,
                (int row, int cellIndex, ref double[,] computedMatrix) =>
                {
                    resultingMatrix[row, cell] = cellIndex < cell
                        ? computedMatrix[row, cellIndex]
                        : computedMatrix[row + 1, cellIndex];
                });

            return resultingMatrix;
        }

        /// <summary>
        /// Creates a invariant vector
        /// </summary>
        private double[,] CreateMatrixWithoutRow(int row, ref double[,] mainMatrix)
        {
            var rows = mainMatrix.GetLength(0);
            var cells = mainMatrix.GetLength(1);

            if (row < 0 || row >= rows)
            {
                throw new ArithmeticException(ExceptionMessages.ArithmeticalExceptionMessage);
            }

            var resultingMatrix = new double[rows - 1, cells];

            FunctionOverMatrixIndexValue(rows, cells, ref mainMatrix,
                (int rowIndex, int cell, ref double[,] computedMatrix) =>
                {
                    resultingMatrix[rowIndex, cell] = rowIndex < row
                        ? computedMatrix[rowIndex, cell]
                        : computedMatrix[rowIndex + 1, cell];
                });

            return resultingMatrix;
        }

        /// <summary>
        /// Evaluate the matrix minor by element index
        /// </summary>
        private double CalculateMinor(int rowIndex, int cellIndex, ref double[,] mainMatrix,
            MatricesRefActionsDelegate.RefFunc<double[,], int, double[,]> createMatrixWithoutCell,
            MatricesRefActionsDelegate.RefFunc<double[,], int, double[,]> createMatrixWithoutRow,
            MatricesRefActionsDelegate.RefFunc<double, double[,]> calculateDeterminant)
        {
            var vector = createMatrixWithoutCell(cellIndex, ref mainMatrix);

            var invariantVector = createMatrixWithoutRow(rowIndex, ref vector);

            return calculateDeterminant(ref invariantVector);
        }

        /// <summary>
        /// Encapsulates an operation over the elements of the matrix
        /// </summary>
        /// <param name="upperBound">Dimension M of the matrix {M, N}</param>
        /// <param name="lowerBound">Dimension N of the matrix {M, N}</param>
        /// <param name="matrix"></param>
        /// <param name="functor">Operation on matrix elements</param>
        private void FunctionOverMatrixIndexValue(int upperBound, int lowerBound, ref double[,] matrix,
            MatricesRefActionsDelegate.RefAction<int, int, double[,]> functor)
        {
            for (var upperIndex = 0; upperIndex < upperBound; upperIndex++)
            {
                for (var lowerIndex = 0; lowerIndex < lowerBound; lowerIndex++)
                {
                    functor(upperIndex, lowerIndex, ref matrix);
                }
            }
        }

        /// <summary>
        /// Encapsulates an operation over the elements of the 2 matrices
        /// </summary>
        /// <param name="upperBound">Dimension M of the matrix {M, N}</param>
        /// <param name="lowerBound">Dimension N of the matrix {M, N}</param>
        /// <param name="firstMatrix">First calculating matrix</param>
        /// <param name="secondMatrix">Second calculating matrix</param>
        /// <param name="functor">Operation on matrix elements</param>
        private void FunctionOver2MatricesIndexValue(int upperBound, int lowerBound, ref double[,] firstMatrix,
            ref double[,] secondMatrix, MatricesRefActionsDelegate.RefAction<int, int, double[,], double[,]> functor)
        {
            for (var upperIndex = 0; upperIndex < upperBound; upperIndex++)
            {
                for (var lowerIndex = 0; lowerIndex < lowerBound; lowerIndex++)
                {
                    functor(upperIndex, lowerIndex, ref firstMatrix, ref secondMatrix);
                }
            }
        }
    }
}