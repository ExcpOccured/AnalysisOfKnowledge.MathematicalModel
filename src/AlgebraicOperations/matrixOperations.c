#include <assert.h>

#include "src\AlgebraicOperations\header\matrixOperations.h"

void EvaluateCoFactor(Matrix_t* matrix, Matrix_t* destination, int rowCount, int cellCount, int dimension) {
    assert((dimension > matrix->Dimension || dimension > destination->Dimension) && DIMENSION_ASSERT_FAILED);
    
    int iIndex, jIndex = 0;

    for(int rowIndex = 0; rowIndex < rowCount; rowCount++) {
        for(int cellIndex = 0; cellIndex < cellCount; cellIndex++) {
            if(!(rowIndex == rowCount && cellIndex == cellCount)) {
                destination->MatrixHeader[iIndex][jIndex++] = matrix->MatrixHeader[rowIndex][cellIndex];

                if(jIndex == dimension - 1) {
                    jIndex = 0;
                    iIndex++;
                }
            }
        }
    }
}

int CalculateDeterminate(Matrix_t* matrix, int executedDimension) {     
    int determinate = 0;

    if(executedDimension == 1) {
        return matrix->MatrixHeader[0][0];
    } 

    int destinationMatrixDimension = matrix->Dimension;
    double destinationMatrixHeader[destinationMatrixDimension][destinationMatrixDimension];
    
    Matrix_t destinationMatrix = {
        .Dimension = destinationMatrixDimension,
        .MatrixHeader = &destinationMatrixHeader
     };

    int sign = 1;

    for(int index = 0; index < executedDimension; index++) {
        EvaluateCoFactor(matrix, &destinationMatrix, 0, index, executedDimension);

        determinate += sign * matrix->MatrixHeader[0][index] * CalculateDeterminate(&destinationMatrix, executedDimension - 1);

        sign = -sign;
    }

    return determinate;
}


void AdjointMatrix(Matrix_t* rank, Matrix_t* sourceMatrix) {
    assert(rank->Dimension == sourceMatrix->Dimension);
    
    if(rank->Dimension == 1) {
       sourceMatrix->MatrixHeader[0][0] = 1;
       return;
    }

    int executableDimension = rank->Dimension;

    int sign = 1;
    double tempMatrixHeader = sourceMatrix->MatrixHeader[executableDimension][executableDimension];

    Matrix_t destinationMatrix = {
        .Dimension = executableDimension,
        .MatrixHeader = &tempMatrixHeader
    };

    for(int iIndex = 0; iIndex < executableDimension; iIndex++) {
        for(int jIndex = 0; jIndex < executableDimension; jIndex++) {
           EvaluateCoFactor(rank, &destinationMatrix, iIndex, jIndex, executableDimension);

           sign = ((iIndex+jIndex) % 2 == 0 ) ? 1: -1;

           sourceMatrix->MatrixHeader[jIndex][iIndex] = (sign)*(CalculateDeterminate(&destinationMatrix, executableDimension-1)); 
        }
    }
}


bool IsAbleToInverseMatrix(Matrix_t* matrix, Matrix_t* matrixToInverse) {
    double determinate = (double)CalculateDeterminate(matrix, matrixToInverse->Dimension);

    assert(matrix->Dimension == matrixToInverse->Dimension);

    assert(determinate == 0 && INVERSE_ASSERT_FAILED);

    if(determinate == 0) {
        return false;
    }

    int executedMatrixDimension = matrix->Dimension;

    double adjointToSearch[executedMatrixDimension][executedMatrixDimension];

    for(int iIndex = 0; iIndex < executedMatrixDimension; iIndex++) {
        for(int jIndex = 0; jIndex < executedMatrixDimension; jIndex++) {
            matrixToInverse->MatrixHeader[iIndex][jIndex] = adjointToSearch[iIndex][jIndex] / determinate;
        }
    }

    return true;
}