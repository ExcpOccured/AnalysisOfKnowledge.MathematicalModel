#include "src\algebraicOperations\header\matrixOperations.h"
#include "src\algebraicOperations\header\primitiveTypes.h"
#include "sdk\header\testUtilities.h"

int main(int argc, char const *argv[])
{
    Matrix_t* matrix1 = AllocateMatrixMemory(4);

    printfn("Matrix dimensions is %d", matrix1->Dimension);

    system("pause");

    return 0;
}
