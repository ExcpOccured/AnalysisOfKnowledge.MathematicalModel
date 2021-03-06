# Mathematical model

It`s a С11 Shared library of [project](https://github.com/users/ExcpOccured/projects/1)

## Description

The mathematical model can be described as factorization of regression features based on 4 steps:

1. Numerical values of the user test features, 
a polynomial is formed that determines the multiple regression lines of the form:
![alt text](https://latex.codecogs.com/gif.latex?y*%20%3D%20a_%7B0%7D%20&plus;%20a_%7B1%7Dx1%20&plus;%20a_%7B2%7Dx2%20&plus;%20a_%7B3%7Dx3%20&plus;%20a_%7B3%7Dx3%20&plus;%20a_%7B4%7Dx4%20&plus;%20a_%7B5%7Dx5%20&plus;%20a_%7B6%7Dx6)
![alt text](http://latex.codecogs.com/gif.latex?%7E%20where%20%7E%20x1%2Cx2%2Cx3%2Cx4%2Cx5%2Cx6%20-%20%7E%20factor%20%7E%20features%2C%20%7E%20y%20-%20resulting%20%7E%20factor)

2. To find the numerical coefficients of _a_ by solving a system of linear algebraic equations of the fifth order:
![alt text](http://latex.codecogs.com/gif.latex?A%20%3D%20%28X%5E%7BT%7D%20%5Ctimes%20X%29%5E%7B-1%7D%20%5Ctimes%20%28X%5E%7BT%7DY%29%20)
![alt text](http://latex.codecogs.com/gif.latex?%7Ewhere%7E%20X%20-%20%7Ethe%20%7E%20matrix%20%7E%20of%20%7E%20factor%20%7E%20variables)

3. Calculating the degree of influence of each attribute on the result:
![alt text](http://latex.codecogs.com/gif.latex?%5Cbeta_%7BK%7D%20%3D%20a_%7BK%7D%20&plus;%20%28%5Cfrac%7B%5Cdelta_%7BK%7D%7D%7B%5Cdelta_%7BY%7D%7D%29%20%7E%20where%20%7E%20%5Cdelta_%7BK%7D%20-%20%7E%20The%20%7E%20average%20%7E%20square%20%7E%20deviation%20%7E%20of%20%7E%20the%20%7E%20five%20%7E%20coefficients)

4. Extrapolating the results obtained with the ability to build a matrix of results

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details



