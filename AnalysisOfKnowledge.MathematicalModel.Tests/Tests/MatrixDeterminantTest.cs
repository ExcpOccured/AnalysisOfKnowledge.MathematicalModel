using AnalysisOfKnowledge.MathematicalModel.Processing;
using AnalysisOfKnowledge.MathematicalModel.Tests.Models;
using NUnit.Framework;

namespace AnalysisOfKnowledge.MathematicalModel.Tests.Tests
{
    [TestFixture]
    public class MatrixDeterminantTest
    {
        private MatrixFunctionsTestSetModel TestSetModel { get; } = new MatrixFunctionsTestSetModel();

        [Test]
        public void InvertMatrixTest()
        {
            var matrix = TestSetModel.TestMatrix1;
            
            var moqAlgebraFunctionsService = new AlgebraicMatrixFunctions();
            var determinant = moqAlgebraFunctionsService.MatrixDeterminant(ref matrix);

            Assert.AreEqual(determinant, 84.0);
        }
    }
}