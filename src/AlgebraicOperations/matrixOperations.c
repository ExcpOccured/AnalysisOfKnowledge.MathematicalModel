#include <assert.h>


#include "src\AlgebraicOperations\header\matrixOperations.h"

void EvaluateCoFactor(Matrix_t* matrix, Matrix_t* destination, int rowCount, int cellCount, int dimension) {
    assert((dimension > matrix->Dimension || dimension > destination->Dimension)&&  DIMENSION_ASSERT_FAILED);
    
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

int DeterminateMatrix(Matrix_t* matrix, int executableDimension) {
    assert(executableDimension <= matrix->Dimension &&  DIMENSION_ASSERT_FAILED);
     
    int dimension = 0;

    if(executableDimension == 1) {
        return matrix->MatrixHeader[0][0];
    } 

    int matrixDimension = matrix->Dimension;
    

    int sign = 1;

    for(int index = 0; index < executableDimension; index++) {
        EvaluateCoFactor(matrix, )
    }




}