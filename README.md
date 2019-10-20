# MatrixEssentials

Library providing extensible Matrix (2d array) abstract data type. Allows more 
Designed to be used for manipulating images. I use it in my blur and edge filter algorithms.
You can use it if you want to encapsulate algorithms modifing matrices.

## Requirements

- dotnet 3.2+

## How to use

1. How to create matrix object.
  - Creating empty matrix can be done like this:
  ```csharp
  var width = ...;
  var height = ...;
  var matrixDataType = typeof(...); //type of the content of the matrix. my api doesnt allow multiple types inside one matrix.
  var matrix = new Matrix(width, height, matrixDataType);
  ```

  - Creating matrix from already instantiated list:
  ```csharp
  IList<IList<IMatrixData>> yourList;
  var matrix = new Matrix(yourlist);
  ```

2. How to set/get value

```csharp
IMatrix matrix;
var matrixValue = matrix.GetValue(x, y);
```

```csharp
matrix.SetValue(x, y, matrixValue);
```

## Show me examples
- https://github.com/LyuboslavLyubenov/LaplacianOperatorFilter/blob/master/LaplacianOperatorFilter.cs#L27 implementation of laplacian operator filter using this library
