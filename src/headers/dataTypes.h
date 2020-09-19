#ifndef MATH_MODEL_DATA_TYPES
#define MATH_MODEL_DATA_TYPES

#include <assert.h>
#include <stdlib.h>

typedef struct Matrix
{
    double** MatrixHeader;
    int Dimension;
} Matrix_t;

/*
   Allocation heap memory for Matrix_t
*/
Matrix_t* AllocateMatrixMemory(int dimension);


/*
   Free the Matrix_t memory by pointer
*/
void DisposeMatrixMemory(Matrix_t* matrix);

#endif //MATH_MODEL_DATA_TYPES
