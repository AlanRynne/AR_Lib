using System;
using System.Collections.Generic;
using AR_Lib.Collections;

namespace AR_Lib.Geometry
{
    /// <summary>
    /// Contains all methods related to 'The Nurbs Book 2nd Edition' implementation of NURBS curves and surfaces.
    /// </summary>
    public static class NurbsCalculator
    {
#pragma warning disable
        public static Point3d Horner1(Point3d[] a, int n, double u0)
        {
            var C = a[n];
            for (int i = n - 1; i >= 0; i--)
                C = C * u0 + a[i];
            return C;
        }

        public static double Bernstein(int i, int n, double u)
        {
            var temp = new List<double>();
            for (int j = 0; j <= n; j++)
                temp.Add(0.0);
            temp[n - i] = 1.0;

            var u1 = 1.0 - u;
            for (int k = 1; k <= n; k++)
                for (int j = n; j >= k; j--)
                    temp[j] = u1 * temp[j] + u * temp[j - 1];

            return temp[n];
        }

        public static Point3d Horner2(Matrix<Point3d> a, int n, int m, double u0, double v0)
        {
            var b = new Point3d[n];
            for (int i = 0; i <= n; i++)
                b[i] = Horner1(a.Row(i), m, v0);
            return Horner1(b, n, u0);
        }

        public static double[] AllBernstein(int n, double u)
        {
            var B = new double[n + 1];
            B[0] = 1.0;
            double u1 = 1.0 - u;
            for (int j = 1; j <= n; j++)
            {
                var saved = 0.0;
                for (int k = 0; k < j; k++)
                {
                    var temp = B[k];
                    B[k] = saved + u1 * temp;
                    saved = u * temp;
                }
                B[j] = saved;
            }
            return B;
        }

        public static Point3d PointOnBezierCurve(List<Point3d> P, int n, double u)
        {
            Point3d C = new Point3d();

            double[] B = AllBernstein(n, u);
            for (int k = 0; k <= n; k++)
                C += B[k] * P[k];

            return C;
        }

        public static Point3d DeCasteljau1(Point3d[] P, int n, double u)
        {
            var Q = new Point3d[n + 1];
            for (int i = 0; i <= n; i++)
                Q[i] = new Point3d(P[i]);
            for (int k = 1; k <= n; k++)
                for (int i = 0; i <= n - k; i++)
                    Q[i] = ((1.0 - u) * Q[i]) + (u * Q[i + 1]);
            return Q[0];
        }

        public static Point3d DeCasteljau2(Matrix<Point3d> P, int n, int m, double u0, double v0)
        {
            var Q = new List<Point3d>();
            if (n <= m)
            {
                for (int j = 0; j <= m; j++)
                    Q.Add(DeCasteljau1(P.Row(j), n, u0));
                return DeCasteljau1(Q.ToArray(), m, v0);
            }
            else
            {
                for (int i = 0; i <= n; i++)
                    Q.Add(DeCasteljau1(P.Column(i), n, u0));
                return DeCasteljau1(Q.ToArray(), n, u0);
            }
        }

        public static int FindSpan(int n, int p, double u, IList<double> U)
        {
            if (u == U[n + 1])
                return n;
            int low = p;
            int high = n + 1;
            int mid = (low + high) / 2;
            while (u < U[mid] || u >= U[mid + 1])
            {
                if (u < U[mid])
                    high = mid;
                else
                    low = mid;
                mid = (low + high) / 2;
            }
            return mid;
        }

        public static double[,] AllBasisFuns(int span, double u, int p, IList<double> U)
        {
            var N = new double[p + 1, p + 1];
            for (int i = 0; i <= p; i++)
                for (int j = 0; j <= i; j++)
                    N[j, i] = OneBasisFun(p, U.Count - 1, U, span - i + j, u);
            return N;
        }

        public static double[] BasisFuns(int i, double u, int p, IList<double> U)
        {
            var N = new double[p + 1];
            var left = new double[p + 1];
            var right = new double[p + 1];
            N[0] = 1.0;
            for (int j = 1; j <= p; j++)
            {
                left[j] = u - U[i + 1 - j];
                right[j] = U[i + j] - u;
                var saved = 0.0;
                for (int r = 0; r < j; r++)
                {
                    var temp = N[r] / (right[r + 1] + left[j - r]);
                    N[r] = saved + right[r + 1] * temp;
                    saved = left[j - r] * temp;
                }
                N[j] = saved;
            }
            return N;
        }

        public static Matrix<double> DersBasisFuns(int i, double u, int p, int n, IList<double> U)
        {
            var ders = new Matrix<double>(n, p);
            var ndu = new double[p + 1, p + 1];
            var a = new double[2, p + 1];
            var left = new double[p + 1];
            var right = new double[p + 1];

            ndu[0, 0] = 1.0;
            for (int j = 1; j <= p; j++)
            {
                left[j] = u - U[i + 1 - j];
                right[j] = U[i + j] - u;
                var saved = 0.0;
                for (int r = 0; r < j; r++)
                {
                    ndu[j, r] = right[r + 1] + left[j - r];
                    var temp = ndu[r, j - 1] / ndu[j, r];
                    ndu[r, j] = saved + right[r + 1] * temp;
                    saved = left[j - r] * temp;
                }
                ndu[j, j] = saved;
            }

            for (int j = 0; j <= p; j++)
                ders[0, j] = ndu[j, p];

            for (int r = 0; r <= p; r++)
            {
                var s1 = 0;
                var s2 = 1;
                a[0, 0] = 1.0;
                for (int k = 1; k <= n; k++)
                {
                    var d = 0.0;
                    var rk = r - k;
                    var pk = p - k;
                    if (r >= k)
                    {
                        a[s2, 0] = a[s1, 0] / ndu[pk + 1, rk];
                        d = a[s2, 0] * ndu[rk, pk];
                    }

                    int j1 = (rk >= -1) ? 1 : -rk;
                    int j2 = (r - 1 <= pk) ? k - 1 : p - r;

                    for (int j = j1; j <= j2; j++)
                    {
                        a[s2, j] = (a[s1, j] - a[s1, j - 1]) / ndu[pk + 1, rk + j];
                        d += a[s2, j] * ndu[rk + j, pk];
                    }
                    if (r <= pk)
                    {
                        a[s2, k] = -a[s1, k - 1] / ndu[pk + 1, r];
                        d += a[s2, k] * ndu[r, pk];
                    }
                    ders[k, r] = d;

                    // Switch rows
                    var temp = s1;
                    s1 = s2;
                    s2 = temp;
                }
            }
            var r0 = p;
            for (int k = 1; k <= n; k++)
            {
                for (int j = 0; j <= p; j++)
                    ders[k, j] *= r0;
                r0 *= p - k;
            }
            return ders;
        }

        public static double OneBasisFun(int p, int m, IList<double> U, int i, double u)
        {
            if ((i == 0 && u == U[0]) || (i == (m - p - 1) && u == U[m]))
                return 1.0;
            if (u < U[i] || u >= U[i + p + 1])
                return 0.0;
            // Initialize zeroth-degree functions
            var N = new double[p + 1];
            for (int j = 0; j <= p; j++)
            {
                if (u >= U[i + j] && u < U[i + j + 1])
                    N[j] = 1.0;
                else
                    N[j] = 0.0;
            }
            for (int k = 1; k <= p; k++)
            {
                double saved = N[0] == 0.0 ? 0.0 : (u - U[i]) * N[0] / (U[i + k] - U[i]);
                for (int j = 0; j < (p - k + 1); j++)
                {
                    var Uleft = U[i + j + 1];
                    var Uright = U[i + j + k + 1];
                    if (N[j + 1] == 0.0)
                    {
                        N[j] = saved;
                        saved = 0.0;
                    }
                    else
                    {
                        var temp = N[j + 1] / (Uright - Uleft);
                        N[j] = saved + ((Uright - u) * temp);
                        saved = (u - Uleft) * temp;
                    }
                }
            }
            return N[0];
        }

        public static double[] DersOneBasisFun(int p, int m, IList<double> U, int i, double u, int n)
        {
            // TODO: Check unused m parameter.
            var ders = new double[n + 1];
            if (u < U[i] || u >= U[i + p + 1])
            {
                for (int k = 0; k <= n; k++)
                    ders[k] = 0.0;
                return ders;
            }
            var N = new double[p + 1, p + 1];
            for (int j = 0; j <= p; j++)
            {
                if (u >= U[i + j] && u < U[i + j + 1])
                    N[j, 0] = 1.0;
            }
            for (int k = 1; k <= p; k++)
            {
                double saved;
                if (N[0, k - 1] == 0.0)
                    saved = 0.0;
                else
                    saved = (u - U[i]) * N[0, k - 1] / (U[i + k] - U[i]);

                for (int j = 0; j < p - k + 1; j++)
                {
                    double Uleft = U[i + j + 1];
                    double Uright = U[i + j + k + 1];
                    if (N[j + 1, k - 1] == 0.0)
                    {
                        N[j, k] = saved;
                        saved = 0.0;
                    }
                    else
                    {
                        double temp = N[j + 1, k - 1] / (Uright - Uleft);
                        N[j, k] = saved + ((Uright - u) * temp);
                        saved = (u - Uleft) * temp;
                    }
                }
            }
            ders[0] = N[0, p];

            double[] ND = new double[n + 1];
            for (int k = 1; k <= n; k++)
            {
                for (int j = 0; j <= k; j++)
                {
                    ND[j] = N[j, p - k];
                }
                for (int jj = 1; jj <= k; jj++)
                {
                    double saved;
                    if (ND[0] == 0.0)
                        saved = 0.0;
                    else
                        saved = ND[0] / (U[i + p - k + jj] - U[i]);
                    for (int j = 0; j < k - jj + 1; j++)
                    {
                        double Uleft = U[i + j + 1];
                        double Uright = U[i + j + p + jj + 1];
                        if (ND[j + 1] == 0.0)
                        {
                            ND[j] = (p - k + jj) * saved;
                            saved = 0.0;
                        }
                        else
                        {
                            double temp = ND[j + 1] / (Uright - Uleft);
                            ND[j] = (p - k + jj) * (saved - temp);
                            saved = temp;
                        }
                    }
                }
                ders[k] = ND[0];
            }
            return ders;
        }

        public static Point3d CurvePoint(int n, int p, IList<double> U, IList<Point3d> P, double u)
        {
            int span = FindSpan(n, p, u, U);
            double[] N = BasisFuns(span, u, p, U);
            Point3d C = Point3d.Unset;
            for (int i = 0; i <= p; i++)
                C += N[i] * P[span - p + i];
            return C;
        }

        public static Vector3d[] CurveDerivsAlg1(int n, int p, IList<double> U, IList<Point3d> P, double u, int d)
        {
            var CK = new Vector3d[d + 1];
            var du = Math.Min(d, p);
            for (int k = p + 1; k <= d; k++)
                CK[k] = new Vector3d();
            var span = FindSpan(n, p, u, U);
            var nders = DersBasisFuns(span, u, p, du, U);
            for (int k = 0; k <= du; k++)
            {
                CK[k] = new Vector3d();
                for (int j = 0; j <= p; j++)
                    CK[k] = CK[k] + (nders[k, j] * (Vector3d)P[span - p + j]);

            }
            return CK;
        }

        public static Point3d[,] CurveDerivCpts(int n, int p, IList<double> U, IList<Point3d> P, int d, int r1, int r2)
        {
            var r = r2 - r1;
            var PK = new Point3d[d + 1, r];
            for (int i = 0; i <= r; i++)
                PK[0, i] = P[r1 + i];
            for (int k = 1; k <= d; k++)
            {
                var tmp = p - k + 1;
                for (int i = 0; i <= r - k; i++)
                {
                    PK[k, i] = tmp * (Point3d)(PK[k - 1, i + 1] - PK[k - 1, i]) / (U[r1 + i + p + 1] - U[r1 + i + k]);
                }
            }
            return PK;
        }

        public static Vector3d[] CurveDerivsAlg2(int n, int p, IList<double> U, IList<Point3d> P, double u, int d)
        {
            var du = Math.Min(d, p);
            var CK = new Vector3d[d + 1];
            for (int k = p + 1; k <= d; k++)
                CK[k] = new Vector3d();
            var span = FindSpan(n, p, u, U);
            var N = AllBasisFuns(span, u, p, U);
            var PK = CurveDerivCpts(n, p, U, P, du, span - p, span);
            for (int k = 0; k <= du; k++)
            {
                CK[k] = new Vector3d();
                for (int j = 0; j <= p - k; j++)
                    CK[k] = CK[k] + (N[j, p - k] * (Vector3d)PK[k, j]);
            }
            return CK;
        }

        public static Point3d SurfacePoint(int n, int p, IList<double> U, int m, int q, IList<double> V, Matrix<Point3d> P, double u, double v)
        {
            var uspan = FindSpan(n, p, u, U);
            var Nu = BasisFuns(uspan, u, p, U);
            var vspan = FindSpan(m, q, v, V);
            var Nv = BasisFuns(vspan, v, q, V);
            var uind = uspan - p;
            var S = Point3d.Unset;
            for (int l = 0; l <= q; l++)
            {
                var temp = Point3d.Unset;
                var vind = vspan - q - l;
                for (int k = 0; k <= p; k++)
                    temp += Nu[k] * P[uind + k, vind];
                S += Nv[l] * temp;
            }
            return S;
        }
        public static double[] CreateUnitKnotVector(int n, int p)
        {
            if (p > n)
                throw new Exception("Degree cannot be bigger than 'ControlPoints - 1'");
            var U = new double[n + p + 2];
            for (int i = 0; i <= p; i++)
                U[i] = 0.0;
            for (int i = p + 1; i < n + 1; i++)
                U[i] = ((double)i - p) / (n - p + 1);
            for (int i = n + 1; i < n + p + 2; i++)
                U[i] = 1.0;
            return U;
        }
    }
}