using System.Collections.Generic;
using Paramdigma.Core.Geometry;

namespace Paramdigma.Core.Optimization
{
    /// <summary>
    /// Generic K-Means Clustering Algorithm for N dimensional vectors.
    /// </summary>
    public class KMeansClustering
    {
        private readonly int maxIterations;
        private readonly int clusterCount;
        private int currentIterations;

        /// <summary>
        /// Gets or sets the list of clusters.
        /// </summary>
        public List<KMeansCluster> Clusters
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KMeansClustering"/> class.
        /// </summary>
        /// <param name="maxIterations">Maximum iterations allowed.</param>
        /// <param name="clusterCount">Desired amount of clusters.</param>
        /// <param name="data">List of N dimensional vectors to cluster.</param>
        public KMeansClustering(int maxIterations, int clusterCount, List<VectorNd> data)
        {
            this.maxIterations = maxIterations;
            this.clusterCount = clusterCount;
            InitializeClusters(data);
        }

        private void InitializeClusters(List<VectorNd> data)
        {
            Clusters = new List<KMeansCluster>(clusterCount);
            for (int i = 0; i < clusterCount; i++)
                Clusters.Add(new KMeansCluster());

            for (int i = 0; i < data.Count; i++)
                Clusters[i % clusterCount].Add(data[i]);
        }

        /// <summary>
        /// Run the algorithm until it reaches the maximum amount of iterations.
        /// </summary>
        public void Run() => Run(maxIterations);

        /// <summary>
        /// Run the k-means clustering algorithm for a specified amount of iterations.
        /// </summary>
        /// <param name="iterations">Iterations to run.</param>
        public void Run(int iterations)
        {
            bool hasChanged = false;
            int iteration = 0;
            do
            {
                hasChanged = false;

                // Compute cluster averages
                var averages = new List<VectorNd>(Clusters.Count);
                Clusters.ForEach(cluster => averages.Add(cluster.Average()));

                // Create placeholder clusters for next iteration
                var newClusters = new List<KMeansCluster>(Clusters.Count);
                for (int i = 0; i < Clusters.Count; i++)
                    newClusters.Add(new KMeansCluster());

                // Find the closest average for each vector in each cluster
                Clusters.ForEach(cluster =>
                {
                    var ind = Clusters.IndexOf(cluster);
                    for (int i = 0; i < cluster.Count; i++)
                    {
                        var vector = cluster[i];
                        int simIndex = FindIndexOfSimilar(averages, vector);
                        newClusters[simIndex].Add(vector);
                        if (simIndex != ind)
                        {
                            hasChanged = true;
                        }
                    }
                });

                // Update clusters and increase iteration
                Clusters = newClusters;
                iteration++;
                currentIterations++;
            }
            while (hasChanged
                     && iteration < iterations
                     && currentIterations < maxIterations);
        }

        /// <summary>
        /// Find the index of the most similar vector to a given one.
        /// </summary>
        /// <param name="pool">List of vectors to compare with.</param>
        /// <param name="vector">Reference vector.</param>
        /// <returns>Index of the most similar vector in the pool.</returns>
        public int FindIndexOfSimilar(List<VectorNd> pool, VectorNd vector)
        {
            double min = double.MaxValue;
            int minIndex = -1;

            for (int i = 0; i < pool.Count; i++)
            {
                var v = pool[i];
                var sim = VectorNd.AngularDistance(v, vector);
                if (sim < min)
                {
                    min = sim;
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}