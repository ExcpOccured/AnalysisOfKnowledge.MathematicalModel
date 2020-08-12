#ifndef MATHMODEL_OPERATION_H
#define MATHMODEL_OPERATION_H

#include "src\AlgebraicOperations\header\algebraicOperations.h"
#include "src\AlgebraicOperations\header\primitiveTypes.h"

#include <stdbool.h>

#define DIMENSION_ASSERT_FAILED "Wrong dimension of matrix!"

#define INVERSE_ASSERT_FAILED "Singular matrix, can't find its inverse"

void EvaluateCoFactor(Matrix_t* matrix, Matrix_t* destination, int row, int cell, int dimension);

int CalculateDeterminate(Matrix_t* matrix, int executedDimension);

void AdjointMatrix(Matrix_t* rank, Matrix_t* sourceMatrix);

bool IsAbleToInverseMatrix(Matrix_t* matrix, Matrix_t* matrixToInverse);

#endif //MATHMODEL_OPERATION_H
