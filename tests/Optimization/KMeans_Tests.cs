using System;
using System.Collections.Generic;
using Paramdigma.Core.Geometry;
using Paramdigma.Core.Optimization;
using Xunit;

namespace Paramdigma.Core.Tests.Optimization
{
    public class KMeansClusteringTests
    {
        [Fact]
        public void KMeans_MainTest()
        {
            //Generate random vectors
            var vectors = new List<VectorNd>();
            var rnd = new Random();
            for (int i = 0; i < 50; i++)
            {
                var v = new VectorNd(rnd.Next(-100, 100) / 10,
                                     rnd.Next(-100, 100) / 10,
                                     rnd.Next(-100, 100) / 10
                                     );
                vectors.Add(v);
            }

            //When
            var kmeans = new KMeansClustering(10, 5, vectors);
            kmeans.Run();

            var clusters = kmeans.Clusters;
            //Then
        }
    }
}