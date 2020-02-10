using System;
using System.Collections.Generic;

#pragma warning disable 1591

namespace AR_Lib.LinearAlgebra
{
    /// <summary>
    /// Represents a set of data in a sparse matrix.
    /// </summary>
    public class Triplet
    {
        // Public fields

        /// <summary>
        /// Gets values held by this triplet.
        /// </summary>
        /// <value></value>
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
                Column = n,
            };

            Values.Add(tD);
        }
    }

    public struct TripletData
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public double Value { get; set; }
    }
}
