namespace AnalysisOfKnowledge.MathematicalModel.Models.Delegates
{
    internal static class MatricesRefActionsDelegate
    {
        /// <summary>
        /// Action delegate that allows to pass a type by reference
        /// </summary>
        internal delegate void RefAction<in TIn1, in TIn2, TIn3>(TIn1 arg1, TIn2 arg2, ref TIn3 arg3);

        /// <summary>
        /// Action delegate that allows to pass a two type by reference
        /// </summary>
        internal delegate void RefAction<in TIn1, in TIn2, TIn3, TIn4>(TIn1 arg1, TIn2 arg2, ref TIn3 arg3,
            ref TIn4 arg4);

        /// <summary>
        /// Function delegate that allows to pass a type by reference
        /// </summary>
        internal delegate TOut RefFunc<out TOut, TIn>(ref TIn arg1);


        /// <summary>
        /// Function delegate that allows to pass a type by reference
        /// </summary>
        internal delegate TOut RefFunc<out TOut, in TIn1, TIn2>(TIn1 arg1, ref TIn2 arg2);
    }
}