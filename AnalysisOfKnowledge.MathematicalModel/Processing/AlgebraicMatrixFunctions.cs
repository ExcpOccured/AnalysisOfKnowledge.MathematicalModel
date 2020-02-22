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
    public class AlgebraicMatrixFunctions : IAlgebraicMatrixFunctions
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
        public double[,] MatrixMultiplication(ref double[,] firstMatrix, ref double[,] secondMatrix,
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

            FunctionOver2MatricesIndexValue(secondMatrixRows, secondMatrixCells, ref firstMatrix,
                ref secondMatrix,
                (int row, int cell, ref double[,] firstComputingMatrix, ref double[,] secondComputingMatrix) =>
                {
                    for (var cellIndex = 0; cellIndex < firstMatrixCells; cellIndex++)
                    {
                        resultingMatrix[row, cell] +=
                            firstComputingMatrix[row, cellIndex] * secondComputingMatrix[cellIndex, cell];
                    }
                });

            return !(functor is null)
                ? functor(resultingMatrix)
                : resultingMatrix;
        }

        /// <summary>
        /// Map by type Matrix * Matrix>
        /// </summary>
        /// <param name="matrix">Matrix with dimension {M, N}</param>
        /// <param name="coefficient">Numerical coefficient</param>
        /// <param name="functor">Optional functor</param>
        /// <returns>The resulting product of two matrices</returns>
        public double[,] MatrixMultiplication(ref double[,] matrix, double coefficient,
            Func<double[,], double[,]> functor = null)
        {
            var rows = matrix.GetLength(0);
            var cells = matrix.GetLength(1);

            var resultingMatrix = new double[rows, cells];

            FunctionOverMatrixIndexValue(rows, cells, ref matrix,
                (int row, int cell, ref double[,] processingMatrix) =>
                {
                    resultingMatrix[row, cell] = processingMatrix[row, cell] * coefficient;
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
        public double MatrixDeterminant(ref double[,] matrix)
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
                          CalculateMinor(index, 1, ref matrix,
                              CreateMatrixWithoutCell, CreateMatrixWithoutRow,
                              MatrixDeterminant);
            }

            PrecalculatedDeterminant = buffer;

            return buffer;
        }

        /// <summary>
        /// Evaluates the matrix minor
        /// </summary>
        private double[,] CreateMatrixWithoutCell(int cell, ref double[,] processingMatrix)
        {
            var rows = processingMatrix.GetLength(0);
            var cells = processingMatrix.GetLength(1);

            if (cell < 0 || cell >= cells)
            {
                throw new ArithmeticException(ExceptionMessages.ArithmeticalExceptionMessage);
            }

            var minor = new double[rows, cells - 1];

            FunctionOverMatrixIndexValue(rows, cells - 1, ref processingMatrix,
                (int row, int cellIndex, ref double[,] computedMatrix) =>
                    minor[row, cell] = cellIndex < cell
                        ? computedMatrix[row, cellIndex]
                        : computedMatrix[row, cellIndex + 1]);

            return minor;
        }

        /// <summary>
        /// Evaluates the matrix invariant minor
        /// </summary>
        private double[,] CreateMatrixWithoutRow(int row, ref double[,] processingMatrix)
        {
            var rows = processingMatrix.GetLength(0);
            var cells = processingMatrix.GetLength(1);

            if (row < 0 || row >= rows)
            {
                throw new ArithmeticException(ExceptionMessages.ArithmeticalExceptionMessage);
            }

            var minor = new double[rows - 1, cells];

            FunctionOverMatrixIndexValue(rows - 1, cells, ref processingMatrix,
                (int rowIndex, int cell, ref double[,] computedMatrix) =>
                    minor[rowIndex, cell] = rowIndex < row
                        ? computedMatrix[rowIndex, cell]
                        : computedMatrix[rowIndex + 1, cell]);

            return minor;
        }

        /// <summary>
        /// Evaluate the matrix minor by element index
        /// </summary>
        private double CalculateMinor(int rowIndex, int cellIndex, ref double[,] processingMatrix,
            RefFunc<double[,], int, double[,]> createMatrixWithoutCell,
            RefFunc<double[,], int, double[,]> createMatrixWithoutRow,
            RefFunc<double, double[,]> calculateDeterminant)
        {
            var minor = createMatrixWithoutCell(cellIndex, ref processingMatrix);

            var invariantMinor = createMatrixWithoutRow(rowIndex, ref minor);

            return calculateDeterminant(ref invariantMinor);
        }

        /// <summary>
        /// Encapsulates an operation over the elements of the matrix
        /// </summary>
        /// <param name="upperBound">Dimension M of the matrix {M, N}</param>
        /// <param name="lowerBound">Dimension N of the matrix {M, N}</param>
        /// <param name="matrix"></param>
        /// <param name="functor">Operation on matrix elements</param>
        private void FunctionOverMatrixIndexValue(int upperBound, int lowerBound, ref double[,] matrix,
            RefAction<int, int, double[,]> functor)
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
            ref double[,] secondMatrix, RefAction<int, int, double[,], double[,]> functor)
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