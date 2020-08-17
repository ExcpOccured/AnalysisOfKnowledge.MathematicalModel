#include <assert.h>
#include <stdlib.h>

#include "src\AlgebraicOperations\header\primitiveTypes.h"

Matrix_t* AllocateMatrixMemory(int dimension) {
    double** matrixHeaderPointer = (double**)malloc(dimension * sizeof(double*) + dimension * dimension * sizeof(double));

    double* headerStartPosition = (double*)((char*) matrixHeaderPointer + dimension * sizeof(double*));  
    
    for(int index = 0; index < dimension; index++) {
        matrixHeaderPointer[index] = headerStartPosition + index * dimension * sizeof(double); 
    }

    Matrix_t* matrix = (Matrix_t*)(sizeof(Matrix_t));

    matrix->Dimension = dimension;
    matrix->MatrixHeader = matrixHeaderPointer;

    return matrix;
}

void DisposeMatrixMemory(Matrix_t* matrix) {
    free(matrix->MatrixHeader);

    free(matrix);
}
