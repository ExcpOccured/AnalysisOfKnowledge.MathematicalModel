#ifndef MATHMODEL_OPERATION_H
#define MATHMODEL_OPERATION_H

#include "src\AlgebraicOperations\header\algebraicOperations.h"
#include "src\AlgebraicOperations\header\primitiveTypes.h"

int GetMatrixDeterminate(Matrix_t* matrix, int executableDimension);

void AdjointMatrix(Matrix_t* rank, Matrix_t* sourceMatrix);

bool IsAbleToInverseMatrix(Matrix_t* matrix, Matrix_t* matrixToInverse);

#endif //MATHMODEL_OPERATION_H
