#include <stdio.h>

#include "../src/headers/dataTypes.h"

int main(int argc, char *argv[]) {
    Matrix_t* matrix = AllocateMatrixMemory(4);

    printf("The matrix dimensions is: %d", matrix->Dimension);

    DisposeMatrixMemory(matrix);

    system("pause");

    return 0;
}