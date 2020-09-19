#ifndef MATH_MODEL_ALGEBRAIC_OPERATIONS
#define MATH_MODEL_ALGEBRAIC_OPERATIONS

#define LENGTH_ASSERT_FAILED_MESSAGE "Length must be more than 0."

#include <assert.h>
#include <math.h>

/*
   Root-mean-square deviation of factor attributes
*/
double CalculateDeviation(double* vectorHeader, int Length);

#endif //MATH_MODEL_ALGEBRAIC_OPERATIONS
