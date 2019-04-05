namespace AR_Lib
{
    public class Matrix<T>
    {
        /// Matrix Class
        /// This class was obtained from:
        /// https://codereview.stackexchange.com/questions/194732/class-matrix-implementation

        private readonly T[,] _data;
        public int N => _data.GetUpperBound(0) + 1;
        public int M => _data.GetUpperBound(1) + 1;

        public Matrix(int n)
        {
            _data = new T[n, n];
        }

        public Matrix(int n, int m)
        {
            _data = new T[n, m];
        }

        public Matrix(T[,] data)
        {
            _data = data;
        }

        public ref T this[int row, int column] => ref _data[row, column];
    }


}
