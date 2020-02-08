using System;

namespace AnalysisOfKnowledge.MathematicalModel.Extensions
{
    internal static class MatrixExtensions
    {
        // 
        internal static TOut[] CastMatrixValues<TOut, TIn>(this TIn[,] matrix)
            where TIn : struct, IEquatable<TIn>
            where TOut : struct, IEquatable<TIn> =>
        matrix.
    }
}