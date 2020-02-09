using System;
using System.Collections.Generic;

namespace AR_Lib.LinearAlgebra
{
    public class Triplet
    {
        // Public fields
        public List<TripletData> Values { get; }
        public int M { get; }
        public int N { get; }

        // Constructor
        public Triplet(int m, int n)
        {
            M = m;
            N = n;
            Values = new List<TripletData>();
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

            Values.Add(tD);
        }
    }

    public struct TripletData
    {
        public int Row;
        public int Column;
        public double Value;
    }
}
