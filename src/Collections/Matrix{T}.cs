using System;
using System.Collections.Generic;

namespace Paramdigma.Core.Collections
{
    /// <summary>
    /// 2-Dimensional generic matrix.
    /// </summary>
    /// <typeparam name="T">Type of the objects in the matrix.</typeparam>
    public class Matrix<T>
    {
        // Matrix Class
        // This class was obtained from:
        // https://codereview.stackexchange.com/questions/194732/class-matrix-implementation
        private T[,] data;

        /// <summary>
        /// Gets columns.
        /// </summary>
        /// <returns>Number of columns on the Matrix.</returns>
        public int N => this.data.GetUpperBound(0) + 1;

        /// <summary>
        /// Gets rows.
        /// </summary>
        /// <returns>Number of rows on the Matrix.</returns>
        public int M => this.data.GetUpperBound(1) + 1;

        /// <summary>
        /// Gets a specific item in the matrix.
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="column">Column.</param>
        public ref T this[int row, int column] => ref data[row, column];

        /// <summary>
        /// Get the row of a matrix at the specified index.
        /// </summary>
        /// <param name="n">Row index.</param>
        /// <returns>Matrix row as list.</returns>
        public T[] Row(int n)
        {
            var row = new T[N];
            for (int i = 0; i < M; i++)
            {
                row[i] = data[n, i];
            }

            return row;
        }

        /// <summary>
        /// Get the column of a matrix at the specified index.
        /// </summary>
        /// <param name="m">Column index.</param>
        /// <returns>Matrix column as list.</returns>
        public T[] Column(int m)
        {
            var col = new T[M];
            for (int i = 0; i < N; i++)
            {
                col[i] = data[i, m];
            }

            return col;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
        /// </summary>
        /// <param name="n">Size of the square Matrix.</param>
        public Matrix(int n)
        {
            data = new T[n, n];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class of the specified size.
        /// </summary>
        /// <param name="n">Column size.</param>
        /// <param name="m">Row size.</param>
        public Matrix(int n, int m)
        {
            data = new T[n, m];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class from a 2D array.
        /// </summary>
        /// <param name="data">2D array of data.</param>
        public Matrix(T[,] data)
        {
            this.data = data;
        }

        // ----- ORDERING METHODS -----

        /// <summary>
        /// Turns columns into rows and rows into columns.
        /// </summary>
        public void FlipMatrix()
        {
            // TODO: Implement FlipMatrix()
            throw new NotImplementedException();
        }

        /// <summary>
        /// Increment Matrix column size by a specified amount.
        /// It accepts both increasing and decreasing the size.
        /// </summary>
        /// <param name="incrementN">Positive or negative increment.</param>
        public void IncrementColumns(int incrementN)
        {
            ResizeMatrix(ref data, N + incrementN, M);
        }

        /// <summary>
        /// Increment Matrix row size by a specified amount.
        /// It accepts both increasing and decreasing the size.
        /// </summary>
        /// <param name="incrementM">Positive or negative increment.</param>
        public void IncrementRows(int incrementM)
        {
            ResizeMatrix(ref data, N, M + incrementM);
        }

        /// <summary>
        /// Increase or decrease the matrix size symetrically.
        /// </summary>
        /// <param name="symetricIncrement">Symetric increase/decrease.</param>
        public void IncrementMatrixSize(int symetricIncrement)
        {
            IncrementMatrixSize(symetricIncrement, symetricIncrement);
        }

        /// <summary>
        /// Increase or decrease the column size of the matrix.
        /// </summary>
        /// <param name="columnIncrement">Column increment.</param>
        /// <param name="rowIncrement">Row increment.</param>
        public void IncrementMatrixSize(int columnIncrement, int rowIncrement)
        {
            IncrementColumns(columnIncrement);
            IncrementRows(rowIncrement);
        }

        /// <summary>
        /// Obtains all neighbour entities surrounding the specified matrix coordinates.
        /// </summary>
        /// <param name="column">Column location.</param>
        /// <param name="row">Row location.</param>
        /// <returns>List of all neighbour entities.</returns>
        public List<T> GetAllNeighboursAt(int column, int row)
        {
            // HACK: This is a hacked up implementation. It provides the neighbours out of order (first contiguous, then corners)
            List<T> neighbours = GetContiguousNeighboursAt(column, row);
            neighbours.AddRange(GetCornerNeighboursAt(column, row));

            return neighbours;
        }

        /// <summary>
        /// Obtains corner neighbour entities surrounding the specified matrix coordinates.
        /// </summary>
        /// <param name="column">Column location.</param>
        /// <param name="row">Row location.</param>
        /// <returns>List of corner neighbours (Diagonally connected).</returns>
        public List<T> GetCornerNeighboursAt(int column, int row)
        {
            // TODO: Implement GetCornerNeighboursOfEntityAt()
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtains contiguous neighbour entities surrounding the specified matrix coordinates.
        /// </summary>
        /// <param name="column">Column location.</param>
        /// <param name="row">Row location.</param>
        /// <returns>List of contiguous neighbours ( Up / Left / Down / Right ).</returns>
        public List<T> GetContiguousNeighboursAt(int column, int row)
        {
            // TODO: Implement GetContiguousNeighboursOfEntityAt()
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resizes any given 2 dimensional array.
        /// It accepts smaller and bigger array outputs.
        /// Obtained from: https://stackoverflow.com/questions/6539571/how-to-resize-multidimensional-2d-array-in-c .
        /// </summary>
        /// <param name="original">2D Array to resize.</param>
        /// <param name="newCoNum">Number of resulting columns in the array.</param>
        /// <param name="newRoNum">Number of resulting rows in the array.</param>
        private void ResizeMatrix(ref T[,] original, int newCoNum, int newRoNum)
        {
            var newArray = new T[newCoNum, newRoNum];
            int columnCount = original.GetLength(1);
            int columnCount2 = newRoNum;
            int columns = original.GetUpperBound(0);
            for (int co = 0; co <= columns; co++)
                Array.Copy(original, co * columnCount, newArray, co * columnCount2, columnCount);
            original = newArray;
        }
    }
}