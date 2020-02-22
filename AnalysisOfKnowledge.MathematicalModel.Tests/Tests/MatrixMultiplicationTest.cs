using AnalysisOfKnowledge.MathematicalModel.Processing;
using AnalysisOfKnowledge.MathematicalModel.Tests.Models;
using NUnit.Framework;

namespace AnalysisOfKnowledge.MathematicalModel.Tests.Tests
{
    [TestFixture]
    internal class MatrixMultiplicationTest
    {
        private MatrixFunctionsTestSetModel TestSetModel { get; } = new MatrixFunctionsTestSetModel();

        [Test]
        public void TestMultiplicationOnEMatrix()
        {
            var matrix = MatrixFunctionsTestSetModel.TestMatrix1;
            var eMatrix = MatrixFunctionsTestSetModel.EMatrix;

            var moqAlgebraFunctionsService = new AlgebraicMatrixFunctions();
            var resultingMatrix = moqAlgebraFunctionsService.MatrixMultiplication(ref matrix, ref eMatrix);

            Assert.AreEqual(resultingMatrix, matrix);
        }

        [Test]
        public void TestMultiplicationOnMatrix()
        {
            var matrix1 = MatrixFunctionsTestSetModel.TestMatrix1;
            var matrix2 = MatrixFunctionsTestSetModel.TestMatrix2;

            var expectedMatrix = new double[,]
            {
                {-7, -7},
                {-18, -26},
                {-4, -12}
            };

            var moqAlgebraFunctionsService = new AlgebraicMatrixFunctions();
            var resultingMatrix = moqAlgebraFunctionsService.MatrixMultiplication(ref matrix1, ref matrix2);

            Assert.AreEqual(resultingMatrix, expectedMatrix);
        }

        [Test]
        public void TestCoefficientsAndCombinedFunctions()
        {
            var matrix = MatrixFunctionsTestSetModel.TestMatrix1;

            var expectedMatrix = new double[,]
            {
                {-2, 0, -4},
                {-10, 8, -14},
                {12, 8, -12}
            };

            var moqAlgebraFunctionsService = new AlgebraicMatrixFunctions();
            var resultingMatrix1 = moqAlgebraFunctionsService.MatrixMultiplication(ref matrix, 2);

            Assert.AreEqual(resultingMatrix1, expectedMatrix);

            var resultingMatrix2 = moqAlgebraFunctionsService.MatrixMultiplication(ref matrix, 2,
                processingMatrix => moqAlgebraFunctionsService.MatrixMultiplication(ref processingMatrix, 0.5));

            Assert.AreEqual(resultingMatrix2, matrix);
        }
    }
}