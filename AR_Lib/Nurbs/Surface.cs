using System;
using System.Collections.Generic;
using System.Text;

using AR_Lib.Collections;

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

        /// <summary>
        /// Degree in the U direction
        /// </summary>
        /// <value>The UDegree property gets/sets the degree value of the surface in the U direction</value>
        public int UDegree { get => uDegree; set => uDegree = value; }

        /// <summary>
        /// Degree in the V direction
        /// </summary>
        /// <value>The UDegree property gets/sets the degree value of the surface in the V direction</value>
        public int VDegree { get => vDegree; set => vDegree = value; }
        
        /// <summary>
        /// The ControlPoints property represents the surface's control points as a list
        /// </summary>
        /// <value>The ControlPoints property gets/sets the control points of the field _controlPoints</value>
        public List<Point4d> ControlPoints { get => _controlPoints; set => _controlPoints = value; }
        
        public List<Point3d> Vertices { get => _vertices; set => _vertices = value; }
        public List<double> UKnots { get => _uKnots; set => _uKnots = value; }
        public List<double> VKnots { get => _vKnots; set => _vKnots = value; }

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
        List<Point4d> _controlPoints; //TODO: This should actually be a Matrix<T>
        List<double> _uBasisCoefficients, _vBasisCoefficients; // Computed basis coefficients
        List<double> _uKnots, _vKnots; // Knot values in each direction
        List<double> _uBasis, _duBasis, _vBasis, _dvBasis;
        List<Point4d> _uTemp, _duTemp;
        List<int> _uTessKnotSpan, _vTessKnotSpan;
        List<Point3d> _vertices;

        // Other properties
        bool _isValid; // TODO: This does nothing for now

        #endregion


        #region Constructors

        // TODO: Implement more constructors
        
        /// <summary>
        /// Initializes a new surface class
        /// </summary>
        /// <param name="uDeg">Degree in U direction</param>
        /// <param name="vDeg">Degree in V direction</param>
        /// <param name="uCntrPts">Control point count in U direction</param>
        /// <param name="vCntrlPts">Control point count in V direction</param>
        /// <param name="cntrlPts">List of control points</param>
        /// <param name="uKnotsValues">Knot values in the U direction</param>
        /// <param name="vKnotsValues">Knot value in the V direction</param>
        /// <param name="uTessellations">Tesselation count in the U direction</param>
        /// <param name="vTessellations">Tesselation count in the V direction</param>
        public Surface(int uDeg, int vDeg, int uCntrPts, int vCntrlPts, List<Point4d> cntrlPts, List<double> uKnotsValues, List<double> vKnotsValues, int uTessellations, int vTessellations)
        {
            //Assign incoming values

            uDegree = uDeg;
            vDegree = vDeg;
            uControlPointCount = uCntrPts;
            vControlPointCount = vCntrlPts;
            _controlPoints = cntrlPts;
            _uKnots = uKnotsValues;
            _vKnots = vKnotsValues;

            //Compute some useful property values

            uOrder = uDegree + 1;
            vOrder = vDegree + 1;

            uKnotCount = uOrder + uControlPointCount;
            vKnotCount = vOrder + vControlPointCount;

            uBasisSpanCount = uOrder - 2 + uDegree;
            vBasisSpanCount = vOrder - 2 + uDegree;

            // Initialize empty objects

            _uBasisCoefficients = new List<double>();
            _vBasisCoefficients = new List<double>();
            _uBasis = new List<double>();
            _duBasis = new List<double>();
            _vBasis = new List<double>();
            _duBasis = new List<double>();
            _uTemp = new List<Point4d>();
            _duTemp = new List<Point4d>();
            _uTessKnotSpan = new List<int>();
            _vTessKnotSpan = new List<int>();
            _vertices = new List<Point3d>();

            // Run initialization routine

            ComputeBasisCoefficients();

            SetTesselations(uTessellations, vTessellations);

        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Tesselates the given surface for rendering
        /// HACK: Currently it only computes the vertices, not the mesh.
        /// </summary>
        public void TessellateSurface()
        {
            //FIXME: Rename this variables to fit the convention.

            List<Point4d> pControlPoints = _controlPoints;
            int u, v, k, l;
            int uKnot, vKnot;
            List<Point4d> UTemp = _uTemp, dUTemp = _duTemp;
            Point4d Pw = new Point4d();
            int vertexCount;
            int iCPOffset;
            double VBasis, dVBasis;
            int idx, uidx;

            if ((uTessellationCount == 0) || (vTessellationCount == 0))
                return;

            vertexCount = 2 * (vTessellationCount + 1);

            // Step over the U and V coordinates and generate triangle strips to render
            //
            for (u = 0; u <= uTessellationCount; u++)
            {
                // What's the current knot span in the U direction?
                uKnot = _uTessKnotSpan[u];

                // Calculate the offset into the pre-calculated basis functions array
                uidx = u * uOrder;
                vKnot = -1;

                // Create one row of vertices
                for (v = 0; v <= vTessellationCount; v++)
                {
                    idx = u * uTessellationCount + v;
                    if (vKnot != _vTessKnotSpan[v])
                    {
                        vKnot = _vTessKnotSpan[v];
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
                            UTemp[k] = _uBasis[uidx] * pControlPoints[iCPOffset + k];

                            dUTemp.Add(new Point4d());
                            _duTemp[k] = _duBasis[uidx] * pControlPoints[iCPOffset + k];

                            for (l = 1; l <= uDegree; l++)
                            {
                                iCPOffset += vControlPointCount;
                                UTemp[k] += _uBasis[uidx + l] * pControlPoints[iCPOffset + k];
                                _duTemp[k] += _duBasis[uidx + l] * pControlPoints[iCPOffset + k];
                            }
                        }
                    }

                    // Compute the point in the U and V directions
                    VBasis = _vBasis[(v * vOrder)];
                    dVBasis = _dvBasis[(v * vOrder)];
                    Pw = VBasis * UTemp[0];

                    for (k = 1; k <= vDegree; k++)
                    {
                        VBasis = _vBasis[(v * vOrder + k)];
                        dVBasis = _dvBasis[(v * vOrder + k)];
                        Pw += VBasis * UTemp[k];
                    }

                    // Bring the 4-D points back into 3-D                    
                    Pw *= 1.0 / Pw.Weight;

                    // Store the vertex position.
                    _vertices.Add(new Point3d(Pw));

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

        /// <summary>
        /// Computes the coefficient given i,p,k values and the current interval
        /// </summary>
        /// <param name="knots">Knot list</param>
        /// <param name="interval">Current interval</param>
        /// <param name="i">i</param>
        /// <param name="p">p</param>
        /// <param name="k">k</param>
        /// <returns>The computed coefficient</returns>
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

        /// <summary>
        /// Compute all basis coefficients of the surface
        /// </summary>
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
                        _uBasisCoefficients.Add(
                            ComputeCoefficient(_uKnots, i + uDegree, i + j, uDegree, k));
                    }
                }
            }

            for (i = 0; i < vBasisSpanCount; i++)
            {
                for (j = 0; j < vOrder; j++)
                {
                    for (k = 0; k < vOrder; k++)
                    {
                        _vBasisCoefficients.Add(
                            ComputeCoefficient(_vKnots, i + vDegree, i + j, vDegree, k));
                    }
                }
            }

        }
        
        /// <summary>
        /// Evaluate the basis functions of the surface
        /// </summary>
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
            u = _uKnots[idx + uDegree];
            uinc = (_uKnots[uKnotCount - uOrder] - _uKnots[uDegree]) / (uTessellationCount);

            for (i = 0; i <= uTessellationCount; i++)
            {
                while ((idx < uKnotCount - uDegree * 2 - 2) && (u >= _uKnots[idx + uDegree + 1]))
                    idx++;

                _uTessKnotSpan.Add(idx + uDegree);

                //
                // Evaluate using Horner's method
                //
                for (j = 0; j < uOrder; j++)
                {
                    _uBasis.Add(_uBasisCoefficients[(idx * uOrder + j) * uOrder + uDegree]);
                    _duBasis.Add(_uBasis[(i * uOrder + j)] * uDegree);
                    for (k = uDegree - 1; k >= 0; k--)
                    {
                        _uBasis[(i * uOrder + j)] = _uBasis[(i * uOrder + j)] * u +
                            _uBasisCoefficients[(idx * uOrder + j) * uOrder + k];
                        if (k > 0)
                        {
                            _duBasis[(i * uOrder + j)] = _duBasis[(i * uOrder + j)] * u +
                                _uBasisCoefficients[(idx * uOrder + j) * uOrder + k] * k;
                        }
                    }
                }

                u += uinc;
            }

            //
            // Finally evaluate the V basis functions at uniformly spaced v values
            //
            idx = 0;
            v = _vKnots[idx + vDegree];
            vinc = (_vKnots[vKnotCount - vOrder] - _vKnots[vDegree]) / (vTessellationCount);

            for (i = 0; i <= vTessellationCount; i++)
            {
                while ((idx < vKnotCount - vDegree * 2 - 2) && (v >= _vKnots[idx + vDegree + 1]))
                    idx++;

                //vTessKnotSpan[i] = idx + vDegree;
                _vTessKnotSpan.Add(idx + vDegree);

                //
                // Evaluate using Horner's method
                //
                for (j = 0; j < vOrder; j++)
                {
                    // vBasis[(i * vOrder + j) * SIMD_SIZE] = vBasisCoefficients[(idx * vOrder + j) * vOrder + vDegree];
                    // dvBasis[(i * vOrder + j) * SIMD_SIZE] = vBasis[(i * vOrder + j) * SIMD_SIZE] * vDegree;
                    _vBasis.Add(_vBasisCoefficients[(idx * vOrder + j) * vOrder + vDegree]);
                    _dvBasis.Add(_vBasis[(i * vOrder + j)] * vDegree);
                    for (k = vDegree - 1; k >= 0; k--)
                    {
                        _vBasis[(i * vOrder + j)] = _vBasis[(i * vOrder + j)] * v +
                            _vBasisCoefficients[(idx * vOrder + j) * vOrder + k];
                        if (k > 0)
                        {
                            _dvBasis[(i * vOrder + j)] = _dvBasis[(i * vOrder + j)] * v +
                                _vBasisCoefficients[(idx * vOrder + j) * vOrder + k] * k;
                        }
                    }
                }
                v += vinc;
            }
        }

        /// <summary>
        /// Set the ammount of tesselations for surface rendering
        /// </summary>
        /// <param name="uTessellations">Tesselations in the U direction</param>
        /// <param name="vTessellations">Tesselations in the V direction</param>
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
                _uBasis = new List<double>(uOrder * (uTessellationCount + 1));
                _vBasis = new List<double>(vOrder * (vTessellationCount + 1));
                _duBasis = new List<double>(uOrder * (uTessellationCount + 1));
                _dvBasis = new List<double>(vOrder * (vTessellationCount + 1));

                _uTessKnotSpan = new List<int>(uTessellationCount + 1);
                _vTessKnotSpan = new List<int>(vTessellationCount + 1);

                int iVertices = ((uTessellations + 1) * (vTessellations + 1)); //2 * (vTessellations + 1);
                _vertices = new List<Point3d>(iVertices);

                //
                // Re-evaluate the basis functions
                //
                EvaluateBasisFunctions();
            }
        }

        #endregion

    }

}
