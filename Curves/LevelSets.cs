using System;
using System.Collections.Generic;
using AR_Lib.HalfEdgeMesh;
using AR_Lib.Curve;
using AR_Lib.Geometry;

namespace AR_Lib.Curve
{
    public static class LevelSets
    {
        public static void Compute(string valueKey, List<double> levels, HE_Mesh mesh, out List<List<Line>> levelSets)
        {
            List<List<Line>> resultLines = new List<List<Line>>();

            for(int i = 0;i < levels.Count; i++)
            {
                resultLines.Add(new List<Line>());
            }
            int iter = 0;
            foreach(HE_Face face in mesh.Faces)
            {
                int count = 0;
                foreach(double level in levels)
                {
                    Line l = new Line();
                    if(getFaceLevel(valueKey,level,face,out l)) 
                    {
                        resultLines[count].Add(l);
                    }

                    count++;
                }
                iter++;
            }

            levelSets = resultLines;
        } 

        public static bool getFaceLevel(string valueKey, double level, HE_Face face, out Line line)
        {
            List<HE_Vertex> adj = face.adjacentVertices();
            List<double> vertexValues = new List<double>{ adj[0].UserValues[valueKey], adj[1].UserValues[valueKey], adj[2].UserValues[valueKey] };

            List<int> above = new List<int>();
            List<int> below = new List<int>();
 
            for (int i = 0; i < vertexValues.Count; i++)
            {
                if (vertexValues[i] < level) below.Add(i);
                else above.Add(i);
            }

            if (above.Count == 3 || below.Count == 3)
            {
                // Triangle is above or below level
                line = new Line(new Point3d(),new Point3d());
                return false;
            }
            else
            {
                // Triangle intersects level
                List<Point3d> intersectionPoints = new List<Point3d>();

                foreach (int i in above)
                {
                    foreach (int j in below)
                    {
                        double diff = vertexValues[i] - vertexValues[j];
                        double desiredDiff = level - vertexValues[j];
                        double unitizedDistance = desiredDiff / diff;
                        Vector3d edgeV = adj[i] - adj[j];
                        Point3d levelPoint = (Point3d)adj[j] + (edgeV * unitizedDistance);
                        intersectionPoints.Add(levelPoint);
                    }
                }
                line = new Line(intersectionPoints[0], intersectionPoints[1]);
                return true;
            }


        }
    }
}

        // bool CheckLevelSetInFace(double level, MeshFace face, out Line line)
        // {
       //     List<double> vertexValues = new List<double> { VertexValues[face.A], VertexValues[face.B], VertexValues[face.C] };
        //     List<Point3d> faceVertices = new List<Point3d> { _mesh.Vertices[face.A], _mesh.Vertices[face.B], _mesh.Vertices[face.C] };

        //     List<int> above = new List<int>();
        //     List<int> below = new List<int>();
 
        //     for (int i = 0; i < vertexValues.Count; i++)
        //     {
        //         if (vertexValues[i] < level) below.Add(i);
        //         else above.Add(i);
        //     }

        //     if (above.Count == 3 || below.Count == 3)
        //     {
        //         // Triangle is above or below level
        //         line = new Line();
        //         return false;
        //     }
        //     else
        //     {
        //         // Triangle intersects level
        //         List<Point3d> intersectionPoints = new List<Point3d>();

        //         foreach (int i in above)
        //         {
        //             foreach (int j in below)
        //             {
        //                 double diff = vertexValues[i] - vertexValues[j];
        //                 double desiredDiff = level - vertexValues[j];
        //                 double unitizedDistance = desiredDiff / diff;
        //                 Vector3d edgeV = faceVertices[i] - faceVertices[j];
        //                 Point3d levelPoint = faceVertices[j] + edgeV * unitizedDistance;
        //                 intersectionPoints.Add(levelPoint);
        //             }
        //         }
        //         line = new Line(intersectionPoints[0], intersectionPoints[1]);
        //         return true;
        //     }
        // }
