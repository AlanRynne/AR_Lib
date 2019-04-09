using System;
using System.Collections.Generic;
using System.Text;

namespace AR_Lib.Geometry
{
    //HACK: This implementation needs to be cleaned up and fixed:

    //FIXME: Coordinates should be divided by weight

    //FIXME: Degree 3 works but others do strange stuff (deg = 1 or 2 creates a surface with a subset of the control points)

    /// <summary>
    /// Class representing an arbitrary N.U.R.B.S surface.
    /// </summary>
    public class Surface
    {
        // This class is implemented based on the explanation on NURBS found at:
        // https://www.gamasutra.com/view/feature/131808/using_nurbs_surfaces_in_realtime_.php?page=4

        #region Public Fields

        public int UDegree { get => uDegree; set => uDegree = value; }
        public int VDegree { get => vDegree; set => vDegree = value; }
        public List<Point4d> ControlPoints { get => controlPoints; set => controlPoints = value; }
        public List<Point3d> PVertices { get => pVertices; set => pVertices = value; }
        public List<double> UKnots { get => uKnots; set => uKnots = value; }
        public List<double> VKnots { get => vKnots; set => vKnots = value; }

        #endregion


        #region Private Properties

        // Integer properties
        int uDegree, vDegree; // Surface degrees
        int uOrder, vOrder; // Surface order
        int uKnotCount, vKnotCount; // Knot count in each direction
        int uControlPointCount, vControlPointCount; // Control point count in each direction
        int uBasisSpanCount, vBasisSpanCount; // Basis span count in each direction
        int uTessellationCount, vTessellationCount; // Tesselation count in each direction (for rendering)

        // Collection properties
        List<Point4d> controlPoints; //TODO: This should actually be a Matrix<T>
        List<double> uBasisCoefficients, vBasisCoefficients; // Computed basis coefficients
        List<double> uKnots, vKnots; // Knot values in each direction
        List<double> uBasis, duBasis, vBasis, dvBasis;
        List<Point4d> uTemp, duTemp;
        List<int> uTessKnotSpan, vTessKnotSpan;
        List<Point3d> pVertices;

        // Other properties
        bool isValid;



        #endregion


        #region Constructors

        // TODO: Implement constructors
        public Surface(int uDeg, int vDeg, int uCntrPts, int vCntrlPts, List<Point4d> cntrlPts, List<double> uKnotsValues, List<double> vKnotsValues, int uTessellations, int vTessellations)
        {
            //Assign incoming values

            uDegree = uDeg;
            vDegree = vDeg;
            uControlPointCount = uCntrPts;
            vControlPointCount = vCntrlPts;
            controlPoints = cntrlPts;
            uKnots = uKnotsValues;
            vKnots = vKnotsValues;

            //Compute some useful property values

            uOrder = uDegree + 1;
            vOrder = vDegree + 1;

            uKnotCount = uOrder + uControlPointCount;
            vKnotCount = vOrder + vControlPointCount;

            uBasisSpanCount = uOrder - 2 + uControlPointCount;
            vBasisSpanCount = vOrder - 2 + vControlPointCount;

            // Initialize empty objects

            uBasisCoefficients = new List<double>();
            vBasisCoefficients = new List<double>();
            uBasis = new List<double>();
            duBasis = new List<double>();
            vBasis = new List<double>();
            duBasis = new List<double>();
            uTemp = new List<Point4d>();
            duTemp = new List<Point4d>();
            uTessKnotSpan = new List<int>();
            vTessKnotSpan = new List<int>();
            pVertices = new List<Point3d>();

            // Run initialization routine

            ComputeBasisCoefficients();

            SetTesselations(uTessellations, vTessellations);

        }

        #endregion


        #region Public Methods

        public void TessellateSurface()
        {
            //FIXME: Rename this variables to fit the convention.

            List<Point4d> pControlPoints = controlPoints;
            int u, v, k, l;
            int uKnot, vKnot;
            List<Point4d> UTemp = uTemp, dUTemp = duTemp;
            Point4d Pw = new Point4d();
            double rhw;
            int iVertices;
            int iCPOffset;
            double VBasis, dVBasis;
            int idx, uidx;

            if ((uTessellationCount == 0) || (vTessellationCount == 0))
                return;

            iVertices = 2 * (vTessellationCount + 1);

            // Step over the U and V coordinates and generate triangle strips to render
            //
            for (u = 0; u <= uTessellationCount; u++)
            {
                // What's the current knot span in the U direction?
                uKnot = uTessKnotSpan[u];

                // Calculate the offset into the pre-calculated basis functions array
                uidx = u * uOrder;
                vKnot = -1;

                // Create one row of vertices
                for (v = 0; v <= vTessellationCount; v++)
                {
                    idx = u * uTessellationCount + v;
                    if (vKnot != vTessKnotSpan[v])
                    {
                        vKnot = vTessKnotSpan[v];
                        //
                        // If our knot span in the V direction has changed, then calculate some
                        // temporary variables.  These are the sum of the U-basis functions times
                        // the control points (times the weights because the control points have
                        // the weights factored in).
                        //
                        for (k = 0; k <= vDegree; k++)
                        {
                            iCPOffset = (uKnot - uDegree) * vControlPointCount + (vKnot - vDegree);

                            UTemp.Add(new Point4d());
                            UTemp[k] = uBasis[uidx] * pControlPoints[iCPOffset + k];

                            dUTemp.Add(new Point4d());
                            duTemp[k] = duBasis[uidx] * pControlPoints[iCPOffset + k];

                            for (l = 1; l <= uDegree; l++)
                            {
                                iCPOffset += vControlPointCount;
                                UTemp[k] += uBasis[uidx + l] * pControlPoints[iCPOffset + k];
                                duTemp[k] += duBasis[uidx + l] * pControlPoints[iCPOffset + k];
                            }
                        }
                    }

                    // Compute the point in the U and V directions
                    VBasis = vBasis[(v * vOrder)];
                    dVBasis = dvBasis[(v * vOrder)];
                    Pw = VBasis * UTemp[0];

                    for (k = 1; k <= vDegree; k++)
                    {
                        VBasis = vBasis[(v * vOrder + k)];
                        dVBasis = dvBasis[(v * vOrder + k)];
                        Pw += VBasis * UTemp[k];
                    }

                    // rhw is the factor to multiply by inorder to bring the 4-D points back into 3-D                    
                    Pw *= 1.0 / Pw.Weight;

                    // Store the vertex position.
                    pVertices.Add(new Point3d(Pw));

                }
            }


        }
        public void UpdateControlPoints()
        {
            //TODO: Implement UpdateControlPoints
            // This method should contain the logic AFTER control points have been changed
            // i.e.: recomputing some of the basis functions...etc
        }

        #endregion


        #region Private Methods

        // Any private methods should go here

        public double ComputeCoefficient(List<double> knots, int interval, int i, int p, int k)
        {
            //TODO: Check ComputeCoefficient
            double result = 0.0;

            if (p == 0)
            {
                if (i == interval)
                    result = 1.0;
            }
            else if (k == 0)
            {
                if (knots[i + p] != knots[i])
                    result -= knots[i] * ComputeCoefficient(knots, interval, i, p - 1, 0) / (knots[i + p] - knots[i]);
                if (knots[i + p + 1] != knots[i + 1])
                    result += knots[i + p + 1] * ComputeCoefficient(knots, interval, i + 1, p - 1, 0) / (knots[i + p + 1] - knots[i + 1]);
            }
            else if (k == p)
            {
                if (knots[i + p] != knots[i])
                    result += ComputeCoefficient(knots, interval, i, p - 1, p - 1) / (knots[i + p] - knots[i]);
                if (knots[i + p + 1] != knots[i + 1])
                    result -= ComputeCoefficient(knots, interval, i + 1, p - 1, p - 1) / (knots[i + p + 1] - knots[i + 1]);
            }
            else if (k > p)
            {
                result = 0.0;
            }
            else
            {
                double C1, C2;
                if (knots[i + p] != knots[i])
                {
                    C1 = ComputeCoefficient(knots, interval, i, p - 1, k - 1);
                    C2 = ComputeCoefficient(knots, interval, i, p - 1, k);
                    result += (C1 - knots[i] * C2) / (knots[i + p] - knots[i]);
                }
                if (knots[i + p + 1] != knots[i + 1])
                {
                    C1 = ComputeCoefficient(knots, interval, i + 1, p - 1, k - 1);
                    C2 = ComputeCoefficient(knots, interval, i + 1, p - 1, k);
                    result -= (C1 - knots[i + p + 1] * C2) / (knots[i + p + 1] - knots[i + 1]);
                }
            }
            return result;
        }
        public void ComputeBasisCoefficients()
        {
            //TODO: Check ComputeBasisCoefficients
            int i, j, k;

            //
            // Start with U.  For each Basis span calculate coefficients
            // for uOrder polynomials each having uOrder coefficients
            //

            for (i = 0; i < uBasisSpanCount; i++)
            {
                for (j = 0; j < uOrder; j++)
                {
                    for (k = 0; k < uOrder; k++)
                    {
                        uBasisCoefficients.Add(
                            ComputeCoefficient(uKnots, i + uDegree, i + j, uDegree, k));
                    }
                }
            }

            for (i = 0; i < vBasisSpanCount; i++)
            {
                for (j = 0; j < vOrder; j++)
                {
                    for (k = 0; k < vOrder; k++)
                    {
                        vBasisCoefficients.Add(
                            ComputeCoefficient(vKnots, i + vDegree, i + j, vDegree, k));
                    }
                }
            }

        }
        public void EvaluateBasisFunctions()
        {
            //TODO: Check EvaluateBasisFunctions, rename properties to something more meaningfull
            int i, j, k, idx;
            double u, uinc;
            double v, vinc;

            //
            // First evaluate the U basis functions and derivitives at uniformly spaced u values
            //
            idx = 0;
            u = uKnots[idx + uDegree];
            uinc = (uKnots[uKnotCount - uOrder] - uKnots[uDegree]) / (uTessellationCount);

            for (i = 0; i <= uTessellationCount; i++)
            {
                while ((idx < uKnotCount - uDegree * 2 - 2) && (u >= uKnots[idx + uDegree + 1]))
                    idx++;

                uTessKnotSpan.Add(idx + uDegree);

                //
                // Evaluate using Horner's method
                //
                for (j = 0; j < uOrder; j++)
                {
                    uBasis.Add(uBasisCoefficients[(idx * uOrder + j) * uOrder + uDegree]);
                    duBasis.Add(uBasis[(i * uOrder + j)] * uDegree);
                    for (k = uDegree - 1; k >= 0; k--)
                    {
                        uBasis[(i * uOrder + j)] = uBasis[(i * uOrder + j)] * u +
                            uBasisCoefficients[(idx * uOrder + j) * uOrder + k];
                        if (k > 0)
                        {
                            duBasis[(i * uOrder + j)] = duBasis[(i * uOrder + j)] * u +
                                uBasisCoefficients[(idx * uOrder + j) * uOrder + k] * k;
                        }
                    }
                }

                u += uinc;
            }

            //
            // Finally evaluate the V basis functions at uniformly spaced v values
            //
            idx = 0;
            v = vKnots[idx + vDegree];
            vinc = (vKnots[vKnotCount - vOrder] - vKnots[vDegree]) / (vTessellationCount);

            for (i = 0; i <= vTessellationCount; i++)
            {
                while ((idx < vKnotCount - vDegree * 2 - 2) && (v >= vKnots[idx + vDegree + 1]))
                    idx++;

                //vTessKnotSpan[i] = idx + vDegree;
                vTessKnotSpan.Add(idx + vDegree);

                //
                // Evaluate using Horner's method
                //
                for (j = 0; j < vOrder; j++)
                {
                    // vBasis[(i * vOrder + j) * SIMD_SIZE] = vBasisCoefficients[(idx * vOrder + j) * vOrder + vDegree];
                    // dvBasis[(i * vOrder + j) * SIMD_SIZE] = vBasis[(i * vOrder + j) * SIMD_SIZE] * vDegree;
                    vBasis.Add(vBasisCoefficients[(idx * vOrder + j) * vOrder + vDegree]);
                    dvBasis.Add(vBasis[(i * vOrder + j)] * vDegree);
                    for (k = vDegree - 1; k >= 0; k--)
                    {
                        vBasis[(i * vOrder + j)] = vBasis[(i * vOrder + j)] * v +
                            vBasisCoefficients[(idx * vOrder + j) * vOrder + k];
                        if (k > 0)
                        {
                            dvBasis[(i * vOrder + j)] = dvBasis[(i * vOrder + j)] * v +
                                vBasisCoefficients[(idx * vOrder + j) * vOrder + k] * k;
                        }
                    }
                }
                v += vinc;
            }
        }
        private void SetTesselations(int uTessellations, int vTessellations)
        {
            //TODO: Implement SetTesselations

            if ((uTessellations != uTessellationCount) || (vTessellations != vTessellationCount))
            {
                uTessellationCount = uTessellations;
                vTessellationCount = vTessellations;

                //
                // Overwrite all entities with emepty values
                //
                uBasis = new List<double>(uOrder * (uTessellationCount + 1));
                vBasis = new List<double>(vOrder * (vTessellationCount + 1));
                duBasis = new List<double>(uOrder * (uTessellationCount + 1));
                dvBasis = new List<double>(vOrder * (vTessellationCount + 1));

                uTessKnotSpan = new List<int>(uTessellationCount + 1);
                vTessKnotSpan = new List<int>(vTessellationCount + 1);

                int iVertices = ((uTessellations + 1) * (vTessellations + 1)); //2 * (vTessellations + 1);
                pVertices = new List<Point3d>(iVertices);

                //
                // Re-evaluate the basis functions
                //
                EvaluateBasisFunctions();
            }
        }

        #endregion

    }

}
