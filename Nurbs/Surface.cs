using System;
using System.Collections;
using System.Collections.Generic;
using AR_Lib.LinearAlgebra;

namespace AR_Lib.Geometry.Nurbs
{
    class Point4d : Point3d
    {
        public double Weight { get => weight; set { weight = value; if (isUnset) isUnset = false; } }

        private double weight;

        // Constructors
        public Point4d() : base()
        {
            weight = 0;
        }
        public Point4d(double x, double y, double z, double w) : base(x, y, z)
        {
            weight = w;
        }

        public Point4d(Point3d pt, double w) : base(pt)
        {
            weight = w;
        }


    }

    class Surface
    {
        // TODO: Check for license? Make sure of this part
        // This class is implemented based on the explanation on NURBS found at:
        // https://www.gamasutra.com/view/feature/131808/using_nurbs_surfaces_in_realtime_.php?page=4
        #region Public Fields

        #endregion

        #region Computed Properties

        int U_Order => uDegree + 1;
        int V_Order => uDegree + 1;
        int U_Knots => U_Order + uControlPoints;
        int V_Knots => U_Order + vControlPoints;
        int U_BasisSpans => U_Knots - 2 * uDegree;
        int V_BasisSpans => V_Knots - 2 * uDegree;

        #endregion

        #region Private properties

        int uDegree, vDegree;
        int uControlPoints, vControlPoints;
        Matrix<Point4d> controlPoints;
        List<double> uKnotValues, vKnotValues;
        int defaultTesselations;
        bool isValid;
        List<double> uBasisCoefficients, vBasisCoefficients;
        int uTesselations, vTesselations;

        #endregion

        #region Constructors

        // TODO: Implement constructors
        public Surface(
            int uDeg, int vDeg,
            int uCntrPts, int vCntrlPts,
            Matrix<Point4d> cntrlPts,
            List<double> uKnots, List<double> vKnots)
        {
            uDegree = uDeg;
            vDegree = vDeg;
            uControlPoints = uCntrPts;
            vControlPoints = vCntrlPts;
            controlPoints = cntrlPts;
            uKnotValues = uKnots;
            vKnotValues = vKnots;
        }

        #endregion

        #region Public Methods

        public void ComputeBasisCoefficients()
        {

        }
        public void ComputeCoefficient()
        {

        }
        public void EvaluateBasisFunctions()
        {

        }
        public void UpdateControlPoints()
        {
            // This method should contain the logic AFTER control points have been changed
            // i.e.: recomputing some of the basis functions...etc
        }

        #endregion

        #region Private Methods

        // Any private methods should go here

        #endregion
    }
}
