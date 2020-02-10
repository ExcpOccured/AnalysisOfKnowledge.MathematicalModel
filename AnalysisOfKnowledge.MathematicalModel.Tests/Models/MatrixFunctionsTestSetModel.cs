namespace AnalysisOfKnowledge.MathematicalModel.Tests.Models
{
    internal class MatrixFunctionsTestSetModel
    {
        internal double[,] EMatrix => new double[,]
        {
            {1, 0, 0},
            {0, 1, 0},
            {0, 0, 1}
        };

        internal double[,] TestMatrix1 => new double[,]
        {
            {-1, 0, -2},
            {-5, 4, -7},
            {6, 4, -6}
        };

        internal double[,] TestMatrix2 => new double[,]
        {
            {1, 1},
            {2, 0},
            {3, 3}
        };

        internal double[,] TestMatrix3 => new double[,]
        {
            {2, 5, 7},
            {6, 3, 4},
            {5, -2, -3}
        };
    }
}