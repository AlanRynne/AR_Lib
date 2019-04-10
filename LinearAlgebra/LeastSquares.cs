using System;
using System.Collections.Generic;
using AR_Lib.Geometry;

namespace AR_Lib.LinearAlgebra
{
    public static class LineFit2d
    {

        // Find the least squares linear fit.
        // Return the total error.
        // Found at: http://csharphelper.com/blog/2014/10/find-a-linear-least-squares-fit-for-a-set-of-points-in-c/
        public static double FindLinearLeastSquaresFit(
            List<Point2d> points, out double m, out double b)
        {
            // Perform the calculation.
            // Find the values S1, Sx, Sy, Sxx, and Sxy.
            double S1 = points.Count;
            double Sx = 0, Sy = 0, Sxx = 0, Sxy = 0;

            foreach (Point2d pt in points)
            {
                Sx += pt.X;
                Sy += pt.Y;
                Sxx += pt.X * pt.X;
                Sxy += pt.X * pt.Y;
            }

            // Solve for m and b.
            m = (Sxy * S1 - Sx * Sy) / (Sxx * S1 - Sx * Sx);
            b = (Sxy * Sx - Sy * Sxx) / (Sx * Sx - S1 * Sxx);

            return Math.Sqrt(ErrorSquared(points, m, b));
        }
        // Return the error squared.
        private static double ErrorSquared(List<Point2d> points, double m, double b)
        {
            double total = 0;
            foreach (Point2d pt in points)
            {
                double dy = pt.Y - (m * pt.X + b);
                total += dy * dy;
            }
            return total;
        }
    }
}