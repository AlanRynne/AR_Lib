using System;
using System.Collections.Generic;

namespace AR_Lib.LinearAlgebra
{
    public class Triplet
    {
        // Public fields
        public List<TripletData> Values { get => _values; }
        public int M { get => _m; }
        public int N { get => _n; }

        // Private properties
        private List<TripletData> _values;
        private int _m;
        private int _n;

        // Constructor
        public Triplet(int m, int n)
        {
            _m = m;
            _n = n;
            _values = new List<TripletData>();
        }

        // Methods
        public void AddEntry(double value, int m, int n)
        {
            TripletData tD = new TripletData()
            {
                Value = value,
                Row = m,
                Column = n
            };

            _values.Add(tD);
        }
    }

    public struct TripletData
    {
        public int Row;
        public int Column;
        public double Value;
    }
}
