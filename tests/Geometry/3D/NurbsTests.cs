using System;
using System.Collections.Generic;
using System.Diagnostics;
using Paramdigma.Core.Collections;
using Paramdigma.Core.Geometry;
using Xunit;

namespace Paramdigma.Core.Tests
{
    public class NurbsTests
    {
        public Matrix<Point3d> FlatGrid(int size)
        {
            var m = new Matrix<Point3d>(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    m[i, j] = new Point3d(i, j, 0);
                }
            }

            return m;
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        public void Decasteljau2_Works(double u, double v)
        {
            const int n = 5;
            var points = FlatGrid(n);
            var pt = NurbsCalculator.DeCasteljau2(points, 3, 3, u, v);

            Assert.NotNull(pt);
        }

        [Fact]
        public void Decasteljau1_Works()
        {
            var points = new List<Point3d>();
            for (int i = 0; i < 5; i++)
                points.Add(new Point3d(i, 0, 0));
            var pt = NurbsCalculator.DeCasteljau1(points.ToArray(), points.Count - 1, 1);
        }

        [Fact]
        public void NurbsCurvePoint_Works()
        {
            var p0 = new Point3d(0, 0, 0);
            var p1 = new Point3d(1, 3, 0);
            var p2 = new Point3d(1.4, 5, 0);
            var p3 = new Point3d(0, 7, 0);
            double[] U = NurbsCalculator.CreateUnitKnotVector(3, 1);
            double[] U2 = NurbsCalculator.CreateUnitKnotVector(3, 2);
            double[] U3 = NurbsCalculator.CreateUnitKnotVector(3, 3);
            var watch = new Stopwatch();
            watch.Start();
            const int n = 100;
            for (int i = 0; i <= n; i++)
            {
                var pt = NurbsCalculator.CurvePoint(3, 1, U, new Point3d[] { p0, p1, p2, p3 }, (double)i / n);
                var pt2 = NurbsCalculator.CurvePoint(3, 2, U2, new Point3d[] { p0, p1, p2, p3 }, (double)i / n);
                var pt3 = NurbsCalculator.CurvePoint(3, 3, U3, new Point3d[] { p0, p1, p2, p3 }, (double)i / n);
                Console.WriteLine($"t: {(double)i / n} — {pt}");
                Console.WriteLine($"t: {(double)i / n} — {pt2}");
                Console.WriteLine($"t: {(double)i / n} — {pt3}");
                Console.WriteLine("---");
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }

        [Fact]
        public void TestKnotVectore()
        {
            var U = NurbsCalculator.CreateUnitKnotVector(3, 1);
            var U2 = NurbsCalculator.CreateUnitKnotVector(3, 2);
            var U3 = NurbsCalculator.CreateUnitKnotVector(3, 3);
        }
    }
}