using System;
using System.Collections.Generic;

namespace AnalysisOfKnowledge.MathematicalModel.Processing.Interfaces
{
    /// <summary>
    /// Contains general-purpose algebraic functions
    /// </summary>
    internal interface ICommonAlgebraicFunctions
    {
        /// <summary>
        /// Sample variance of collection value
        /// </summary>
        /// <param name="collectionOfResearchingValues">Collection of investigated values</param>
        /// <typeparam name="TValue">Type of processing value</typeparam>
        /// <returns>RMS value</returns>
        TValue AverageSquareValue<TValue>(IEnumerable<TValue> collectionOfResearchingValues)
            where TValue : struct, IEquatable<TValue>;
    }
}