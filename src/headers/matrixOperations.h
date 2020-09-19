#ifndef MATH_MODEL_MATRIX_OPERATIONS
#define MATH_MODEL_MATRIX_OPERATIONS

#include "algebraicOperations.h"
#include "dataTypes.h"

#include <stdbool.h>
#include <math.h>

#define DIMENSION_ASSERT_FAILED "Wrong dimension of matrix!"
#define INVERSE_ASSERT_FAILED "Singular matrix, can't find its inverse"

/*

*/
void EvaluateCoFactor(Matrix_t* matrix, Matrix_t* destination, int row, int cell, int dimension);

/*

*/
double CalculateDeterminate(Matrix_t* matrix, int executedDimension);

/*

*/
void AdjointMatrix(Matrix_t* rank, Matrix_t* sourceMatrix);


/*

*/
bool IsAbleToInverseMatrix(Matrix_t* matrix, Matrix_t* matrixToInverse);

/*
   ATTENTION: Contains the allocation!

   Multiplication of two square matrix
*/
Matrix_t* Multiply(Matrix_t* firstMatrix, Matrix_t* secondMatrix);

#endif //MATH_MODEL_MATRIX_OPERATIONS
