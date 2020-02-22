using AnalysisOfKnowledge.MathematicalModel.Processing;
using AnalysisOfKnowledge.MathematicalModel.Tests.Models;
using NUnit.Framework;

namespace AnalysisOfKnowledge.MathematicalModel.Tests.Tests
{
    [TestFixture]
    internal class MatrixInvertTest
    {
        private MatrixFunctionsTestSetModel TestSetModel { get; } = new MatrixFunctionsTestSetModel();

        [Test]
        public void InvertMatrixTest()
        {
            var matrix = MatrixFunctionsTestSetModel.TestMatrix3;

            var expectedMatrix = new double[,]
            {
                {1, -1, 1},
                {-38, 41, -34},
                {27, -29, 24}
            };

            var moqAlgebraFunctionsService = new AlgebraicMatrixFunctions();
            var resultingMatrix = moqAlgebraFunctionsService.InverseMatrix(ref matrix);

            Assert.AreEqual(resultingMatrix, expectedMatrix);
        }
    }
}