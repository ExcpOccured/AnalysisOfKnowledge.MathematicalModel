typedef struct Matrix
{
    double** MatrixHeader;
    int Dimension;
} Matrix_t;


Matrix_t* AllocateMatrixMemory(int dimension);

void DisposeMatrixMemory(Matrix_t* matrix);
