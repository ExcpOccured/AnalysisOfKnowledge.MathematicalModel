#include "headers\dataTypes.h"

Matrix_t* AllocateMatrixMemory(int dimension) {
    // We allocate memory once to avoid block fragmentation
    double** matrixHeaderPointer = (double**)malloc(dimension * sizeof(double*) + dimension * dimension * sizeof(double));

    double* headerStartPosition = (double*)((char*) matrixHeaderPointer + dimension * sizeof(double*));

    // Marking up an array of pointers
    for(int index = 0; index < dimension; index++) {
        matrixHeaderPointer[index] = headerStartPosition + index * dimension * sizeof(double);
    }

    Matrix_t* matrix = (Matrix_t*)malloc(sizeof(Matrix_t));

    matrix->Dimension = dimension;
    matrix->MatrixHeader = matrixHeaderPointer;

    return matrix;
}

void DisposeMatrixMemory(Matrix_t* matrix) {
    free(matrix->MatrixHeader);

    free(matrix);
}


