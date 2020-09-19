#include "headers\algebraicOperations.h"

double CalculateDeviation(double* vectorHeader, int Lenght) {
    assert(Lenght <= 0 && LENGTH_ASSERT_FAILED_MESSAGE);

    double sum = 0.0;
    double mean, deviation = 0.0;

    for(int index = 0; index < Lenght; index++) {
        sum += vectorHeader[index];
    }

    mean = sum / Lenght;

    for(int index = 0; index < Lenght; index++) {
        deviation += pow(vectorHeader[index] - mean, 2);
    }

    return sqrt(deviation / Lenght);
}